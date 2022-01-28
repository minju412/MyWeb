using Microsoft.AspNetCore.Mvc;

namespace MyWeb.MyHome.Controllers.ViewComponents
{
    public class LeftMenu : ViewComponent
    {
        public LeftMenu()
        {

        }
        
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
