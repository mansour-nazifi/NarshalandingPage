namespace NarshaStaticSite.Data;

public static class SiteRoutes
{
    public const string Home = "/";
    public const string Faq = "/faq";
    public const string PaymentMethods = "/payment-methods";
    public const string CustomSewing = "/custom-sewing";
    public const string ContactUs = "/contact-us";
    public const string Sitemap = "/sitemap.xml";
    public const string Robots = "/robots.txt";

    // Route template segments (for [HttpGet] attributes)
    public const string FaqSegment = "faq";
    public const string PaymentMethodsSegment = "payment-methods";
    public const string CustomSewingSegment = "custom-sewing";
    public const string ContactUsSegment = "contact-us";
}

public static class SiteContact
{
    public const string BaleUrl = "https://ble.ir/narsha_ir";
    public const string InstagramUrl = "https://instagram.com/narsha.ir";
    public const string InstagramHandle = "@narsha.ir";
    public const string Phone = "+989149529879";
    public const string PhoneDisplay = "0914-952-9879";
    public const string PhoneDisplayFa = "۰۹۱۴۹۵۲۹۸۷۹";
    public const string TelHref = "tel:+989149529879";
    public const string Address = "اردبیل، بلوار طلائیه، شهدا، پیام شرقی ۳ (شهرک کشاورزی)";
    public const string Hours = "شنبه تا پنجشنبه — ۱۰:۰۰ تا ۲۰:۰۰";
    public const string MapEmbedUrl = "https://neshan.org/maps/iframe/places/_bCgxV25aTbZ#c38.211-48.305-21z-0p/38.210618080723066/48.30536459611554";
}

public static class SiteConfig
{
    public const string SiteName = "مزون نارشا";
    public const string Domain = "https://narsha.ir";
    public const string OgImagePath = "/images/og-image.webp";
    public static string OgImageUrl => Domain + OgImagePath;
    public const string LogoPath = "/images/logo.webp";
}
