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
        ICardService _cardService;
        public BotController(IErrorService errorService, ICardService cardService) : base(errorService)
        {
            _cardService = cardService;
        }

        // GET: Bot
        public ActionResult Index()
        {
            return View();
        }     

		public ActionResult QnA(int id)
		{
            ViewBag.BotID = id;
            var lstCard = _cardService.GetListCardByBotID(id);
            return View(lstCard);
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