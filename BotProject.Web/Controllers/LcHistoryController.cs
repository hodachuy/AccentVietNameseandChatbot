using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BotProject.Service;

namespace BotProject.Web.Controllers
{
    public class LcHistoryController : BaseController
    {
        public LcHistoryController(IErrorService errorService) : base(errorService)
        {
        }

        // GET: LcHistory
        public ActionResult Index()
        {
            ViewBag.BotID = UserInfo.BotActiveID;
            return View();
        }
    }
}