namespace NarshaStaticSite.ViewComponents;

using Microsoft.AspNetCore.Mvc;
using NarshaStaticSite.Models;

public class PortfolioItemViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(LightboxItem item, int index)
    {
        return View(new PortfolioItemData(item, index));
    }
}