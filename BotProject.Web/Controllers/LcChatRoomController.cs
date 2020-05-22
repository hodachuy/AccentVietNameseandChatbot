using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BotProject.Service;

namespace BotProject.Web.Controllers
{
    public class LcChatRoomController : BaseController
    {
        public LcChatRoomController(IErrorService errorService) : base(errorService)
        {
        }

        // GET: ChatRoom
        public ActionResult Index()
        {
            ViewBag.UserInfo = UserInfo;
            ViewBag.BotID = UserInfo.BotActiveID; 
            return View(UserInfo);
        }
    }
}