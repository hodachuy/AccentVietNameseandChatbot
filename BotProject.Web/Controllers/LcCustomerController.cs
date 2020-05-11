using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BotProject.Service;

namespace BotProject.Web.Controllers
{
    public class LcCustomerController : BaseController
    {
        public LcCustomerController(IErrorService errorService) : base(errorService)
        {
        }
        // GET: LcCustomer
        public ActionResult Index()
        {
            ViewBag.BotID = UserInfo.BotActiveID;
            return View();
        }
    }
}