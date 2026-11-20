using Microsoft.AspNetCore.Mvc;
using NarshaStaticSite.Data;
using System.Xml.Linq;

namespace NarshaStaticSite.Controllers;

public class SitemapController : Controller
{
    [HttpGet("sitemap.xml")]
    public IActionResult Sitemap()
    {
        var ns = XNamespace.Get("http://www.sitemaps.org/schemas/sitemap/0.9");
        var now = DateTimeOffset.UtcNow.ToString("yyyy-MM-dd");
        var domain = SiteData.Domain;

        var urls = new List<XElement>
        {
            SitemapUrl(ns, domain + "/", now, "weekly", "1.0"),
            SitemapUrl(ns, domain + "/faq", now, "monthly", "0.8"),
        };

        foreach (var c in SiteData.Categories())
            urls.Add(SitemapUrl(ns, domain + "/" + c.Slug, now, "weekly", "0.9"));

        foreach (var p in SiteData.Products())
            urls.Add(SitemapUrl(ns, domain + "/" + p.Slug, now, "monthly", "0.8"));

        var doc = new XDocument(
            new XDeclaration("1.0", "utf-8", "yes"),
            new XElement(ns + "urlset", urls));

        return Content(doc.ToString(SaveOptions.DisableFormatting), "application/xml; charset=utf-8");
    }

    [HttpGet("robots.txt")]
    public IActionResult Robots()
    {
        var text = "User-agent: *\nAllow: /\n\nSitemap: https://narsha.ir/sitemap.xml\n";
        return Content(text, "text/plain; charset=utf-8");
    }

    private static XElement SitemapUrl(XNamespace ns, string loc, string lastmod, string changefreq, string priority) =>
        new XElement(ns + "url",
            new XElement(ns + "loc", loc),
            new XElement(ns + "lastmod", lastmod),
            new XElement(ns + "changefreq", changefreq),
            new XElement(ns + "priority", priority));
}
