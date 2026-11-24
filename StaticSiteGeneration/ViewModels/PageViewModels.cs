namespace NarshaStaticSite.ViewModels;

using NarshaStaticSite.Models;

public class LayoutViewModel
{
    public SeoMeta Seo { get; set; } = new("", "", "/");
    public string ActiveNav { get; set; } = "";
    public bool HomePage { get; set; }
    public bool IncludeCustomTailoringScript { get; set; }
}

public sealed class HomeViewModel : LayoutViewModel { }

public sealed class FaqPageViewModel : LayoutViewModel
{
    public List<FaqItem> Items { get; set; } = new();
}

public sealed class PaymentMethodsPageViewModel : LayoutViewModel { }

public sealed class CustomSewingPageViewModel : LayoutViewModel { }

public sealed class ContactUsPageViewModel : LayoutViewModel { }

public sealed class NotFoundPageViewModel : LayoutViewModel { }

public sealed class CategoryPageViewModel : LayoutViewModel
{
    public Category Category { get; set; } = new();
    public List<ProductCard> Products { get; set; } = new();
}

public sealed class ProductPageViewModel : LayoutViewModel
{
    public Category Category { get; set; } = new();
    public Product Product { get; set; } = new();
    public List<ProductCard> Related { get; set; } = new();
}
