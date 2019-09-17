using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BotProject.Service;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using BotProject.Model.Models;
using BotProject.Web.Infrastructure.Extensions;

namespace BotProject.Web.API
{
    public class ZaloWebhookController : ApiController
    {
        string pageToken = Helper.ReadString("AccessToken");
        string appSecret = Helper.ReadString("AppSecret");
        string verifytoken = Helper.ReadString("VerifyTokenWebHook");

        private IErrorService _errorService;
        public ZaloWebhookController(IErrorService errorService)
        {
            _errorService = errorService;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post()
        {
            var body = await Request.Content.ReadAsStringAsync();
            if (body.Contains("user_send_text") == false)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            var value = JsonConvert.DeserializeObject<ZaloBotRequest>(body);
            LogError(body);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        private void LogError(string message)
        {
            try
            {
                Error error = new Error();
                error.CreatedDate = DateTime.Now;
                error.Message = message;
                _errorService.Create(error);
                _errorService.Save();
            }
            catch
            {
            }
        }
    }
}
