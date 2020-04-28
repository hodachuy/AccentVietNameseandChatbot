using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BotProject.Service;

namespace BotProject.Web.Controllers
{
    public class LcSettingController : BaseController
    {
        public LcSettingController(IErrorService errorService) : base(errorService)
        {
        }

        // GET: LcSetting
        public ActionResult Index()
        {
            return View();
        }
    }
}