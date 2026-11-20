namespace NarshaStaticSite.ViewComponents;

using Microsoft.AspNetCore.Mvc;
using NarshaStaticSite.Models;

public class PaymentGridViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(IReadOnlyList<PaymentCardData> cards)
    {
        return View(cards);
    }
}