namespace NarshaStaticSite.ViewComponents;

using Microsoft.AspNetCore.Mvc;
using NarshaStaticSite.Models;

public class FaqGridViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(IReadOnlyList<FaqItem> items)
    {
        return View(items);
    }
}