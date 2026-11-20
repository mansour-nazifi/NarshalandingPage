namespace NarshaStaticSite.ViewComponents;

using Microsoft.AspNetCore.Mvc;

public class CustomSewingCategoryListViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}