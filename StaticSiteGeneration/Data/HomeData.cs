namespace NarshaStaticSite.Data;

using NarshaStaticSite.Models;

public static class HomeData
{
    // Service / card icons (extracted from inline SVG in Index.cshtml)
    public static class Icons
    {
        public const string TShirt = "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"48\" height=\"48\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"1.3\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"icon-gold\"><line x1=\"8\" y1=\"1.5\" x2=\"9.2\" y2=\"5.5\"></line><line x1=\"16\" y1=\"1.5\" x2=\"14.8\" y2=\"5.5\"></line><path d=\"M9.2 5.5 C8.5 5.5 8 6.2 8 7 L8.5 10.5 L3 23 L21 23 L15.5 10.5 L16 7 C16 6.2 15.5 5.5 14.8 5.5 Z\"></path><path d=\"M9.2 5.5 C10 7.5 11 7 12 6.2 C13 7 14 7.5 14.8 5.5\"></path><line x1=\"8.5\" y1=\"10.5\" x2=\"15.5\" y2=\"10.5\"></line><path d=\"M12 9.5 L12.8 10.5 L12 11.5 L11.2 10.5 Z\" fill=\"currentColor\"></path></svg>";
        public const string Coat = "<svg width=\"48\" height=\"48\" viewBox=\"0 0 64 64\" fill=\"none\" class=\"icon-gold\"><path d=\"M24 10 L18 18 L12 22 L16 28 L20 24 L22 56 H42 L44 24 L48 28 L52 22 L46 18 L40 10 Z\" stroke=\"currentColor\" stroke-width=\"3\" stroke-linejoin=\"round\" stroke-linecap=\"round\"/><path d=\"M28 10 L32 18 L36 10\" stroke=\"currentColor\" stroke-width=\"3\" stroke-linejoin=\"round\" stroke-linecap=\"round\"/><line x1=\"32\" y1=\"18\" x2=\"32\" y2=\"56\" stroke=\"currentColor\" stroke-width=\"2.5\" stroke-linecap=\"round\"/><circle cx=\"32\" cy=\"28\" r=\"2\" fill=\"currentColor\"/><circle cx=\"32\" cy=\"36\" r=\"2\" fill=\"currentColor\"/><circle cx=\"32\" cy=\"44\" r=\"2\" fill=\"currentColor\"/></svg>";
        public const string Scissors = "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"48\" height=\"48\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"1.5\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"icon-gold\"><path d=\"M13 7 8.7 2.7a2.41 2.41 0 0 0-3.4 0L2.7 5.3a2.41 2.41 0 0 0 0 3.4L7 13\"></path><path d=\"m8 6 2-2\"></path><path d=\"m18 16 2-2\"></path><path d=\"m17 11 4.3 4.3c.94.94.94 2.46 0 3.4l-2.6 2.6c-.94.94-2.46.94-3.4 0L11 17\"></path><path d=\"M21.174 6.812a1 1 0 0 0-3.986-3.987L3.842 16.174a2 2 0 0 0-.5.83l-1.321 4.352a.5.5 0 0 0 .623.622l4.353-1.32a2 2 0 0 0 .83-.497z\"></path><path d=\"m15 5 4 4\"></path></svg>";
        public const string Needle = "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"48\" height=\"48\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"1.5\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"icon-gold\"><path d=\"M21.3 15.3a2.4 2.4 0 0 1 0 3.4l-2.6 2.6a2.4 2.4 0 0 1-3.4 0L2.7 8.7a2.41 2.41 0 0 1 0-3.4l2.6-2.6a2.41 2.41 0 0 1 3.4 0Z\"></path><path d=\"m14.5 12.5 2-2\"></path><path d=\"m11.5 9.5 2-2\"></path><path d=\"m8.5 6.5 2-2\"></path><path d=\"m17.5 15.5 2-2\"></path></svg>";
        public const string Sparkle = "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"48\" height=\"48\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"1.5\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"icon-gold\"><path d=\"m21.64 3.64-1.28-1.28a1.21 1.21 0 0 0-1.72 0L2.36 18.64a1.21 1.21 0 0 0 0 1.72l1.28 1.28a1.2 1.2 0 0 0 1.72 0L21.64 5.36a1.2 1.2 0 0 0 0-1.72\"></path><path d=\"m14 7 3 3\"></path><path d=\"M5 6v4\"></path><path d=\"M19 14v4\"></path><path d=\"M10 2v2\"></path><path d=\"M7 8H3\"></path><path d=\"M21 16h-4\"></path><path d=\"M11 3H9\"></path></svg>";
        public const string Tools = "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"48\" height=\"48\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"1.5\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"icon-gold\"><circle cx=\"6\" cy=\"6\" r=\"3\"></circle><path d=\"M8.12 8.12 12 12\"></path><path d=\"M20 4 8.12 15.88\"></path><circle cx=\"6\" cy=\"18\" r=\"3\"></circle><path d=\"M14.8 14.8 20 20\"></path></svg>";
        public const string ShieldCheck = "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"48\" height=\"48\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"1.5\" class=\"icon-gold\"><path d=\"M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z\"></path><polyline points=\"9 12 11 14 15 10\" stroke-width=\"2\"></polyline></svg>";
        public const string Shirt2 = "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"48\" height=\"48\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"1.5\" class=\"icon-gold\"><path d=\"M6 3h12l4 5-10 13L2 8z\"></path><path d=\"M2 8h20\"></path></svg>";
        public const string Box = "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"48\" height=\"48\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"1.5\" class=\"icon-gold\"><path d=\"M21 8v8a2 2 0 0 1-1 1.73l-7 4a2 2 0 0 1-2 0l-7-4A2 2 0 0 1 3 16V8\"></path><path d=\"M22 6 12 1 2 6l10 5 10-5z\"></path></svg>";
        public const string Clock = "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"48\" height=\"48\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"1.5\" class=\"icon-gold\"><circle cx=\"12\" cy=\"12\" r=\"10\"></circle><path d=\"M12 6v6l4 2\"></path></svg>";

