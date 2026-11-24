using Microsoft.AspNetCore.Mvc;
using NarshaStaticSite.Data;
using System.Net;
using System.Text;

namespace NarshaStaticSite.Controllers;

public class StaticSiteGenerationController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IWebHostEnvironment _env;

    public StaticSiteGenerationController(IHttpClientFactory httpClientFactory, IWebHostEnvironment env)
    {
        _httpClientFactory = httpClientFactory;
        _env = env;
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
