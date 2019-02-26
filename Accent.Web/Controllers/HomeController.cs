using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Accent.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QnA()
        {
            return View();
        }
        public ActionResult AccentVN()
        {
            return View();
        }
        public ActionResult BotLearning()
        {
            return View();
        }
        public ActionResult BubleChatBot()
        {
            return View();
        }
        public ActionResult FormChatBot()
        {
            return View();
        }
    }
}