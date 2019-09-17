using AIMLbot;
using BotProject.Common;
using BotProject.Model.Models;
using BotProject.Service;
using BotProject.Web.Infrastructure.Core;
using BotProject.Web.Infrastructure.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SearchEngine.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace BotProject.Web.API
{
    /// <summary>
    /// Webhook
    /// Receive request from Facebook when user trigger to message
    /// </summary>
    public class WebhookController : ApiController
    {
        string pageToken = Helper.ReadString("AccessToken");
        string appSecret = Helper.ReadString("AppSecret");
        string verifytoken =  Helper.ReadString("VerifyTokenWebHook");


        private readonly string UrlAPI = Helper.ReadString("UrlAPI");
        private readonly string KeyAPI = Helper.ReadString("KeyAPI");
        private string pathAIML = PathServer.PathAIML;
        private string pathSetting = PathServer.PathAIML + "config";
        private BotService _botService;
        private IBotService _botDbService;
        private ISettingService _settingService;
        private IHandleModuleServiceService _handleMdService;
        private IModuleService _mdService;
        private IModuleKnowledegeService _mdKnowledgeService;
        private IMdSearchService _mdSearchService;
        private IErrorService _errorService;
        private IAIMLFileService _aimlFileService;
        private IQnAService _qnaService;
        private ApiQnaNLRService _apiNLR;
        private IModuleSearchEngineService _moduleSearchEngineService;
        private IHistoryService _historyService;
        private ICardService _cardService;
        //private Bot _bot;
        private User _user;


        public WebhookController(IErrorService errorService,
                              IBotService botDbService,
                              ISettingService settingService,
                              IHandleModuleServiceService handleMdService,
                              IModuleService mdService,
                              IMdSearchService mdSearchService,
                              IModuleKnowledegeService mdKnowledgeService,
                              IAIMLFileService aimlFileService,
                              IQnAService qnaService,
                              IModuleSearchEngineService moduleSearchEngineService,
                              IHistoryService historyService,
                              ICardService cardService)
        {
            _errorService = errorService;
            //_accentService = AccentService.AccentInstance;
            _botDbService = botDbService;
            _settingService = settingService;
            _handleMdService = handleMdService;
            _mdService = mdService;
            _apiNLR = new ApiQnaNLRService();
            _mdKnowledgeService = mdKnowledgeService;
            _mdSearchService = mdSearchService;
            _aimlFileService = aimlFileService;
            _qnaService = qnaService;
            _botService = BotService.BotInstance;
            _moduleSearchEngineService = moduleSearchEngineService;
            _historyService = historyService;
            _cardService = cardService;
        }

        public HttpResponseMessage Get()
        {
            var querystrings = Request.GetQueryNameValuePairs().ToDictionary(x => x.Key, x => x.Value);
            if (querystrings["hub.verify_token"] == "lacviet_bot_chat")
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(querystrings["hub.challenge"], Encoding.UTF8, "text/plain")
                };
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post()
        {
            var signature = Request.Headers.GetValues("X-Hub-Signature").FirstOrDefault().Replace("sha1=", "");
            var body = await Request.Content.ReadAsStringAsync();
            if (!VerifySignature(signature, body))
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            var value = JsonConvert.DeserializeObject<FacebookBotRequest>(body);

            //Test
            var botRequest = new JavaScriptSerializer().Serialize(value);
            LogError(botRequest);

            if (value.@object != "page")
                return new HttpResponseMessage(HttpStatusCode.OK);

            foreach (var item in value.entry[0].messaging)
            {
                //Test
                var json = new JavaScriptSerializer().Serialize(item);
                LogError(json);

                if (item.message == null && item.postback == null)
                    continue;
                else
                    await SendMessage(GetMessageTemplate(item.message.text, item.sender.id));
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        private bool VerifySignature(string signature, string body)
        {
            var hashString = new StringBuilder();
            using (var crypto = new HMACSHA1(Encoding.UTF8.GetBytes(appSecret)))
            {
                var hash = crypto.ComputeHash(Encoding.UTF8.GetBytes(body));
                foreach (var item in hash)
                    hashString.Append(item.ToString("X2"));
            }

            return hashString.ToString().ToLower() == signature.ToLower();
        }

        /// <summary>
        /// get text message template
        /// </summary>
        /// <param name="text">text</param>
        /// <param name="sender">sender id</param>
        /// <returns>json</returns>
        private JObject GetMessageTemplate(string text, string sender)
        {
            return JObject.FromObject(
                new
                {
                    recipient = new { id = sender },
                    message = new { text = "Welcome to Chatbot Lacviet!" },
                });
        }

        /// <summary>
        /// send message
        /// </summary>
        /// <param name="json">json</param>
        private async Task SendMessage(JObject json)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.PostAsync($"https://graph.facebook.com/v3.2/me/messages?access_token={pageToken}", new StringContent(json.ToString(), Encoding.UTF8, "application/json"));
            }
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
