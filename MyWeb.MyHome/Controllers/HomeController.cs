using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWeb.MyHome.Models;
using System;
using System.Diagnostics;
using System.Security.Claims;

namespace MyWeb.MyHome.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TicketList()
        {
            string status = "In Progress";

            return View(TicketModel.GetList(status));
        }

        public IActionResult TicketChange([FromForm]TicketModel model)
        {
            model.Update();

            return Redirect("/home/ticketList"); //Json(new { msg = "Ok" });
        }

        public IActionResult BoardList(string search)
        {
            return View(BoardModel.GetList(search));
        }

        public IActionResult BoardWrite()
        {
            return View();
        }

        public IActionResult BoardWrite_Input(string title, string contents)
        {
            var model = new BoardModel();

            model.Title = title;
            model.Contents = contents;
            model.Reg_User = Convert.ToUInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            model.Reg_Username = User.Identity.Name;

            model.Insert();

            return Redirect("/home/boardlist");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
