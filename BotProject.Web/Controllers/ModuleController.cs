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
        private IMdQnAService _mdQnAService;
        public ModuleController(IErrorService errorService, IMdQnAService mdQnAService) : base(errorService)
        {
            _mdQnAService = mdQnAService;
        }

        // GET: Module
        public ActionResult Search()
        {
            var lstMdArea = _mdQnAService.GetListMdArea(null).ToList();
            ViewBag.MdArea = lstMdArea;
            return View();
        }
    }
}