using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AIMLbot;
using System.Web.Hosting;

namespace Accent.Web.Controllers
{
    public class LegalController : Controller
    {
        ApiController API = new ApiController();
        
        private readonly string UrlAPI = Helper.ReadString("UrlAPI");
        private readonly string KeyAPI = Helper.ReadString("KeyAPI");
        private Bot bot;
        private User user;
        private string pathAIML = HostingEnvironment.MapPath("~/Datasets_BOT/aiml");
        Dictionary<string, string> NOT_MATCH;

        // GET: Legal
        public ActionResult ChatLegal()
        {          
            return View();
        }
    }
}