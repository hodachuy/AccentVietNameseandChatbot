using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BotProject.Service;

namespace BotProject.Web.API_Livechat
{
    public class ConversationController : ApiControllerBase
    {
        public ConversationController(IErrorService errorService) : base(errorService)
        {
        }
    }
}
