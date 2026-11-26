using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;
using NarshaStaticSite.Data;
using System.Net;
using System.Text;

namespace NarshaStaticSite.Controllers;

public class StaticSiteGenerationController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _config;

    public StaticSiteGenerationController(IHttpClientFactory httpClientFactory, IWebHostEnvironment env, IConfiguration config)
    {
        _httpClientFactory = httpClientFactory;
        _env = env;
        _config = config;
    }

    [HttpGet("generate-static")]
    public async Task<IActionResult> Generate()
    {
        var outDir = Path.Combine(_env.ContentRootPath, "static-output");
        var baseUrlVal = $"{Request.Scheme}://{Request.Host}";

        if (Directory.Exists(outDir))
            Directory.Delete(outDir, true);

        Directory.CreateDirectory(outDir);

        var urls = new List<string>
        {
            SiteRoutes.Home,
            SiteRoutes.Faq,
            SiteRoutes.PaymentMethods,
            SiteRoutes.CustomSewing,
            SiteRoutes.ContactUs,
            SiteRoutes.NotFound,
            SiteRoutes.Sitemap,
            SiteRoutes.Robots
        };
        foreach (var c in SiteData.Categories())
            urls.Add("/" + c.Slug);
        foreach (var p in SiteData.Products())
            urls.Add("/" + p.Slug);

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.UserAgent.ParseAdd("NarshaStaticSiteGenerator/1.0");
        client.DefaultRequestHeaders.Add("X-Static-Generation", "true");

        var generated = new List<string>();

        foreach (var url in urls)
        {
            var resp = await client.GetAsync(baseUrlVal + url);
            if (resp.StatusCode != HttpStatusCode.OK)
                return StatusCode((int)resp.StatusCode, $"Failed to fetch {url}: {resp.StatusCode} {resp.ReasonPhrase}");

            var content = await resp.Content.ReadAsStringAsync();
            var relPath = OutputPathFor(url);
            var filePath = Path.Combine(outDir, relPath);
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            await System.IO.File.WriteAllTextAsync(filePath, content, Encoding.UTF8);
            generated.Add(relPath);
        }

        CopyDirectory(_env.WebRootPath, outDir);

        return Ok(new
        {
            outputPath = Path.GetFullPath(outDir),
            baseUrl = baseUrlVal,
            pagesGenerated = generated.Count,
            files = generated
        });
    }

    [HttpGet("upload-html")]
    public async Task<IActionResult> UploadHtml()
    {
        var section = _config.GetSection("ArvanStorage");
        var endpoint = section["Endpoint"];
        var accessKey = section["AccessKey"];
        var secretKey = section["SecretKey"];
        var bucketName = section["BucketName"];
        var forcePathStyle = bool.TryParse(section["ForcePathStyle"], out var fps) && fps;

        if (string.IsNullOrWhiteSpace(endpoint) ||
            string.IsNullOrWhiteSpace(accessKey) ||
            string.IsNullOrWhiteSpace(secretKey) ||
            string.IsNullOrWhiteSpace(bucketName))
        {
            return BadRequest("ArvanStorage settings (Endpoint, AccessKey, SecretKey, BucketName) are required in appsettings.json.");
        }

        var outDir = Path.Combine(_env.ContentRootPath, "static-output");
        if (!Directory.Exists(outDir))
            return NotFound($"Output directory not found: {outDir}. Run /generate-static first.");

        var htmlFiles = Directory.GetFiles(outDir, "*.html", SearchOption.AllDirectories);
        if (htmlFiles.Length == 0)
            return NotFound("No .html files found in static-output. Run /generate-static first.");

        var s3Config = new AmazonS3Config
        {
            ServiceURL = endpoint,
            ForcePathStyle = forcePathStyle
        };

        using var client = new AmazonS3Client(new BasicAWSCredentials(accessKey, secretKey), s3Config);

        var keyByFile = htmlFiles.ToDictionary(
            f => f,
            f =>
            {
                var k = Path.GetRelativePath(outDir, f).Replace('\\', '/');
                return k.EndsWith(".html", StringComparison.OrdinalIgnoreCase) ? k[..^".html".Length] : k;
            });

        var targetKeys = new HashSet<string>(keyByFile.Values, StringComparer.Ordinal);

        var deletedKeys = new List<string>();
        var listReq = new ListObjectsV2Request { BucketName = bucketName };
        ListObjectsV2Response listResp;
        do
        {
            listResp = await client.ListObjectsV2Async(listReq);
            var toDelete = listResp.S3Objects
                .Where(o => targetKeys.Contains(o.Key) ||
                            o.Key.EndsWith(".html", StringComparison.OrdinalIgnoreCase))
                .Select(o => new KeyVersion { Key = o.Key })
                .ToList();
            if (toDelete.Count > 0)
            {
                var deleteReq = new DeleteObjectsRequest
                {
                    BucketName = bucketName,
                    Objects = toDelete
                };
                var deleteResp = await client.DeleteObjectsAsync(deleteReq);
                deletedKeys.AddRange(deleteResp.DeletedObjects.Select(d => d.Key));
            }
            listReq.ContinuationToken = listResp.NextContinuationToken;
        } while (listResp.IsTruncated == true);

        var uploaded = new ConcurrentBag<string>();
        var failed = new ConcurrentBag<object>();

        await Parallel.ForEachAsync(htmlFiles, new ParallelOptions { MaxDegreeOfParallelism = 16 }, async (file, ct) =>
        {
            var key = keyByFile[file];
            try
            {
                using var stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
                var request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = key,
                    InputStream = stream,
                    ContentType = "text/html; charset=utf-8",
                    CannedACL = S3CannedACL.PublicRead,
                    AutoCloseStream = false
                };
                await client.PutObjectAsync(request, ct);
                uploaded.Add(key);
            }
            catch (Exception ex)
            {
                failed.Add(new { key, error = ex.Message });
            }
        });

        return Ok(new
        {
            bucket = bucketName,
            endpoint,
            deletedCount = deletedKeys.Count,
            uploadedCount = uploaded.Count,
            failedCount = failed.Count,
            uploaded,
            failed
        });
    }

    private static string OutputPathFor(string url)
    {
        var path = url.TrimEnd('/');
        if (path == "") return "index.html";
        if (path.Contains('.')) return path.TrimStart('/');
        return path.TrimStart('/') + ".html";
    }

    private static void CopyDirectory(string sourceDir, string targetDir)
    {
        if (!Directory.Exists(sourceDir)) return;
        foreach (var file in Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories))
        {
            if (Path.GetExtension(file).Equals(".scss", StringComparison.OrdinalIgnoreCase)) continue;
            var rel = Path.GetRelativePath(sourceDir, file);
            var dest = Path.Combine(targetDir, rel);
            Directory.CreateDirectory(Path.GetDirectoryName(dest)!);
            System.IO.File.Copy(file, dest, overwrite: true);
        }
    }
}
