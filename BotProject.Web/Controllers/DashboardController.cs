using AutoMapper;
using BotProject.Model.Models;
using BotProject.Service;
using BotProject.Web.Infrastructure.Core;
using BotProject.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace BotProject.Web.Controllers
{
    public class DashboardController : BaseController
    {
        IBotService _botService;
        public DashboardController(IErrorService errorService, IBotService botService) : base(errorService)
        {
            _botService = botService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            ViewBag.UserName = UserInfo.UserName;
            return PartialView();
        }

        [ChildActionOnly]
        //[OutputCache(Duration = 3600, Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult BotCategory()
        {
            var lstBot = _botService.GetListBotByUserID(UserInfo.Id);
            var lstBotViewModel = Mapper.Map<IEnumerable<Bot>, IEnumerable<BotViewModel>>(lstBot);
            return PartialView(lstBotViewModel);
        }
    }
}