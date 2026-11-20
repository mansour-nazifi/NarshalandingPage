namespace NarshaStaticSite.ViewComponents;

using Microsoft.AspNetCore.Mvc;
using NarshaStaticSite.Models;

public class ServiceCardListViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(IReadOnlyList<ServiceItem> items, bool numbered = false)
    {
        if (items == null) items = new List<ServiceItem>();
        return View(new ServiceCardListData(items, numbered));
    }
}