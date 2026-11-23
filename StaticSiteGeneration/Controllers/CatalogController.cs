using Microsoft.AspNetCore.Mvc;
using NarshaStaticSite.Data;
using NarshaStaticSite.Models;
using NarshaStaticSite.ViewModels;
using System.Text.Json;

namespace NarshaStaticSite.Controllers;

public class CatalogController : Controller
{
    [HttpGet("{slug}")]
    public IActionResult Page(string slug)
    {
        var cat = SiteData.GetCategory(slug);
        if (cat is not null)
        {
            var products = SiteData.CategoryProducts(slug).Select(SiteData.ToCard).ToList();
            var seo = BuildCategorySeo(cat, products);
            return View("Category", new CategoryPageViewModel { Seo = seo, Category = cat, Products = products, ActiveNav = "/" + slug });
        }

        var p = SiteData.GetProduct(slug);
        if (p is not null)
        {
            var pcat = SiteData.GetCategory(p.CategorySlug)!;
            var seo = BuildProductSeo(pcat, p);
            return View("Product", new ProductPageViewModel { Seo = seo, Category = pcat, Product = p, Related = SiteData.RelatedCards(p), ActiveNav = "/" + pcat.Slug, IncludeCustomTailoringScript = true });
        }

        return NotFound();
    }

    private static SeoMeta BuildCategorySeo(Category c, List<ProductCard> products)
    {
        var title = c.SeoTitle.Length > 0 ? c.SeoTitle : "مزون نارشا";
        var desc = c.SeoDescription.Length > 0 ? c.SeoDescription : c.HeroSubtitle;
        var keywords = c.SeoKeywords.Length > 0 ? c.SeoKeywords : null;
        var canonical = "/" + c.Slug;
        var items = string.Join(",", products.Select((p,i)=> $"{{\"@type\":\"ListItem\",\"position\":{i+1},\"name\":{J(p.Title)},\"url\":{J(SiteConfig.Domain+"/"+p.Slug)}}}"));
        var coll = $"<script type=\"application/ld+json\">{{\"@context\":\"https://schema.org\",\"@type\":\"CollectionPage\",\"name\":{J(title)},\"description\":{J(desc)},\"url\":{J(SiteConfig.Domain+canonical)},\"inLanguage\":\"fa-IR\",\"isPartOf\":{{\"@type\":\"WebSite\",\"name\":\"مزون نارشا\",\"url\":\"{SiteConfig.Domain}\"}},\"mainEntity\":{{\"@type\":\"ItemList\",\"numberOfItems\":{products.Count},\"itemListElement\":[{items}]}},\"provider\":{{\"@id\":\"{SiteConfig.Domain}/#business\"}}}}</script>";
        var bc = BreadcrumbLd(("صفحه اصلی",""), (c.SingularName, canonical));
        return new SeoMeta(title, desc, canonical, keywords, "website", c.OgImage, title, coll+"\n"+bc);
    }

    private static SeoMeta BuildProductSeo(Category c, Product p)
    {
        var title = $"دوخت {p.Title} سفارشی در اردبیل | مزون نارشا";
        var desc = $"سفارش دوخت {p.Title} در مزون نارشا اردبیل با الگوی اختصاصی و دوخت تمیز. " + (p.Options.Count>0 ? "امکان انتخاب گزینه‌های مربوطه در محاسبه هزینه. " : "قیمت پایه دوخت مشخص و بدون گزینه اضافه. ") + "پارچه و ملزومات با مشتری است.";
        var canonical = "/" + p.Slug;
        var optDesc = BuildOptionDesc(p);
        var fullDesc = p.HeroDescription + " " + optDesc;
        var offerDesc = p.Options.Count>0 ? "قیمت پایه دوخت؛ گزینه‌های انتخابی جداگانه محاسبه می‌شوند." : "قیمت پایه دوخت.";
        var productObj = new Dictionary<string, object?>
        {
            ["@context"] = "https://schema.org",
            ["@type"] = "Product",
            ["name"] = p.Title,
            ["description"] = fullDesc,
            ["image"] = SiteConfig.Domain + p.Image,
            ["sku"] = p.Slug,
            ["category"] = c.SingularName,
            ["brand"] = new Dictionary<string, object?> { ["@type"] = "Brand", ["name"] = SiteConfig.SiteName },
            ["provider"] = new Dictionary<string, object?> { ["@id"] = SiteConfig.Domain + "/#business" },
            ["offers"] = new Dictionary<string, object?>
            {
                ["@type"] = "Offer",
                ["price"] = p.BasePrice.ToString(),
                ["priceCurrency"] = "IRR",
                ["availability"] = "https://schema.org/InStock",
                ["url"] = SiteConfig.Domain + canonical,
                ["description"] = offerDesc
            },
            ["areaServed"] = "IR"
        };
        var productJson = JsonSerializer.Serialize(productObj);
        var bc = BreadcrumbLd(("صفحه اصلی",""), (c.SingularName, "/"+c.Slug), (p.Title, canonical));
        return new SeoMeta(title, desc, canonical, null, "website", SiteConfig.OgImageUrl, $"{p.Title} دوخت سفارشی در مزون نارشا اردبیل", $"<script type=\"application/ld+json\">{productJson}</script>\n"+bc);
    }

    private static string BuildOptionDesc(Product p)
    {
        if (p.Options.Count==0) return "این مدل گزینه اضافه‌ای برای محاسبه هزینه ندارد و قیمت بر اساس دستمزد پایه دوخت محاسبه می‌شود.";
        var o = p.Options[0];
        if (o.Mandatory) return $"{o.Name} در این مدل اجباری است و در هزینه پایه دوخت لحاظ شده است. جزئیات در بخش محاسبه هزینه قابل مشاهده است.";
        return $"{o.Name} به‌صورت اختیاری با هزینه {PriceFormat.Toman(o.Price)} تومان قابل انتخاب است. جزئیات در بخش محاسبه هزینه قابل مشاهده است.";
    }

    private static string BreadcrumbLd(params (string name,string url)[] items)
    {
        var list = string.Join(",", items.Select((it,i)=> $"{{\"@type\":\"ListItem\",\"position\":{i+1},\"name\":{J(it.name)},\"item\":{J(SiteConfig.Domain+it.url)}}}"));
        return $"<script type=\"application/ld+json\">{{\"@context\":\"https://schema.org\",\"@type\":\"BreadcrumbList\",\"itemListElement\":[{list}]}}</script>";
    }

    private static string J(string s) => JsonSerializer.Serialize(s);
}