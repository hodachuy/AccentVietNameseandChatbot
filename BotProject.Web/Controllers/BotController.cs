using BotProject.Service;
using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BotProject.Web.Controllers
{
    public class BotController : BaseController
    {
        IErrorService _errorService;
        public BotController(IErrorService errorService) : base(errorService)
        {
            this._errorService = errorService;
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}