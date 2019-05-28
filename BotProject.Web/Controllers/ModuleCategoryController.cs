using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BotProject.Service;

namespace BotProject.Web.Controllers
{
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class ModuleCategoryController : BaseController
    {
        private IModuleCategoryService _mdQnAService;
        public ModuleCategoryController(IErrorService errorService, IModuleCategoryService mdQnAService) : base(errorService)
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