using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BotProject.Service;

namespace BotProject.Web.Controllers
{
    public class BotController : BaseController
    {
        private ICardService _cardService;
		private IQnAService _qnaService;
        public BotController(IErrorService errorService, ICardService cardService, IQnAService qnaService) : base(errorService)
        {
            _cardService = cardService;
			_qnaService = qnaService;
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
			var lstCard = _cardService.GetListCardByBotID(botQnA.ID);
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
    }
}