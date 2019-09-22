using AIMLbot;
using AutoMapper;
using BotProject.Common;
using BotProject.Model.Models;
using BotProject.Service;
using BotProject.Web.Infrastructure.Core;
using BotProject.Web.Infrastructure.Extensions;
using BotProject.Web.Models;
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
            //var signature = Request.Headers.GetValues("X-Hub-Signature").FirstOrDefault().Replace("sha1=", "");
            var body = await Request.Content.ReadAsStringAsync();
            //if (!VerifySignature(signature, body))
                //return new HttpResponseMessage(HttpStatusCode.BadRequest);

            var value = JsonConvert.DeserializeObject<FacebookBotRequest>(body);

            ////Test
            //var botRequest = new JavaScriptSerializer().Serialize(value);
            //LogError(botRequest);

            if (value.@object != "page")
                return new HttpResponseMessage(HttpStatusCode.OK);


            var lstAIML = _aimlFileService.GetByBotId(5028);
            var lstAIMLVm = Mapper.Map<IEnumerable<AIMLFile>, IEnumerable<AIMLViewModel>>(lstAIML);
            _botService.loadAIMLFromDatabase(lstAIMLVm);
            _user = _botService.loadUserBot(value.entry[0].messaging[0].sender.id);
            _user.Predicates.addSetting("agecheck", "false");

            foreach (var item in value.entry[0].messaging)
            {
                if (item.message == null && item.postback == null)
                {
                    continue;
                }
                else if(item.message == null && item.postback != null)
                {
                    await SendMessage(GetMessageTemplate(item.postback.payload, item.sender.id));
                }
                else
                {
                    if(item.message.quick_reply == null)
					{
						await SendMessage(GetMessageTemplate(item.message.text, item.sender.id));
					}
					else
					{
						await SendMessage(GetMessageTemplate(item.message.quick_reply.payload, item.sender.id));
					}
				}
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }


		public async Task ExcuteMessage(string text, string sender)
		{
			// check user exist and add update user
			// get predicate user from db

			AIMLbot.Result aimlBotResult = _botService.Chat(text, _user);
			string result = aimlBotResult.OutputSentences[0].ToString();

		}

		/// <summary>
		/// get text message template
		/// </summary>
		/// <param name="text">text</param>
		/// <param name="sender">sender id</param>
		/// <returns>json</returns>
		private JObject GetMessageTemplate(string text, string sender)
        {
            if (text.ToLower().Contains("menu") != true)
            {
                return JObject.FromObject(
                    new
                    {
                        recipient = new { id = sender },
                        message = new { text = "Chào mừng bạn đến với Trung tâm Digipro, A/C có vấn đề gì cần giải đáp ạ." },
                    });
            }


            return JObject.FromObject(
                new
                {
                    recipient = new { id = sender },
                    message = new
                    {
                        attachment = new
                        {
                            type = "template",
                            payload = new
                            {
                                template_type = "generic",
                                elements = new[]
                                {
                                    new
                                    {
                                        title = "Trung tâm chăm sóc khách hàng Digipro.vn",
                                        item_url = "http://digipro.vn/",
                                        image_url = "https://bot.surelrn.vn/File/Images/Card/134a16f1-7c56-4eca-a61b-1bbe5a23a42b-Logo_DGP_EN_1600-800_5.png",
                                        subtitle = "Tư vấn bảo hành, sửa chữa máy tính",
                                        buttons = new []
                                        {
                                            new
                                            {
                                                  type = "postback",
                                                  title = "💻 Bảo hành dòng máy Dell",
                                                  payload = "postback_card_6070"
                                            },
                                            new
                                            {
                                                  type = "postback",
                                                  title = "🔍 Tra cứu máy bảo hành",
                                                  payload = "postback_card_6071"
                                            },
                                            new
                                            {
                                                  type = "postback",
                                                  title =  "📞 Thông tin hỗ trợ",
                                                  payload = "postback_card_6072"
                                            },
                                        }
                                    }
                                }
                            }
                        }
                    },
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

		/// <summary>
		/// send message
		/// </summary>
		/// <param name="templateJson">templateJson</param>
		private async Task SendMessage(string templateJson)
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage res = await client.PostAsync($"https://graph.facebook.com/v3.2/me/messages?access_token={pageToken}", new StringContent(templateJson, Encoding.UTF8, "application/json"));
			}
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
