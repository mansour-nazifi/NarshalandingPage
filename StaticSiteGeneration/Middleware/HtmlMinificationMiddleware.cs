using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WebMarkupMin.Core;

namespace NarshaStaticSite.Middleware;

public class HtmlMinificationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly HtmlMinifier _minifier = new();

    public HtmlMinificationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {         
        var shouldMinify = context.Request.Headers.ContainsKey("X-Static-Generation");
        
        if (!shouldMinify)
        {
            await _next(context);
            return;
        }

        var originalBody = context.Response.Body;
        using var minifiedStream = new MemoryStream();
        context.Response.Body = minifiedStream;

        await _next(context);

        context.Response.Body = originalBody;

        if (IsHtml(context.Response.ContentType))
        {
            minifiedStream.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(minifiedStream);
            var html = await reader.ReadToEndAsync();
            var result = _minifier.Minify(html);
            var minified = result.MinifiedContent ?? html;
            var bytes = Encoding.UTF8.GetBytes(minified);
            context.Response.ContentLength = bytes.Length;
            await originalBody.WriteAsync(bytes, 0, bytes.Length);
        }
        else
        {
            minifiedStream.Seek(0, SeekOrigin.Begin);
            await minifiedStream.CopyToAsync(originalBody);
        }
    }

    private static bool IsHtml(string? contentType)
    {
        return !string.IsNullOrEmpty(contentType) &&
               contentType.Contains("text/html", StringComparison.OrdinalIgnoreCase);
    }
}
