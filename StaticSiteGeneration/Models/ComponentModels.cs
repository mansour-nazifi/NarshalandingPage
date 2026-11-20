namespace NarshaStaticSite.Models;

public sealed record ServiceItem(string Title, string Description, string IconSvg, string? Badge = null);

public sealed record PaymentOption(string Text);

public sealed record PaymentCardData(
    string Title,
    string IconSvg,
    IReadOnlyList<PaymentOption> Features,
    string Badge,
    bool Featured = false
);

public sealed record ServiceCardListData(IReadOnlyList<ServiceItem> Items, bool Numbered);

public sealed record LightboxItem(string Image, string Alt, string Category, string Name)
{
    public int Index { get; init; }
}

public sealed record PortfolioItemData(LightboxItem Item, int Index);

public sealed record SectionHeaderData(
    string Tag,
    string Title,
    string? Gold = null,
    string? TitleEnd = null,
    string? Description = null,
    string? TitleId = null
);