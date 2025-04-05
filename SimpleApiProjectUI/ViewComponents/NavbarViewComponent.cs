using Microsoft.AspNetCore.Mvc;

namespace SimpleApiProjectUI.ViewComponents
{
    public class NavbarViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
