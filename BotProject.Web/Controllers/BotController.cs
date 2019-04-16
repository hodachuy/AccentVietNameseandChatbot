using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BotProject.Service;
using AutoMapper;
using BotProject.Model.Models;
using BotProject.Web.Models;

namespace BotProject.Web.Controllers
{
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class BotController : BaseController
    {
        private ICardService _cardService;
		private IQnAService _qnaService;
        private ISettingService _settingService;
        public BotController(IErrorService errorService,
            ICardService cardService,
            IQnAService qnaService,
            ISettingService settingService
           ) : base(errorService)
        {
            _cardService = cardService;
			_qnaService = qnaService;
            _settingService = settingService;

        }

        // GET: Bot
        public ActionResult Index()
        {
            return View();
        }     

		public ActionResult QnA(int id)
		{
            ViewBag.BotQnAnswerID = id;
			var botQnA = _qnaService.GetBotQnAnswerById(id);
			var lstCard = _cardService.GetListCardByBotID(botQnA.BotID).ToList();
			ViewBag.Cards = lstCard;
			return View(botQnA);
		}

		public ActionResult CardCategory(int id) {
            ViewBag.BotID = id;
            var lstCard = _cardService.GetListCardByBotID(id);
            return View(lstCard);
		}

		public ActionResult AIML(int id)
		{
            ViewBag.BotID = id;
            return View();
		}

        public ActionResult Setting(int id, string name)
        {
            var settingDb = _settingService.GetSettingByBotID(id);
            var lstCard = _cardService.GetListCardByBotID(id);
            var settingVm = Mapper.Map<Setting, BotSettingViewModel>(settingDb);
            ViewBag.Cards = lstCard;
            ViewBag.BotName = name;
            ViewBag.UserID = UserInfo.Id;
            return View(settingVm);
        }

        public ActionResult FormChatSetting(string botName)
        {
            ViewBag.BotName = botName;
            return View();
        }
    }
}