namespace NarshaStaticSite.ViewComponents;

using Microsoft.AspNetCore.Mvc;
using NarshaStaticSite.Models;

public class BreadcrumbViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(List<BreadcrumbItem> items)
    {
        return View(items);
    }
}