        // Payment icons
        public const string Cash = "<svg width=\"56\" height=\"56\" viewBox=\"0 0 64 64\" fill=\"none\" class=\"icon-gold\"><rect x=\"10\" y=\"18\" width=\"44\" height=\"28\" rx=\"4\" stroke=\"currentColor\" stroke-width=\"2\"/><path d=\"M16 18 V14 H42 C46 14 48 16 48 20\" stroke=\"currentColor\" stroke-width=\"2\"/><circle cx=\"44\" cy=\"32\" r=\"2\" fill=\"currentColor\"/><path d=\"M22 52 L28 58 L42 44\" stroke=\"currentColor\" stroke-width=\"2.5\" stroke-linecap=\"round\" stroke-linejoin=\"round\"/></svg>";
        public const string Installment = "<svg width=\"56\" height=\"56\" viewBox=\"0 0 64 64\" fill=\"none\" xmlns=\"http://www.w3.org/2000/svg\" class=\"icon-gold\"><rect x=\"12\" y=\"12\" width=\"40\" height=\"24\" rx=\"4\" stroke=\"currentColor\" stroke-width=\"2\"/><line x1=\"12\" y1=\"20\" x2=\"52\" y2=\"20\" stroke=\"currentColor\" stroke-width=\"2\"/><path d=\"M32 36 V46\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\"/><path d=\"M32 46 L20 54\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\"/><path d=\"M32 46 L44 54\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\"/><circle cx=\"20\" cy=\"54\" r=\"2.5\" fill=\"currentColor\"/><circle cx=\"32\" cy=\"54\" r=\"2.5\" fill=\"currentColor\"/><circle cx=\"44\" cy=\"54\" r=\"2.5\" fill=\"currentColor\"/></svg>";

        // UI icons
        public const string CheckCircle = "<svg width=\"18\" height=\"18\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"#22c55e\" stroke-width=\"2.5\" aria-hidden=\"true\" class=\"icon-check\"><polyline points=\"20 6 9 17 4 12\" /></svg>";
        public const string ZoomIn = "<svg width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" class=\"icon-zoom\"><circle cx=\"11\" cy=\"11\" r=\"8\" /><path d=\"M21 21l-4.35-4.35\" /><line x1=\"11\" y1=\"8\" x2=\"11\" y2=\"14\" /><line x1=\"8\" y1=\"11\" x2=\"14\" y2=\"11\" /></svg>";
    }

