namespace NarshaStaticSite.Data;

using NarshaStaticSite.Models;

public static class ProductFaqFactory
{
    public static List<FaqItem> For(Product p)
    {
        if (p.Faqs.Count > 0) return p.Faqs;
        var priceOptText = OptionDescription(p);
        return new List<FaqItem>
        {
            new($"هزینه پایه دوخت {p.Title} چقدر است؟", $"هزینه پایه دوخت {p.Title} {PriceFormat.Toman(p.BasePrice)} تومان است. {priceOptText} قیمت‌های درج‌شده شامل پارچه، آستر و ملزومات نمی‌شوند و هزینه نهایی پس از بررسی سفارش اعلام می‌شود."),
            new("پارچه و ملزومات چگونه تأمین می‌شود؟", "پارچه اصلی، آستر (در صورت نیاز یا انتخاب گزینه مربوطه)، دکمه، زیپ، نخ و سایر خرج‌کارها توسط مشتری تهیه می‌شود. مزون نارشا برش، دوخت و کنترل کیفیت را انجام می‌دهد."),
            new("زمان آماده‌سازی سفارش چقدر است؟", $"زمان آماده‌سازی بسته به مدل، نوع پارچه، جزئیات دوخت و برنامه کاری مزون متفاوت است. {p.TimeText} زمان دقیق در مرحله مشاوره هماهنگ می‌شود."),
            new($"چگونه می‌توانم {p.Title} را سفارش دهم؟", "برای ثبت سفارش، تصویر پارچه، سایزها و جزئیات موردنظر را از طریق <a href=\"https://ble.ir/narsha_ir\" target=\"_blank\" rel=\"noopener noreferrer\"><strong>بله</strong></a> یا تماس با <a href=\"tel:+989149529879\"><strong>۰۹۱۴۹۵۲۹۸۷۹</strong></a> ارسال کنید تا شرایط اجرا و هزینه نهایی بررسی شود."),
        };
    }

    public static string OptionDescription(Product p)
    {
        if (p.Options.Count == 0) return "این مدل گزینه اضافه‌ای برای محاسبه هزینه ندارد و قیمت بر اساس دستمزد پایه دوخت محاسبه می‌شود.";
        var o = p.Options[0];
        if (o.Mandatory) return $"{o.Name} در این مدل اجباری است و در هزینه پایه دوخت لحاظ شده است. جزئیات در بخش محاسبه هزینه قابل مشاهده است.";
        return $"{o.Name} به‌صورت اختیاری با هزینه {PriceFormat.Toman(o.Price)} تومان قابل انتخاب است. جزئیات در بخش محاسبه هزینه قابل مشاهده است.";
    }
}