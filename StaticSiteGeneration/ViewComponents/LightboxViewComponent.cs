namespace NarshaStaticSite.ViewComponents;

using Microsoft.AspNetCore.Mvc;
using NarshaStaticSite.Models;

public class LightboxViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(IReadOnlyList<LightboxItem> items)
    {
        return View(items);
    }
}