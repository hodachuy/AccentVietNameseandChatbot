using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BotProject.Web.Controllers
{
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class SearchEngineController : Controller
    {
        // GET: SearchEngine
        public ActionResult Index()
        {
            return View();
        }
    }
}