namespace NarshaStaticSite.ViewComponents;

using Microsoft.AspNetCore.Mvc;
using NarshaStaticSite.Models;

public class InfoListViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(IReadOnlyList<InfoItem> items)
    {
        return View(items);
    }
}