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
        private IGroupCardService _groupCardService;
        public BotController(IErrorService errorService,
            ICardService cardService,
            IQnAService qnaService,
            ISettingService settingService,
            IGroupCardService groupCardService
           ) : base(errorService)
        {
            _cardService = cardService;
			_qnaService = qnaService;
            _settingService = settingService;
            _groupCardService = groupCardService;

        }

        // GET: Bot
        public ActionResult Index()
        {
            return View();
        }     

		public ActionResult QnA(int formQnAId, int botId)
		{
            ViewBag.BotQnAnswerID = formQnAId;
			var formQnA = _qnaService.GetFormQnAnswerById(formQnAId);
			//var lstCard = _cardService.GetListCardByBotID(formQnA.BotID).ToList();
            var lstGroupCard = _groupCardService.GetListGroupCardByBotID(botId);
            var lstGroupCardVm = Mapper.Map<IEnumerable<GroupCard>, IEnumerable<GroupCardViewModel>>(lstGroupCard);
            if (lstGroupCardVm != null && lstGroupCardVm.Count() != 0)
            {
                foreach (var item in lstGroupCardVm)
                {
                    var lstCard = _cardService.GetListCardByGroupCardID(item.ID).ToList();
                    item.Cards = Mapper.Map<IEnumerable<Card>, IEnumerable<CardViewModel>>(lstCard);
                }
            }
            ViewBag.Cards = lstGroupCardVm;
			return View(formQnA);
		}

		public ActionResult CardCategory(int id) {
            ViewBag.BotID = id;
            return View();
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