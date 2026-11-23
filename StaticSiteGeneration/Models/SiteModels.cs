using System.Globalization;

namespace NarshaStaticSite.Models;

public sealed record NavItem(string Title, string Href, bool IsActive = false);
public sealed record BreadcrumbItem(string Title, string? Href = null);
public sealed record PriceOption(string Id, string Name, long Price, bool Mandatory);
public sealed record InfoItem(string Title, string Text);
public sealed record ProcessStep(string Title, string Text);
public sealed record FaqItem(string Question, string AnswerHtml);
public sealed record ProductCard(string Title, string Slug, string Image, long BasePrice, string Alt, string Description = "");
public sealed record SeoMeta(
    string Title,
    string Description,
    string CanonicalPath,
    string? Keywords = null,
    string? OgType = null,
    string? OgImage = null,
    string? OgImageAlt = null,
    string? JsonLd = null
);

public sealed class Category
{
    public string Key { get; init; } = "";
    public string Slug { get; init; } = "";
    public string SingularName { get; init; } = "";
    public string PluralName { get; init; } = "";
    public string Image { get; init; } = "/images/hero.webp";
    public string HeroBadge { get; init; } = "";
    public string HeroTitleLine1 { get; init; } = "";
    public string HeroTitleLine2 { get; init; } = "";
    public string HeroSubtitle { get; init; } = "";
    public string ProductsTag { get; init; } = "مدل‌ها";
    public string ProductsTitle { get; init; } = "";
    public string ProductsTitleGold { get; init; } = "";
    public string ProductsDesc { get; init; } = "";
    public List<InfoItem> ImportantNotes { get; init; } = new();
    public List<ProcessStep> ProcessSteps { get; init; } = new();
    public string ProcessTitleGold { get; init; } = "";
    public List<FaqItem> Faqs { get; init; } = new();
    public string FaqTitleGold { get; init; } = "";
    public string CtaTitle { get; init; } = "";
    public string CtaDesc { get; init; } = "";
    public string SeoTitle { get; init; } = "";
    public string SeoDescription { get; init; } = "";
    public string SeoKeywords { get; init; } = "";
    public string OgImage { get; init; } = "https://narsha.ir/images/og-image.webp";
}

public sealed class Product
{
    public string Slug { get; init; } = "";
    public string CategorySlug { get; init; } = "";
    public string Title { get; init; } = "";
    public string CardTitle { get; init; } = "";
    public string CategoryName { get; init; } = "";
    public long BasePrice { get; init; }
    public string Image { get; init; } = "/images/hero.webp";
    public string HeroDescription { get; init; } = "";
    public string FeatureText { get; init; } = "";
    public string FitText { get; init; } = "";
    public string TimeText { get; init; } = "زمان آماده‌سازی پس از بررسی مدل، پارچه و جزئیات سفارش اعلام می‌شود.";
    public string MaterialsText { get; init; } = "پارچه، آستر (در صورت نیاز)، دکمه، زیپ، نخ و سایر خرج‌کارها بر عهده مشتری است.";
    public List<PriceOption> Options { get; init; } = new();
    public List<FaqItem> Faqs { get; init; } = new();
    public List<string> RelatedSlugs { get; init; } = new();
    public string DataProductName { get; init; } = "";

    public string BasePriceText => BasePrice.ToString("N0", CultureInfo.InvariantCulture);
}

public static class PriceFormat
{
    public static string Toman(long amount) => amount.ToString("N0", CultureInfo.InvariantCulture);
}
