using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyWeb.MyHome.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public AdminController()
        {
        }

        [Authorize(Roles ="ADMIN")]
        public IActionResult GetCheck()
        {
            //if (User.IsInRole("ADMIN"))
            //{
                return Json(new { a = 9 });
            //}

            //return Json(new { a = 1 });
        }
        
        [AllowAnonymous] //로그인 되지 않은 익명사용자 받음
        public IActionResult GetUserCheck()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Json(new { a = 9 });
            }

            return Json(new { a = 1 });
        }
    }
}
