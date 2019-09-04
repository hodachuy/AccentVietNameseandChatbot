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
        private IModuleService _moduleService;
        private IModuleSearchEngineService _mdSearchEngineService;
        private IBotService _botService;
        public BotController(IErrorService errorService,
            ICardService cardService,
            IQnAService qnaService,
            ISettingService settingService,
            IGroupCardService groupCardService,
            IModuleService moduleService,
            IBotService botService,
            IModuleSearchEngineService mdSearchEngineService
           ) : base(errorService)
        {
            _cardService = cardService;
			_qnaService = qnaService;
            _settingService = settingService;
            _groupCardService = groupCardService;
            _moduleService = moduleService;
            _mdSearchEngineService = mdSearchEngineService;
            _botService = botService;

        }

        // GET: Bot
        public ActionResult Index()
        {
            return View();
        }     

		public ActionResult QnA(int formQnAId, int botId, string botName)
		{
            if (String.IsNullOrEmpty(botName))
            {
                return RedirectToAction("Index","Dashboard");
            }
            var formQnA = _qnaService.GetFormQnAnswerById(formQnAId);
            var formQnAVm = Mapper.Map<FormQuestionAnswer, FormQuestionAnswerViewModel>(formQnA);
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
            ViewBag.BotQnAnswerID = formQnAId;
            ViewBag.BotName = botName;
            ViewBag.Cards = lstGroupCardVm;
            return View(formQnAVm);
		}

        public ActionResult Module(int id, string botName)
        {
            if (String.IsNullOrEmpty(botName))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.BotID = id;
            ViewBag.BotName = botName;
            return View();
        }

        public ActionResult CardCategory(int id, string botName)
        {
            if (String.IsNullOrEmpty(botName))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.BotID = id;
            ViewBag.BotName = botName;
            var lstModule = _moduleService.GetAllModuleByBotID(id);
            var lstModuleVm = Mapper.Map<IEnumerable<Module>, IEnumerable<ModuleViewModel>>(lstModule);
            return View(lstModuleVm);
		}

		public ActionResult AIML(int id)
		{
            ViewBag.BotID = id;
            return View();
		}

        public ActionResult Setting(int id, string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            var settingDb = _settingService.GetSettingByBotID(id);
            var lstCard = _cardService.GetListCardByBotID(id);
            var settingVm = Mapper.Map<Setting, BotSettingViewModel>(settingDb);
            var lstBot = _botService.GetListBotByUserID(UserInfo.Id);
            var lstBotVm = Mapper.Map<IEnumerable<Bot>,IEnumerable<BotViewModel>>(lstBot);
            var lstSystemConfig = _settingService.GetListSystemConfigByBotId(id);
            ViewBag.Cards = lstCard;
            ViewBag.BotName = name;
            ViewBag.UserID = UserInfo.Id;
            ViewBag.Bots = lstBotVm;
            ViewBag.SystemConfigs = lstSystemConfig;
            return View(settingVm);
        }

        public ActionResult FormChatSetting(string botName)
        {
            ViewBag.BotName = botName;
            return View();
        }

        public ActionResult BotSearchEngine(int botId, string botName)
        {
            if (String.IsNullOrEmpty(botName))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.BotName = botName;
            ViewBag.BotID = botId;
            var lstMdArea = _mdSearchEngineService.GetListMdArea(botId).ToList();
            ViewBag.MdArea = lstMdArea;
            return View();
        }

        public ActionResult BotHistory(int botId, string botName)
        {
            ViewBag.BotName = botName;
            ViewBag.BotID = botId;
            return View();
        }
    }
}