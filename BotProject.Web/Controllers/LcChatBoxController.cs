using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BotProject.Web.Controllers
{
    public class LcChatBoxController : Controller
    {
        // GET: LcOpenChat
        public ActionResult Index()
        {
            // Tạo chuỗi ID định danh cho customer
            ViewBag.ConnectionID = Guid.NewGuid().ToString();
            return View();
        }
    }
}