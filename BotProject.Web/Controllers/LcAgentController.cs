using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BotProject.Service;

namespace BotProject.Web.Controllers
{
    public class LcAgentController : BaseController
    {
        public LcAgentController(IErrorService errorService) : base(errorService)
        {
        }

        // GET: LcAgent
        public ActionResult Index()
        {
            return View();
        }
    }
}