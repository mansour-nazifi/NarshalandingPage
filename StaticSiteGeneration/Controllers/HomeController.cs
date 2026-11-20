using Microsoft.AspNetCore.Mvc;
using NarshaStaticSite.Data;
using NarshaStaticSite.Models;
using NarshaStaticSite.ViewModels;

namespace NarshaStaticSite.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var seo = new SeoMeta(
            "مزون نارشا اردبیل | دوخت سفارشی لباس زنانه، عروس، مجلسی و مانتو",
            "مزون نارشا در اردبیل، تخصص در دوخت سفارشی لباس زنانه شامل لباس عروس، مجلسی، مانتو، کت، پالتو و پوشاک روزمره. سایزگیری دقیق، مشاوره انتخاب مدل و پارچه، پرداخت نقدی و اقساطی. تماس: " + SiteContact.PhoneDisplayFa,
            SiteRoutes.Home,
            "مزون نارشا, مزون لباس زنانه اردبیل, دوخت سفارشی اردبیل, خیاطی زنانه اردبیل, لباس عروس اردبیل, لباس مجلسی اردبیل, مانتو سفارشی اردبیل, دوخت کت و پالتو زنانه, مزون نارشا اردبیل",
            "website",
            SiteConfig.OgImageUrl,
            "نمونه آثار دوخت سفارشی مزون نارشا در اردبیل",
            HomeJsonLd()
        );
        return View(new HomeViewModel { Seo = seo, HomePage = true });
    }

    [HttpGet(SiteRoutes.FaqSegment)]
    public IActionResult Faq()
    {
        var items = SiteData.GlobalFaq();
        var json = FaqJsonLd(items);
        var seo = new SeoMeta(
            "پرسش‌های متداول دوخت سفارشی | مزون نارشا اردبیل",
            "پاسخ به پرسش‌های متداول درباره دوخت سفارشی در مزون نارشا اردبیل: زمان آماده‌سازی، هزینه دوخت، سایزگیری حضوری و غیرحضوری، پارچه، شرایط پرداخت، اصلاحات و ارسال سفارش.",
            SiteRoutes.Faq,
            "پرسش‌های متداول مزون نارشا, سوالات دوخت سفارشی, مزون نارشا اردبیل, هزینه دوخت, زمان دوخت",
            "website", SiteConfig.OgImageUrl, null, json);
        return View(new FaqPageViewModel { Seo = seo, Items = items });
    }

    private static string HomeJsonLd() =>
        "<script type=\"application/ld+json\">{\"@context\":\"https://schema.org\",\"@type\":\"WebSite\",\"@id\":\"" + SiteConfig.Domain + "/#website\",\"url\":\"" + SiteConfig.Domain + "/\",\"name\":\"مزون نارشا\",\"alternateName\":\"Narsha Atelier\",\"inLanguage\":\"fa-IR\"}</script>\n" +
        "<script type=\"application/ld+json\">{\"@context\":\"https://schema.org\",\"@type\":\"LocalBusiness\",\"@id\":\"" + SiteConfig.Domain + "/#business\",\"name\":\"مزون نارشا\",\"alternateName\":\"مزون نارشا اردبیل\",\"description\":\"مزون نارشا در اردبیل، ارائه‌دهنده خدمات دوخت سفارشی و طراحی اختصاصی لباس زنانه شامل لباس عروس، لباس مجلسی و شب، مانتو، کت، پالتو، شومیز، دامن و شلوار با سایزگیری و فیتینگ تخصصی.\",\"url\":\"" + SiteConfig.Domain + "/\",\"telephone\":\"" + SiteContact.Phone + "\",\"priceRange\":\"$$\",\"image\":\"" + SiteConfig.OgImageUrl + "\",\"address\":{\"@type\":\"PostalAddress\",\"streetAddress\":\"بلوار طلائیه، شهدا، پیام شرقی 3 (شهرک کشاورزی)\",\"addressLocality\":\"اردبیل\",\"addressRegion\":\"اردبیل\",\"addressCountry\":\"IR\"},\"geo\":{\"@type\":\"GeoCoordinates\",\"latitude\":\"38.210618\",\"longitude\":\"48.305364\"},\"openingHoursSpecification\":[{\"@type\":\"OpeningHoursSpecification\",\"dayOfWeek\":[\"Saturday\",\"Sunday\",\"Monday\",\"Tuesday\",\"Wednesday\",\"Thursday\"],\"opens\":\"10:00\",\"closes\":\"20:00\"}],\"areaServed\":[{\"@type\":\"City\",\"name\":\"اردبیل\"},{\"@type\":\"Country\",\"name\":\"ایران\"}],\"sameAs\":[\"" + SiteContact.InstagramUrl + "\",\"" + SiteContact.BaleUrl + "\"]}</script>\n" +
        "<script type=\"application/ld+json\">{\"@context\":\"https://schema.org\",\"@type\":\"BreadcrumbList\",\"itemListElement\":[{\"@type\":\"ListItem\",\"position\":1,\"name\":\"صفحه اصلی\",\"item\":\"" + SiteConfig.Domain + "/\"}]}</script>";

    [HttpGet(SiteRoutes.PaymentMethodsSegment)]
    public IActionResult PaymentMethods()
    {
        var seo = new SeoMeta(
            "روش‌های پرداخت | مزون نارشا اردبیل",
            "روش‌های پرداخت در مزون نارشا اردبیل: امکان تسویه نقدی با تخفیف یا پرداخت اقساطی مرحله‌ای. برای جزئیات نهایی با مزون در تماس باشید.",
            SiteRoutes.PaymentMethods,
            "روش‌های پرداخت مزون نارشا, پرداخت اقساطی مزون, پرداخت نقدی مزون, مزون نارشا اردبیل",
            "website", SiteConfig.OgImageUrl, null, PaymentMethodsJsonLd());
        return View(new PaymentMethodsPageViewModel { Seo = seo, ActiveNav = SiteRoutes.PaymentMethods });
    }

    private static string PaymentMethodsJsonLd() => BreadcrumbJsonLd("روش‌های پرداخت", SiteRoutes.PaymentMethods);

    [HttpGet(SiteRoutes.CustomSewingSegment)]
    public IActionResult CustomSewing()
    {
        var items = SiteData.GlobalFaq();
        var json = FaqJsonLd(items);
        var seo = new SeoMeta(
            "دوخت سفارشی لباس زنانه | مزون نارشا اردبیل",
            "راهنمای دوخت سفارشی در مزون نارشا اردبیل: نکات انتخاب پارچه و تناسب، رویکرد مزون در دوخت سفارشی، و روند مرحله‌ای ثبت و اجرای سفارش. پاسخ به سوالات متداول درباره زمان، هزینه، پارچه و شرایط دوخت.",
            SiteRoutes.CustomSewing,
            "دوخت سفارشی مزون نارشا, راهنمای دوخت سفارشی, مزون نارشا اردبیل, دوخت لباس زنانه سفارشی, سوالات متخصص دوخت سفارشی",
            "website", SiteConfig.OgImageUrl, null, json);
        return View(new CustomSewingPageViewModel { Seo = seo, ActiveNav = SiteRoutes.CustomSewing });
    }

    private static string CustomSewingJsonLd() => BreadcrumbJsonLd("دوخت سفارشی", SiteRoutes.CustomSewing);

    [HttpGet(SiteRoutes.ContactUsSegment)]
    public IActionResult ContactUs()
    {
        var seo = new SeoMeta(
            "تماس با ما | مزون نارشا اردبیل",
            "تماس با مزون نارشا در اردبیل: مشاوره و ثبت سفارش دوخت سفارشی لباس زنانه از طریق تماس تلفنی، پیام در بله، اینستاگرام یا مراجعه حضوری.",
            SiteRoutes.ContactUs,
            "تماس با مزون نارشا, آدرس مزون نارشا, شماره مزون نارشا, مزون نارشا اردبیل",
            "website", SiteConfig.OgImageUrl, null, ContactUsJsonLd());
        return View(new ContactUsPageViewModel { Seo = seo, ActiveNav = SiteRoutes.ContactUs });
    }

    private static string ContactUsJsonLd() => BreadcrumbJsonLd("تماس با ما", SiteRoutes.ContactUs);

    private static string BreadcrumbJsonLd(string name, string path) =>
        "<script type=\"application/ld+json\">{\"@context\":\"https://schema.org\",\"@type\":\"BreadcrumbList\",\"itemListElement\":[{\"@type\":\"ListItem\",\"position\":1,\"name\":\"صفحه اصلی\",\"item\":\"" + SiteConfig.Domain + "/\"},{\"@type\":\"ListItem\",\"position\":2,\"name\":\"" + name + "\",\"item\":\"" + SiteConfig.Domain + path + "\"}]}</script>";

    private static string FaqJsonLd(List<FaqItem> items)
    {
        var ents = string.Join(",", items.Select(i => "{\"@type\":\"Question\",\"name\":\"" + Esc(i.Question) + "\",\"acceptedAnswer\":{\"@type\":\"Answer\",\"text\":\"" + Esc(Strip(i.AnswerHtml)) + "\"}}"));
        var crumbs = "{\"@context\":\"https://schema.org\",\"@type\":\"BreadcrumbList\",\"itemListElement\":[{\"@type\":\"ListItem\",\"position\":1,\"name\":\"صفحه اصلی\",\"item\":\"" + SiteConfig.Domain + "/\"},{\"@type\":\"ListItem\",\"position\":2,\"name\":\"پرسش‌های متداول\",\"item\":\"" + SiteConfig.Domain + SiteRoutes.Faq + "\"}]}";
        return $"<script type=\"application/ld+json\">{{\"@context\":\"https://schema.org\",\"@type\":\"FAQPage\",\"mainEntity\":[{ents}]}}</script>\n<script type=\"application/ld+json\">{crumbs}</script>";
    }
    private static string Esc(string s) => System.Text.Json.JsonEncodedText.Encode(s).ToString();
    private static string Strip(string h) => System.Text.RegularExpressions.Regex.Replace(h, "<.*?>", "");
}
