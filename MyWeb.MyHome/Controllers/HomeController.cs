using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public IActionResult BoardWrite()
        {
            return View();
        }

        public IActionResult BoardView(uint idx)
        {
            return View(BoardModel.Get(idx));
        }

        //[Authorize(Roles = "Admin")]
        [Authorize]
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

        [Authorize]
        public IActionResult BoardEdit(uint idx, string type)
        {
            var model = BoardModel.Get(idx);
            var userSeq= Convert.ToUInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (model.Reg_User != userSeq)
            {
                throw new Exception("수정할 수 없습니다.");
            }
            if (type == "U")
            {
                return View(model);
            }
            else if (type == "D")
            {
                model.Delete();
                return Redirect("/home/boardList");
            }
            throw new Exception("잘못된 요청입니다");
        }

        [Authorize]
        public IActionResult BoardEdit_Input(uint idx, string title, string contents)
        {
            var model = BoardModel.Get(idx);
            var userSeq = Convert.ToUInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (model.Reg_User != userSeq)
            {
                throw new Exception("수정할 수 없습니다.");
            }

            model.Title = title;
            model.Contents = contents;

            model.Update();

            return Redirect("/home/boardlist");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
