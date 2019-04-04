using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BotProject.Service;

namespace BotProject.Web.Controllers
{
    public class ModuleController : BaseController
    {
        public ModuleController(IErrorService errorService) : base(errorService)
        {
        }

        // GET: Module
        public ActionResult Search()
        {
            return View();
        }
    }
}