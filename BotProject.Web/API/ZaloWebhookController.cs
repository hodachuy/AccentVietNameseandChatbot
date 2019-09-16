using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BotProject.Service;
using System.Threading.Tasks;

namespace BotProject.Web.API
{
    public class ZaloWebhookController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> Post()
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
