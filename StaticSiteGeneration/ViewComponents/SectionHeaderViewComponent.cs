namespace NarshaStaticSite.ViewComponents;

using Microsoft.AspNetCore.Mvc;
using NarshaStaticSite.Models;

public class SectionHeaderViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(SectionHeaderData data)
    {
        return View(data);
    }
}