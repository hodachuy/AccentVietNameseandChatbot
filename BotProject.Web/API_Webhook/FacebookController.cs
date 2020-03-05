using AIMLbot;
using AutoMapper;
using BotProject.Common;
using BotProject.Common.AppThird3PartyTemplate;
using BotProject.Common.ViewModels;
using BotProject.Model.Models;
using BotProject.Service;
using BotProject.Web.Infrastructure.Core;
using BotProject.Web.Infrastructure.Extensions;
using BotProject.Web.Infrastructure.Log4Net;
using BotProject.Web.Models;
using Common.Logging.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace BotProject.Web.API_Webhook
{
    /// <summary>
    /// --- Developer Facebook ---
    /// http://developers.facebook.com
    /// ------------------------------
    /// Webhook nhận tín hiệu dữ liệu tin nhắn gửi tới từ người dùng trên
    /// nền tảng facebook.
    /// </summary>
    public class FacebookController : ApiController
    {
        // appSettings
        string pageToken = Helper.ReadString("AccessToken");
        string appSecret = Helper.ReadString("AppSecret");
        string verifytoken = Helper.ReadString("VerifyTokenWebHook");
        private Dictionary<string, string> _dicAttributeUser = new Dictionary<string, string>();
        private readonly string Domain = Helper.ReadString("Domain");
        private Dictionary<string, string> _dicNotMatch = new Dictionary<string, string>();

        /// <summary>
        /// Điều kiện để đi tới card tiếp theo
        /// </summary>
        private readonly string[] ARRAY_CONDITION_NAME = new string[]
        {
            "REQUIRE_CLICK_BUTTON_TO_NEXT_CARD",//IsHaveCondition
            "REQUIRE_INPUT_TEXT_TO_NEXT_CARD",//IsConditionWithInputText + CardStepID NOT NULL
            "VERIFY_TEXT_WITH_AREA_BUTTON",//IsConditionWithAreaButton
            "POSTBACK_MODULE",
        };

        /// <summary>
        /// Thời gian chờ để phản hồi lại tin nhắn,thời gian tính từ tin nhắn cuối cùng
        /// người dùng không tương tác lại
        /// </summary>
        private int TIMEOUT_DELAY_SEND_MESSAGE = 60;

        /// <summary>
        /// Thẻ bắt đầu khi lần đầu tiên người dùng tương tác
        /// </summary>
        private string CARD_GET_STARTED = "danh mục"; //default "danh mục knowledgebase"

        private string TITLE_PAYLOAD_QUICKREPLY = "";

        // Model user
        ApplicationFacebookUser _fbUser;

        // Services
        private IApplicationFacebookUserService _appFacebookUser;
        private BotServiceMedical _botService;
        private ISettingService _settingService;
        private IHandleModuleServiceService _handleMdService;
        private IErrorService _errorService;
        private IAIMLFileService _aimlFileService;
        private IQnAService _qnaService;
        private ApiQnaNLRService _apiNLR;
        private IHistoryService _historyService;
        private ICardService _cardService;
        private IApplicationThirdPartyService _app3rd;
        private AccentService _accentService;
        private IAttributeSystemService _attributeService;
        public FacebookController(IApplicationFacebookUserService appFacebookUser,
                                  ISettingService settingService,
                                  IHandleModuleServiceService handleMdService,
                                  IErrorService errorService,
                                  IAIMLFileService aimlFileService,
                                  IQnAService qnaService,
                                  IHistoryService historyService,
                                  ICardService cardService,
                                  IApplicationThirdPartyService _app3rd,
                                  IAttributeSystemService attributeService)
        {
            _errorService = errorService;
            _appFacebookUser = appFacebookUser;
            _settingService = settingService;
            _historyService = historyService;
            _cardService = cardService;
            _attributeService = attributeService;
            _accentService = new AccentService();
            _fbUser = new ApplicationFacebookUser();
        }

        /// <summary>
        /// Facebook kiểm tra mã bảo mật của đường dẫn webhook thiết lập
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Nhận tin nhắn từ người dùng facebook
        /// FacebookBotRequest
        /// <param></param>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> Post()
        {
            var signature = Request.Headers.GetValues("X-Hub-Signature").FirstOrDefault().Replace("sha1=", "");
            var body = await Request.Content.ReadAsStringAsync();
            if (!VerifySignature(signature, body))
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            FacebookBotRequest objMsgUser = JsonConvert.DeserializeObject<FacebookBotRequest>(body);
            if (objMsgUser.@object != "page")
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            string fbPageId = objMsgUser.entry[0].id;
            Setting settingDb = _settingService.GetSettingByFbPageID(fbPageId);
            int botId = settingDb.BotID;

            foreach (var item in objMsgUser.entry[0].messaging)
            {
                if (item.message == null && item.postback == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                var lstAttribute = _attributeService.GetListAttributeFacebook(item.sender.id, botId).ToList();
                if (lstAttribute.Count() != 0)
                {
                    _dicAttributeUser = new Dictionary<string, string>();
                    foreach (var attr in lstAttribute)
                    {
                        _dicAttributeUser.Add(attr.AttributeKey, attr.AttributeValue);
                    }
                }
                if (item.message == null && item.postback != null)
                {
                    await ExcuteMessage(item.postback.payload, item.sender.id, botId, item.timestamp, "payload_postback");
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    if (item.message.quick_reply != null)
                    {
                        await ExcuteMessage(item.message.quick_reply.payload, item.sender.id, botId, item.timestamp, "payload_postback");
                        return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                    else
                    {
                        await ExcuteMessage(item.message.text, item.sender.id, botId, item.timestamp, "text");
                        return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// Xử lý tin nhắn facebook
        /// </summary>
        /// <param name="text"></param>
        /// <param name="sender"></param>
        /// <param name="botId"></param>
        /// <param name="timeStamp"></param>
        /// <param name="typeBotRequest"></param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> ExcuteMessage(string text,
                                                              string sender,
                                                              int botId,
                                                              string timeStamp,
                                                              string typeRequest)
        {
            if (String.IsNullOrEmpty(text) || String.IsNullOrEmpty(sender))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            // Kiểm tra duplicate request trong cùng một thời gian
            if (!String.IsNullOrEmpty(timeStamp))
            {
                var rs = _appFacebookUser.CheckDuplicateRequestWithTimeStamp(timeStamp, sender);
                if (rs == 4)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }

            text = HttpUtility.HtmlDecode(text);
            text = Regex.Replace(text, @"<(.|\n)*?>", "").Trim(); // remove tag html
            text = Regex.Replace(text, @"\p{Cs}", "").Trim();// remove emoji

            // Typing writing...
            await SendMessageTyping(sender);

            // Lấy thông tin người dùng
            _fbUser = GetUserById(sender);

            // Input text
            if (typeRequest == CommonConstants.BOT_REQUEST_TEXT)
            {
                // Thêm dấu tiếng việt
                string textAccentVN = GetPredictAccentVN(text);
                if (textAccentVN != text)
                {
                    string msg = FacebookTemplate.GetMessageTemplateText("Ý bạn là: " + textAccentVN + "", sender).ToString();
                    await SendMessageTask(msg, sender);
                }
                text = textAccentVN;
            }

            // Input postback            
            if (typeRequest == CommonConstants.BOT_REQUEST_PAYLOAD_POSTBACK)
            {
                string[] arrPayloadQuickReply = Regex.Split(text, "-");
                if (arrPayloadQuickReply.Length > 1)
                {
                    TITLE_PAYLOAD_QUICKREPLY = arrPayloadQuickReply[1];
                }
                text = arrPayloadQuickReply[0];
            }

            if (_fbUser.IsHavePredicate)
            {
                if (typeRequest == CommonConstants.BOT_REQUEST_PAYLOAD_POSTBACK)
                {
                    _fbUser.IsHavePredicate = false;
                    _fbUser.PredicateName = "";
                }
                if (_fbUser.PredicateName == "REQUIRE_CLICK_BUTTON_TO_NEXT_CARD")
                {

                }
                if (_fbUser.PredicateName == "REQUIRE_INPUT_TEXT_TO_NEXT_CARD")
                {

                }
                if (_fbUser.PredicateName == "VERIFY_TEXT_WITH_AREA_BUTTON")
                {

                }
                if (_fbUser.PredicateName == "POSTBACK_MODULE")
                {
                    if (_fbUser.PredicateValue == "POSTBACK_MODULE_ADMIN_CONTACT")
                    {

                    }
                }
                //Điều kiện CHAT_ADMIN
                //Điều kiện CONDITION_CARD
                //Điều kiện CONDITION_POSTBACK_MODULE
            }

            AIMLbot.Result rsAIMLBot;
            rsAIMLBot = GetBotReplyFromAIMLBot(text);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        private AIMLbot.Result GetBotReplyFromAIMLBot(string text)
        {
            AIMLbot.Result aimlBotResult = _botService.Chat(text);
            return aimlBotResult;
        }
        private ResultBot checkResultBotReply(AIMLbot.Result rsAIMLBot)
        {
            ResultBot rsBot = new ResultBot();
            string result = "";
            // TH postback_module
            // TH NOTMATCH
            // TH postback_card
            if (rsAIMLBot.OutputSentences != null && rsAIMLBot.OutputSentences.Count() != 0)
            {
                result = rsAIMLBot.OutputSentences[0].ToString();

                string strTempPostback = rsAIMLBot.SubQueries[0].Template;
                // nếu nhập text trả về output là postback
                bool isPostback = Regex.Match(strTempPostback, "<template><srai>postback_card_(\\d+)</srai></template>").Success;
                //trường hợp trả về câu hỏi random chứa postback
                bool isPostbackAnswer = Regex.Match(strTempPostback, "<template><srai>postback_answer_(\\d+)</srai></template>").Success;


                string payload = result.Replace("\r\n", "").Trim();
                if (payload.Contains(CommonConstants.POSTBACK_MODULE))
                {
                    //k phải từ button module trả về
                    if (payload.Contains("<module>") != true)
                    {
                        string txtModule = payload.Replace("\r\n", "").Replace(".", "").Trim();
                        txtModule = Regex.Replace(txtModule, @"<(.|\n)*?>", "").Trim();
                        int idxModule = txtModule.IndexOf("postback_module");
                        if (idxModule != -1)
                        {
                            string strPostback = txtModule.Substring(idxModule, txtModule.Length - idxModule);
                            var punctuation = strPostback.Where(Char.IsPunctuation).Distinct().ToArray();
                            var words = strPostback.Split().Select(x => x.Trim(punctuation));
                            var patternPayloadModule = words.SingleOrDefault(x => x.Contains("postback_module") == true);

                            if (words.ToList().Count == 1 && (txtModule.Length == patternPayloadModule.Length))
                            {
                                rsBot.Type = "POSTBACK_MODULE";
                                rsBot.Total = 1;
                                rsBot.PatternPayload = patternPayloadModule;
                            }

                            _fbUser.IsHavePredicate = true;
                            _fbUser.PredicateValue = patternPayloadModule;
                            _fbUser.PredicateName = "POSTBACK_MODULE";

                            return rsBot;
                        }
                    }
                    else if (result.Contains("NOT_MATCH"))
                    {

                    }                   
                }
            }
            return rsBot;
        }

        private string getCasePayload(string caseName)
        {
            string rsCase = "0";
            switch (caseName)
            {
                case "NOT_MATCH":
                    _dicNotMatch = new Dictionary<string, string>()
                    {
                       {"NOT_MATCH_01", "Xin lỗi,em chưa hiểu ý anh/chị ạ!"},
                       {"NOT_MATCH_02", "Anh/chị có thể giải thích thêm được không?"},
                       {"NOT_MATCH_03", "Chưa hiểu lắm ạ, anh/chị có thể nói rõ hơn được không ạ?"},
                       {"NOT_MATCH_04", "Xin lỗi, Anh/chị có thể giải thích thêm được không?"},
                       {"NOT_MATCH_05", "Xin lỗi, Tôi chưa được học để hiểu nội dung này?"}
                    };
                    rsCase = "0";
                    break;
                case "POSTBACK_MODULE":
                    break;
                case "POSTBACK_CARD":
                    break;
            }
            return rsCase;
        }

        private string GetBotReplyFromCard(string patternCard)
        {
            var cardDb = _cardService.GetCardByPattern(patternCard);
            return cardDb.TemplateJsonFacebook;
        }



        #region Create update and get Facebook user
        private ApplicationFacebookUser UpdateStatusFacebookUser(string userId, ApplicationFacebookUser fbUserVm)
        {
            ApplicationFacebookUser appFbUser = new ApplicationFacebookUser();
            appFbUser = _appFacebookUser.GetByUserId(userId);
            appFbUser.IsHavePredicate = fbUserVm.IsHavePredicate;
            appFbUser.PredicateName = fbUserVm.PredicateName;
            appFbUser.PredicateValue = fbUserVm.PredicateValue;
            _appFacebookUser.Update(appFbUser);
            _appFacebookUser.Save();
            return appFbUser;
        }
        private ApplicationFacebookUser GetUserById(string senderId)
        {
            ApplicationFacebookUser fbUserDb = new ApplicationFacebookUser();
            fbUserDb = _appFacebookUser.GetByUserId(senderId);
            if (fbUserDb == null)
            {
                ProfileUser profileUser = new ProfileUser();
                profileUser = GetProfileUser(senderId);

                fbUserDb = new ApplicationFacebookUser();
                ApplicationFacebookUserViewModel fbUserVm = new ApplicationFacebookUserViewModel();
                fbUserDb.UserId = senderId;
                fbUserDb.IsHavePredicate = false;
                fbUserDb.IsProactiveMessage = false;
                fbUserDb.TimeOut = DateTime.Now.AddSeconds(TIMEOUT_DELAY_SEND_MESSAGE);
                fbUserDb.CreatedDate = DateTime.Now;
                fbUserDb.StartedOn = DateTime.Now;
                fbUserDb.FirstName = profileUser.first_name;
                fbUserDb.Age = 0;// "N/A";
                fbUserDb.LastName = profileUser.last_name;
                fbUserDb.UserName = profileUser.first_name + " " + profileUser.last_name;
                fbUserDb.Gender = true; //"N/A";
                fbUserDb.UpdateFacebookUser(fbUserVm);
                _appFacebookUser.Add(fbUserDb);
                _appFacebookUser.Save();
            }
            return fbUserDb;
        }

        private ProfileUser GetProfileUser(string senderId)
        {
            ProfileUser user = new ProfileUser();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage res = new HttpResponseMessage();
                res = client.GetAsync($"https://graph.facebook.com/" + senderId + "?fields=first_name,last_name,profile_pic&access_token=" + pageToken).Result;//gender y/c khi sử dụng
                if (res.IsSuccessStatusCode)
                {
                    var serializer = new JavaScriptSerializer();
                    serializer.MaxJsonLength = Int32.MaxValue;
                    user = serializer.Deserialize<ProfileUser>(res.Content.ReadAsStringAsync().Result);
                }
                return user;
            }
        }

        private void AddAttributeDefault(string userId, int BotId, string key, string value)
        {
            AttributeFacebookUser attFbUser = new AttributeFacebookUser();
            attFbUser.UserID = userId;
            attFbUser.BotID = BotId;
            attFbUser.AttributeKey = key;
            attFbUser.AttributeValue = value;
            _attributeService.CreateUpdateAttributeFacebook(attFbUser);
            _attributeService.Save();
        }
        #endregion

        #region Handle condition card and module
        private string HandleCardCondition()
        {
            return "";
        }
        private string HandleModuleCondition()
        {
            return "";
        }
        #endregion

        #region Convert AccentVN - Thêm dấu Tiếng việt
        private string GetPredictAccentVN(string text, bool isActive = false)
        {
            string textVN = text;
            if (isActive)
            {
                textVN = _accentService.GetAccentVN(text);
            }
            return textVN;
        }
        #endregion

        #region Send API Message Facebook
        private async Task SendMessageTask(string templateJson, string sender)
        {
            if (!String.IsNullOrEmpty(templateJson))
            {
                templateJson = templateJson.Replace("{{senderId}}", sender);
                templateJson = Regex.Replace(templateJson, "File/", Domain + "File/");
                if (templateJson.Contains("{{"))
                {
                    if (_dicAttributeUser != null && _dicAttributeUser.Count() != 0)
                    {
                        foreach (var item in _dicAttributeUser)
                        {
                            string val = String.IsNullOrEmpty(item.Value) == true ? "N/A" : item.Value;
                            templateJson = templateJson.Replace("{{" + item.Key + "}}", val);
                        }
                    }
                }
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage res = await client.PostAsync($"https://graph.facebook.com/v3.2/me/messages?access_token=" + pageToken + "",
                    new StringContent(templateJson, Encoding.UTF8, "application/json"));
                }
            }
        }

        private async Task SendMessageTyping(string sender)
        {
            string senderActionTyping = FacebookTemplate.GetMessageSenderAction("typing_on", sender).ToString();
            await SendMessageTask(senderActionTyping, sender);
        }
        #endregion

        #region VerifySignatureFacebook
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
        #endregion


        public class ProfileUser
        {
            public string first_name { set; get; }
            public string last_name { set; get; }
            public string profile_pic { set; get; }
            public string id { set; get; }
            public string gender { set; get; }
        }

        public class ResultBot
        {
            public string Type { set; get; }
            public int Total { set; get; }
            public string PatternPayload { set; get; }
        }
    }
}
