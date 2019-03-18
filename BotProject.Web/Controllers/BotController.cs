using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BotProject.Service;

namespace BotProject.Web.Controllers
{
    public class BotController : BaseController
    {
        public BotController(IErrorService errorService) : base(errorService)
        {
        }

        // GET: Bot
        public ActionResult Index()
        {
            return View();
        }     

		public ActionResult QnA(int id)
		{
            ViewBag.BotID = id;
            return View();
		}

		public ActionResult CardCategory(int id) {
            ViewBag.BotID = id;
			return View();
		}

		public ActionResult AIML(int id)
		{
            ViewBag.BotID = id;
            return View();
		}
    }
}