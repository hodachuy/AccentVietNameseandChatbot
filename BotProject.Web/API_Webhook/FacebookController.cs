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
        string pageToken = "";
        string appSecret = "";
        string verifytoken = "lacviet_bot_chat";
        private Dictionary<string, string> _dicAttributeUser = new Dictionary<string, string>();
        private readonly string Domain = Helper.ReadString("Domain");
        private Dictionary<string, string> _dicNotMatch = new Dictionary<string, string>();

        private const string POSTBACK_MODULE = "POSTBACK_MODULE";
        private const string POSTBACK_CARD = "POSTBACK_CARD";
        private const string POSTBACK_TEXT = "POSTBACK_TEXT";
        private const string POSTBACK_NOT_MATCH = "POSTBACK_NOT_MATCH";

        private readonly Dictionary<string, string> _DICTIONARY_NOT_MATCH = new Dictionary<string, string>()
        {
           {"NOT_MATCH_01", "Xin lỗi,em chưa hiểu ý anh/chị ạ!"},
           {"NOT_MATCH_02", "Anh/chị có thể giải thích thêm được không?"},
           {"NOT_MATCH_03", "Chưa hiểu lắm ạ, anh/chị có thể nói rõ hơn được không ạ?"},
           {"NOT_MATCH_04", "Xin lỗi, Anh/chị có thể giải thích thêm được không?"},
           {"NOT_MATCH_05", "Xin lỗi, Tôi chưa được học để hiểu nội dung này?"},
        };

        ///// <summary>
        ///// Điều kiện để đi tới card tiếp theo
        ///// </summary>
        //private readonly string[] ARRAY_CONDITION_NAME = new string[]
        //{
        //    "REQUIRE_CLICK_BUTTON_TO_NEXT_CARD",//IsHaveCondition
        //    "REQUIRE_INPUT_TEXT_TO_NEXT_CARD",//IsConditionWithInputText true + CardStepID NOT NULL
        //    "VERIFY_TEXT_WITH_AREA_BUTTON",//IsConditionWithAreaButton
        //    "POSTBACK_MODULE",
        //    "ADD_ATTRIBUTE_USER", // Thuộc tính người dùng 
        //    "AUTO_NEXT_CARD" //IsConditionWithInputText false + CardStepID NOT NULL
        //};

        //private Dictionary<string, string> CONDITION_CARD = new Dictionary<string, string>
        //{
        //    {"CONDITION_CLICK","REQUIRE_CLICK_BUTTON_TO_NEXT_CARD"},
        //    {"CONDITION_INPUT","REQUIRE_INPUT_TEXT_TO_NEXT_CARD"},
        //    {"CONDITION_VERIFY","VERIFY_TEXT_WITH_AREA_BUTTON"},
        //    {"POSTBACK_MODULE","POSTBACK_MODULE"},
        //    {"ADD_ATTRIBUTE_USER","ADD_ATTRIBUTE_USER"},
        //    {"AUTO_NEXT_CARD","AUTO_NEXT_CARD"},
        //};

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

        private string _contactAdmin = Helper.ReadString("AdminContact");
        private string _titlePayloadContactAdmin = Helper.ReadString("TitlePayloadAdminContact");

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
        private AccentService _accentService;
        private IApplicationThirdPartyService _app3rd;
        private IAttributeSystemService _attributeService;
        public FacebookController(IApplicationFacebookUserService appFacebookUser,
                                  ISettingService settingService,
                                  IHandleModuleServiceService handleMdService,
                                  IErrorService errorService,
                                  IAIMLFileService aimlFileService,
                                  IQnAService qnaService,
                                  IHistoryService historyService,
                                  ICardService cardService,
                                  IApplicationThirdPartyService app3rd,
                                  IAttributeSystemService attributeService)
        {
            _errorService = errorService;
            _appFacebookUser = appFacebookUser;
            _settingService = settingService;
            _historyService = historyService;
            _cardService = cardService;
            _attributeService = attributeService;
            _handleMdService = handleMdService;
            _aimlFileService = aimlFileService;
            _app3rd = app3rd;
            _botService = BotServiceMedical.BotInstance;
            _accentService = new AccentService();
            _apiNLR = new ApiQnaNLRService();
            _fbUser = new ApplicationFacebookUser();
        }

        /// <summary>
        /// Facebook kiểm tra mã bảo mật của đường dẫn webhook thiết lập
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Get()
        {
            var querystrings = Request.GetQueryNameValuePairs().ToDictionary(x => x.Key, x => x.Value);
            if (querystrings["hub.verify_token"] == verifytoken)
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
            FacebookBotRequest objMsgUser = JsonConvert.DeserializeObject<FacebookBotRequest>(body);
            if (objMsgUser.@object != "page")
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            string fbPageId = objMsgUser.entry[0].id;
            var app3rd = _app3rd.GetByPageId(fbPageId);
            if (app3rd == null)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            int botId = app3rd.BotID;
            Setting settingDb = _settingService.GetSettingByBotID(botId);
            pageToken = settingDb.FacebookPageToken;
            appSecret = settingDb.FacebookAppSecrect;
            
            if (!VerifySignature(signature, body))
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

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

            //Kiểm tra duplicate request trong cùng một thời gian
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
            _fbUser = GetUserById(sender, botId);

            //HistoryViewModel hisVm = new HistoryViewModel();

            // Input text
            if (typeRequest == CommonConstants.BOT_REQUEST_TEXT)
            {
                // Thêm dấu tiếng việt
                bool isActive = true;
                string textAccentVN = GetPredictAccentVN(text, isActive);
                if (textAccentVN != text)
                {
                    string msg = FacebookTemplate.GetMessageTemplateText("Ý bạn là: " + textAccentVN + "", sender).ToString();
                    await SendMessage(msg, sender);
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

            // Xét điều kiện đi tiếp hay cần lưu giá trị của thẻ đi trước
            if (_fbUser.IsHaveSetAttributeSystem)
            {
                AttributeFacebookUser attFbUser = new AttributeFacebookUser();
                attFbUser.AttributeKey = _fbUser.AttributeName;
                attFbUser.BotID = botId;
                attFbUser.UserID = sender;
                bool isUpdateAttr = false;
                if (typeRequest == CommonConstants.BOT_REQUEST_TEXT)
                {
                    attFbUser.AttributeValue = text;
                    isUpdateAttr = true;
                }
                if (typeRequest == CommonConstants.BOT_REQUEST_PAYLOAD_POSTBACK)
                {
                    if (!String.IsNullOrEmpty(TITLE_PAYLOAD_QUICKREPLY))
                    {
                        attFbUser.AttributeValue = TITLE_PAYLOAD_QUICKREPLY;
                        isUpdateAttr = true;

                    }
                }
                if (isUpdateAttr)
                {
                    _dicAttributeUser.Remove(attFbUser.AttributeKey);
                    _dicAttributeUser.Add(attFbUser.AttributeKey, attFbUser.AttributeValue);
                    _attributeService.CreateUpdateAttributeFacebook(attFbUser);
                }


                //hisVm.BotID = botId;
                //hisVm.CreatedDate = DateTime.Now;
                //hisVm.UserSay = text;
                //hisVm.UserName = sender;
                //hisVm.Type = CommonConstants.TYPE_FACEBOOK;
                //hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_004;
                //AddHistory(hisVm);

            }
            if (_fbUser.PredicateName == "REQUIRE_CLICK_BUTTON_TO_NEXT_CARD")
            {
                if (typeRequest == CommonConstants.BOT_REQUEST_TEXT)
                {
                    string contentRequireClick = FacebookTemplate.GetMessageTemplateText("Anh/chị vui lòng chọn lại thông tin bên dưới", sender).ToString();
                    await SendMessage(contentRequireClick, sender);
                    string partternCardRequireClick = _fbUser.PredicateValue;
                    string templateCardRequireClick = HandlePostbackCard(partternCardRequireClick, botId);
                    await SendMultiMessageTask(templateCardRequireClick, sender);
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
            else if (_fbUser.PredicateName == "REQUIRE_INPUT_TEXT_TO_NEXT_CARD")
            {
                if (typeRequest == CommonConstants.BOT_REQUEST_TEXT)
                {
                    string partternCardRequireInput = _fbUser.PredicateValue;
                    string templateCardRequireInput = HandlePostbackCard(partternCardRequireInput, botId);
                    await SendMultiMessageTask(templateCardRequireInput, sender);
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
            else if (_fbUser.PredicateName == "VERIFY_TEXT_WITH_AREA_BUTTON")
            {
                if (typeRequest == CommonConstants.BOT_REQUEST_TEXT)
                {
                    var cardDb = _cardService.GetCardByPattern(_fbUser.PredicateValue);
                    if (cardDb == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                    string area = cardDb.Name;
                    text = text + " " + area;
                }
            }
            else if (_fbUser.PredicateName == "POSTBACK_MODULE")
            {
                if (typeRequest == CommonConstants.BOT_REQUEST_PAYLOAD_POSTBACK)
                {
                    _fbUser.PredicateName = "";
                    _fbUser.PredicateValue = "";
                    _fbUser.IsHaveSetAttributeSystem = false;
                    _fbUser.AttributeName = "";
                    UpdateStatusFacebookUser(_fbUser);
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                string postbackModule = _fbUser.PredicateValue;
                string templateModule = HandlePostbackModule(postbackModule, text, botId, false);
                await SendMessage(templateModule, sender);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            // print postback card
            if (typeRequest == CommonConstants.BOT_REQUEST_PAYLOAD_POSTBACK)
            {
                string templateCard = HandlePostbackCard(text, botId);
                await SendMultiMessageTask(templateCard, sender);
                if (_fbUser.PredicateName == "AUTO_NEXT_CARD")
                {
                    string partternNextCard = _fbUser.PredicateValue;
                    string templateNextCard = HandlePostbackCard(partternNextCard, botId);
                    await SendMultiMessageTask(templateNextCard, sender);
                }
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            AIMLbot.Result rsAIMLBot = GetBotReplyFromAIMLBot(text);
            ResultBot rsBOT = new ResultBot();
            rsBOT = CheckTypePostbackFromResultBotReply(rsAIMLBot);
            if (rsBOT.Type == POSTBACK_MODULE)
            {
                string templateModule = HandlePostbackModule(rsBOT.PatternPayload, text, botId, true);
                await SendMessage(templateModule, sender);
            }
            if (rsBOT.Type == POSTBACK_CARD)
            {
                string templateCard = HandlePostbackCard(rsBOT.PatternPayload, botId);
                await SendMultiMessageTask(templateCard, sender);
                if (_fbUser.PredicateName == "AUTO_NEXT_CARD")
                {
                    string partternNextCard = _fbUser.PredicateValue;
                    string templateNextCard = HandlePostbackCard(partternNextCard, botId);
                    await SendMultiMessageTask(templateNextCard, sender);
                }
            }
            if (rsBOT.Type == POSTBACK_NOT_MATCH)
            {
                List<string> lstSymptoms = new List<string>();
                if (botId == 3019)
                {
                    lstSymptoms = GetSymptoms(text);
                    if (lstSymptoms.Count() != 0)
                    {
                        foreach (var symp in lstSymptoms)
                        {
                            await SendMessage(symp, sender);
                        }
                    }
                }
                List<string> lstFaq = new List<string>();
                lstFaq = GetRelatedQuestion(text, "0", "5", botId.ToString());
                if (lstFaq.Count() != 0)
                {
                    foreach (var faq in lstFaq)
                    {
                        await SendMessage(faq, sender);
                    }
                }
                if (lstSymptoms.Count() == 0 && lstFaq.Count() == 0)
                {
                    List<string> keyList = new List<string>(_DICTIONARY_NOT_MATCH.Keys);
                    Random rand = new Random();
                    string randomKey = keyList[rand.Next(keyList.Count)];
                    string contentNotFound = _DICTIONARY_NOT_MATCH[randomKey];
                    string templateNotFound = FacebookTemplate.GetMessageTemplateTextAndQuickReply(
                        contentNotFound, sender, _contactAdmin, _titlePayloadContactAdmin).ToString();
                    await SendMessage(templateNotFound, sender);
                }
            }
            if (rsBOT.Type == POSTBACK_TEXT)
            {
                string templateText = FacebookTemplate.GetMessageTemplateText(rsBOT.PatternPayload, sender).ToString();
                await SendMessage(templateText, sender);
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        private AIMLbot.Result GetBotReplyFromAIMLBot(string text)
        {
            AIMLbot.Result aimlBotResult = _botService.Chat(text);
            return aimlBotResult;
        }
        private ResultBot CheckTypePostbackFromResultBotReply(AIMLbot.Result rsAIMLBot)
        {
            ResultBot rsBot = new ResultBot();
            string result = "";
            if (rsAIMLBot.OutputSentences != null && rsAIMLBot.OutputSentences.Count() != 0)
            {
                result = rsAIMLBot.OutputSentences[0].ToString().Replace(".", "").Trim();

                string strTempPostback = rsAIMLBot.SubQueries[0].Template;
                // nếu nhập text trả về output là postback
                bool isPostbackCard = Regex.Match(strTempPostback, "<template><srai>postback_card_(\\d+)</srai></template>").Success;
                //trường hợp trả về câu hỏi random chứa postback
                bool isPostbackAnswer = Regex.Match(strTempPostback, "<template><srai>postback_answer_(\\d+)</srai></template>").Success;

                if (result.Contains(CommonConstants.POSTBACK_MODULE))
                {
                    string patternPayloadModule = result;

                    rsBot.Type = POSTBACK_MODULE;
                    rsBot.Total = 1;
                    rsBot.PatternPayload = patternPayloadModule;
                    return rsBot;
                }
                else if (isPostbackCard)
                {
                    strTempPostback = Regex.Replace(strTempPostback, @"<(.|\n)*?>", "").Trim();
                    rsBot.Type = POSTBACK_CARD;
                    rsBot.Total = 1;
                    rsBot.PatternPayload = strTempPostback;
                }
                else if (isPostbackAnswer)
                {
                    if (result.Contains(CommonConstants.PostBackCard))
                    {
                        rsBot.Type = POSTBACK_CARD;
                        rsBot.Total = 1;
                        rsBot.PatternPayload = result;
                    }
                }
                else if (result.Contains("NOT_MATCH"))
                {
                    rsBot.Type = POSTBACK_NOT_MATCH;
                    rsBot.Total = 1;
                    rsBot.PatternPayload = "";
                }
                else
                {
                    rsBot.Type = POSTBACK_TEXT;
                    rsBot.Total = 1;
                    rsBot.PatternPayload = result;
                }
            }
            return rsBot;
        }

        #region Handle condition card and module
        private string HandlePostbackCard(string patternCard, int botId)
        {
            _fbUser.PredicateName = "";
            _fbUser.PredicateValue = "";
            _fbUser.IsHaveSetAttributeSystem = false;
            _fbUser.AttributeName = "";
            var cardDb = _cardService.GetCardByPattern(patternCard);
            string tempCardFb = cardDb.TemplateJsonFacebook;

            if (cardDb.TemplateJsonFacebook.Contains("module"))
            {
                var rsAIMLBot = GetBotReplyFromAIMLBot(patternCard);
                string patternModule = rsAIMLBot.OutputSentences[0].ToString().Replace(".", "").Trim();
                return HandlePostbackModule(patternModule, patternModule, botId, true);
            }
            if (!String.IsNullOrEmpty(cardDb.AttributeSystemName))
            {
                _fbUser.IsHaveSetAttributeSystem = true;
                _fbUser.AttributeName = cardDb.AttributeSystemName;
            }
            if (cardDb.IsHaveCondition)
            {
                _fbUser.PredicateName = "REQUIRE_CLICK_BUTTON_TO_NEXT_CARD";
                _fbUser.PredicateValue = patternCard;
            }
            if (cardDb.CardStepID != null && cardDb.IsConditionWithInputText)
            {
                _fbUser.PredicateName = "REQUIRE_INPUT_TEXT_TO_NEXT_CARD";
                _fbUser.PredicateValue = CommonConstants.PostBackCard + cardDb.CardStepID;
            }
            if (cardDb.CardStepID != null && cardDb.IsConditionWithInputText == false)
            {
                _fbUser.PredicateName = "AUTO_NEXT_CARD";
                _fbUser.PredicateValue = CommonConstants.PostBackCard + cardDb.CardStepID;
            }
            if (cardDb.IsConditionWithAreaButton)
            {
                _fbUser.PredicateName = "VERIFY_TEXT_WITH_AREA_BUTTON";
            }
            UpdateStatusFacebookUser(_fbUser);
            return tempCardFb;
        }
        private string HandlePostbackModule(string postbackModule, string text, int botId, bool isFristRequest)
        {
            string templateHandle = "";
            _fbUser.IsHaveSetAttributeSystem = false;
            _fbUser.AttributeName = "";
            _fbUser.PredicateName = "POSTBACK_MODULE";
            if (postbackModule.Contains(CommonConstants.ModuleSearchAPI))
            {
                if (isFristRequest)
                {
                    string mdSearchId = postbackModule.Replace("postback_module_api_search_", "");
                    var handleMdSearch = _handleMdService.HandleIsSearchAPI(postbackModule, mdSearchId, "");
                    templateHandle = handleMdSearch.TemplateJsonFacebook;
                }
                else
                {
                    var handleMdSearch = _handleMdService.HandleIsSearchAPI(text, "", "");
                    templateHandle = handleMdSearch.TemplateJsonFacebook;
                }
                _fbUser.PredicateValue = CommonConstants.ModuleSearchAPI;
            }
            if (postbackModule.Contains(CommonConstants.ModuleAdminContact))
            {
                var handleAdminContact = _handleMdService.HandleIsAdminContact(text, botId);
                templateHandle = handleAdminContact.TemplateJsonFacebook;
                _fbUser.PredicateValue = CommonConstants.ModuleAdminContact;
            }

            UpdateStatusFacebookUser(_fbUser);
            return templateHandle;
        }
        #endregion


        #region Create update and get Facebook user
        private ApplicationFacebookUser UpdateStatusFacebookUser(ApplicationFacebookUser fbUserVm)
        {
            _appFacebookUser.Update(fbUserVm);
            _appFacebookUser.Save();
            return fbUserVm;
        }
        private ApplicationFacebookUser GetUserById(string senderId, int botId)
        {
            ApplicationFacebookUser fbUserDb = new ApplicationFacebookUser();
            fbUserDb = _appFacebookUser.GetByUserId(senderId);
            if (fbUserDb == null)
            {
                ProfileUser profileUser = new ProfileUser();
                profileUser = GetProfileUser(senderId);

                fbUserDb = new ApplicationFacebookUser();
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
                _appFacebookUser.Add(fbUserDb);
                _appFacebookUser.Save();

                AddAttributeDefault(senderId, botId, "sender_name", fbUserDb.UserName);
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
        private async Task SendMultiMessageTask(string templateJson, string sender)
        {
            templateJson = templateJson.Trim();
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
            string[] strArrayJson = Regex.Split(templateJson, "split");
            foreach (var temp in strArrayJson)
            {
                string tempJson = temp;
                await SendMessage(tempJson, sender);
            }
        }
        private async Task SendMessageTyping(string sender)
        {
            string senderActionTyping = FacebookTemplate.GetMessageSenderAction("typing_on", sender).ToString();
            await SendMessage(senderActionTyping, sender);
        }
        private async Task SendMessage(string templateJson, string sender)
        {
            if (!String.IsNullOrEmpty(templateJson))
            {
                templateJson = templateJson.Replace("{{senderId}}", sender);
                templateJson = Regex.Replace(templateJson, "File/", Domain + "File/");
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage res = await client.PostAsync($"https://graph.facebook.com/v3.2/me/messages?access_token=" + pageToken + "",
                    new StringContent(templateJson, Encoding.UTF8, "application/json"));
                }
            }
        }

        #endregion

        #region CALL API SEARCH NATURAL LANGUAGE PROCESS
        private List<string> GetRelatedQuestion(string question, string field, string number, string botId)
        {
            List<string> _lstQuestion = new List<string>();
            string resultAPI = _apiNLR.GetRelatedPair(question, field, number, botId);
            if (!String.IsNullOrEmpty(resultAPI))
            {
                var lstQnaAPI = new JavaScriptSerializer
                {
                    MaxJsonLength = Int32.MaxValue,
                    RecursionLimit = 100
                }.Deserialize<List<SearchNlpQnAViewModel>>(resultAPI);
                // render template json generic
                int totalQnA = lstQnaAPI.Count();
                string totalFind = "Tôi tìm thấy " + totalQnA + " câu hỏi liên quan đến câu hỏi của bạn";
                string strTemplateTextFb = FacebookTemplate.GetMessageTemplateText(totalFind, "{{senderId}}").ToString();
                string strTemplateGenericRelatedQuestion = FacebookTemplate.GetMessageTemplateGenericByList("{{senderId}}", lstQnaAPI).ToString();
                _lstQuestion.Add(strTemplateTextFb);
                _lstQuestion.Add(strTemplateGenericRelatedQuestion);
            }

            return _lstQuestion;
        }
        private List<string> GetSymptoms(string text)
        {
            List<string> _lstSymptoms = new List<string>();
            string resultSymptomp = _apiNLR.GetListSymptoms(text, 1);
            if (!String.IsNullOrEmpty(resultSymptomp))
            {
                var dataSymptomp = new JavaScriptSerializer
                {
                    MaxJsonLength = Int32.MaxValue,
                    RecursionLimit = 100
                }.Deserialize<List<SearchSymptomViewModel>>(resultSymptomp);
                if (dataSymptomp.Count() != 0)
                {
                    string msgSymptoms = "Bạn vui lòng xem thêm thông tin triệu chứng bên dưới";
                    string strTemplateTextFb = FacebookTemplate.GetMessageTemplateText(msgSymptoms, "{{senderId}}").ToString();
                    string strTemplateGenericMedicalSymptoms = FacebookTemplate.GetMessageTemplateGenericByListMed("{{senderId}}", dataSymptomp).ToString();
                    _lstSymptoms.Add(strTemplateTextFb);
                    _lstSymptoms.Add(strTemplateGenericMedicalSymptoms);
                }
            }
            return _lstSymptoms;
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

        private void AddHistory(HistoryViewModel hisVm)
        {
            History hisDb = new History();
            hisDb.UpdateHistory(hisVm);
            _historyService.Create(hisDb);
            _historyService.Save();
        }


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
