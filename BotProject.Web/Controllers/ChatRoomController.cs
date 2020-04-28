﻿using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BotProject.Service;

namespace BotProject.Web.Controllers
{
    public class ChatRoomController : BaseController
    {
        public ChatRoomController(IErrorService errorService) : base(errorService)
        {
        }

        // GET: ChatRoom
        public ActionResult Index()
        {
            return View();
        }
    }
}