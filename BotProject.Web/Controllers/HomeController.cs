using BotProject.Common.ViewModels;
using BotProject.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BotProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private IModuleSearchEngineService _moduleSearchEngineService;
        public HomeController(IModuleSearchEngineService moduleSearchEngineService)
        {
            _moduleSearchEngineService = moduleSearchEngineService;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FAQ(int id)
        {
            string filter = "q.ID = " + id;
            MdQnAViewModel qna = new MdQnAViewModel();
            qna = _moduleSearchEngineService.GetListMdQnA(filter, "", 1, 1, null).ToList().FirstOrDefault();
            return View(qna);
        }
    }
}