    public static List<ServiceItem> Services() => new()
    {
        new("دوخت لباس مجلسی و شب", "دوخت لباس‌های مجلسی و شب با تمرکز بر فرم اندام، ایستایی پارچه و جزئیات ظریف؛ مناسب مهمانی‌ها، جشن‌ها و مراسم‌های خاص.", Icons.TShirt),
        new("دوخت مانتو و پوشاک روزمره", "دوخت مانتو و پوشاک زنانه برای استفاده روزمره، اداری یا رسمی، با الگوی اختصاصی و تناسب متناسب با سبک پوشش شما.", Icons.Coat),
        new("دوخت لباس عروس و نامزدی", "طراحی و دوخت لباس عروس و نامزدی با نگاه ویژه به تناسب، راحتی و هماهنگی با فرم اندام و سلیقه شما در یکی از مهم‌ترین روزها.", Icons.Scissors),
        new("سایزگیری و فیتینگ تخصصی", "اندازه‌گیری دقیق و جلسات پرو برای رسیدن به فیت درست شانه، کمر، قد و خطوط اصلی لباس؛ عاملی کلیدی در زیبایی دوخت سفارشی.", Icons.Needle),
        new("مشاوره مدل و انتخاب پارچه", "راهنمایی در انتخاب مدل، رنگ و جنس پارچه متناسب با فرم اندام، فصل، کاربرد لباس و بودجه؛ برای تصمیمی آگاهانه‌تر پیش از دوخت.", Icons.Sparkle),
        new("طراحی و اجرای اختصاصی", "از ایده یا نمونه مرجع تا الگو و دوخت نهایی؛ اجرای جزئیاتی مانند یقه، آستین، برش و تزئینات بر اساس درخواست شما.", Icons.Tools),
    };

    public static List<ServiceItem> WhyChoose() => new()
    {
        new("دقت در برش و دوخت", "از تناسب خطوط اصلی لباس تا تمیزی دوخت و اتوکشی، جزئیات در کیفیت نهایی نقش تعیین‌کننده دارند.", Icons.ShieldCheck),
        new("انتخاب متناسب با شما", "مدل، پارچه و جزئیات اجرا بر اساس فرم اندام، کاربرد لباس و سلیقه شما پیشنهاد و نهایی می‌شود.", Icons.Shirt2),
        new("پذیرش سفارش حضوری و غیرحضوری", "علاوه بر مراجعه حضوری در اردبیل، امکان هماهنگی و پیگیری سفارش برای مشتریان سایر شهرها نیز فراهم است.", Icons.Box),
    };

    public static List<ServiceItem> ProcessSteps() => new()
    {
        new("مشاوره و انتخاب مدل", "در جلسه حضوری یا از طریق پیام، مدل، کاربرد لباس، پارچه و جزئیات موردنظر بررسی و پیشنهادهای متناسب ارائه می‌شود.", ""),
        new("سایزگیری و برش", "اندازه‌گیری دقیق انجام و الگوی لباس متناسب با فرم بدن شما آماده می‌شود. برای سفارش‌های غیرحضوری، راهنمای اندازه‌گیری ارائه می‌گردد.", ""),
        new("دوخت، پرو و تحویل", "پس از دوخت اولیه، در صورت نیاز پرو و اصلاحات لازم انجام می‌شود تا فیت نهایی مناسب باشد و لباس آماده تحویل گردد.", ""),
    };

    public static List<PaymentCardData> PaymentCards() => new()
    {
        new(
            "پرداخت نقدی",
            Icons.Cash,
            new List<PaymentOption>
            {
                new("۵٪ تخفیف روی مبلغ نهایی سفارش"),
                new("ثبت و آغاز سریع‌تر فرآیند دوخت"),
                new("اولویت در زمان‌بندی و برنامه‌ریزی سفارش"),
                new("پیگیری و اصلاحات پس از تحویل طبق توافق"),
            },
            "همراه با تخفیف نقدی",
            Featured: false
        ),
        new(
            "پرداخت اقساطی",
            Icons.Installment,
            new List<PaymentOption>
            {
                new("امکان تسویه در ۲ تا ۴ مرحله"),
                new("شروع سفارش با پیش‌پرداخت اولیه"),
                new("شرایط شفاف و هماهنگ‌شده با نوع سفارش"),
                new("بدون هزینه یا بهره اضافی (طبق شرایط اعلام‌شده)"),
            },
            "پرداخت مرحله‌ای",
            Featured: true
        ),
    };

    public static List<LightboxItem> PortfolioItems() => new()
    {
        new("/images/hero.webp", "نمونه لباس مجلسی زنانه دوخته‌شده به‌صورت سفارشی در مزون نارشا اردبیل", "لباس مجلسی", "کت و لباس مجلسی سفارشی") { Index = 0 },
        new("/images/hero-2.webp", "نمونه لباس مجلسی زنانه با دوخت اختصاصی و فیت دقیق در مزون نارشا", "لباس مجلسی", "مانتو و لباس مجلسی سفارشی") { Index = 1 },
        new("/images/portfolio-2.webp", "نمونه دوخت سفارشی لباس زنانه با تمرکز بر تناسب اندام و جزئیات دوخت", "لباس مجلسی", "دوخت سفارشی لباس زنانه") { Index = 2 },
    };
}