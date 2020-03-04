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
//using SearchEngine.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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

namespace BotProject.Web.API_Mobile
{
    public class MessageFbMedicalController : ApiController
    {
        string pageToken = "EAAfPgPELM6IBAAmr0xU2ZBOBxZCTtinA4t75iRAK6V7yZC2NJAgF3c2sZCaJXHcVF39wqu9AbtdFks5vOqx6cRrZCAKUEcZBxxB2hz86EblZBkJRSwVjwGsWXxHXCFFONpY8mRXaN6irO2ZADsaZBucboUjRZCyTzolFDNsbOajdxS0QZDZD";
        string appSecret = "be46264e45777ee8c35bc75d64132c53";
        string _contactAdmin = Helper.ReadString("AdminContact");
        string _titlePayloadContactAdmin = Helper.ReadString("TitlePayloadAdminContact");
        int _timeOut = 60;
        bool _isHaveTimeOut = false;
        //tin nhắn phản hồi chờ
        string _messageProactive = "";
        bool _isSearchAI = false;

        //tin nhắn vắng mặt
        string _messageAbsent = "";
        bool _isHaveMessageAbsent = false;

        string _patternCardPayloadProactive = "";
        string _titleCardPayloadProactive = "🔙 Quay về";

        //OTP
        int _timeOutOTP = 60;
        bool _isHaveTimeOutOTP = false;
        string _messageOTP = "";

        string _cardResendSMS = Helper.ReadString("CARD_RESEND_SMS");

        string _pathStopWord = System.IO.Path.Combine(PathServer.PathNLR, "StopWord.txt");
        string _stopWord = "";

        private const string NumberPattern = @"^\d+$";
        private int age = 10;

        private readonly string Domain = Helper.ReadString("Domain");
        private readonly string UrlAPI = Helper.ReadString("UrlAPI");
        private readonly string KeyAPI = Helper.ReadString("KeyAPI");
        private string pathAIML = PathServer.PathAIML;
        private string pathSetting = PathServer.PathAIML + "config";

        private Dictionary<string, string> _dicNotMatch;


        private Dictionary<string, string> _dicAttributeUser;

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
        //private Bot _bot;
        //private User _user;
        public MessageFbMedicalController(IErrorService errorService,
                              ISettingService settingService,
                              IHandleModuleServiceService handleMdService,
                              IAIMLFileService aimlFileService,
                              IQnAService qnaService,
                              IHistoryService historyService,
                              ICardService cardService,
                              IApplicationFacebookUserService appFacebookUser,
                              IApplicationThirdPartyService app3rd,
                              IAttributeSystemService attributeService)
        {
            _errorService = errorService;
            _settingService = settingService;
            _handleMdService = handleMdService;
            _apiNLR = new ApiQnaNLRService();
            _aimlFileService = aimlFileService;
            _qnaService = qnaService;
            _botService = BotServiceMedical.BotInstance;
            _historyService = historyService;
            _cardService = cardService;
            _appFacebookUser = appFacebookUser;
            _app3rd = app3rd;
            _attributeService = attributeService;
            //_accentService = new AccentService();// AccentService.AccentInstance;
        }

        public HttpResponseMessage Get()
        {
            var querystrings = Request.GetQueryNameValuePairs().ToDictionary(x => x.Key, x => x.Value);
            foreach (var item in querystrings)
            {
                if (item.Key == "hub.verify_token")
                {
                    if (item.Value == "lacviet_bot_chat")
                        return new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new StringContent(querystrings["hub.challenge"], Encoding.UTF8, "text/plain")
                        };
                }
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post()
        {
            int botId = 3019;// y tế

            var signature = Request.Headers.GetValues("X-Hub-Signature").FirstOrDefault().Replace("sha1=", "");
            var body = await Request.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<FacebookBotRequest>(body);
            //BotLog.Info(body);

            //var app3rd = _app3rd.GetByPageId(value.entry[0].id);
            //if (app3rd == null)
            //{
            //    return new HttpResponseMessage(HttpStatusCode.OK);
            //}
            //botId = app3rd.BotID;

            //var settingDb = _settingService.GetSettingByBotID(botId);


            //get pagetoken
            //pageToken = settingDb.FacebookPageToken;
            //appSecret = settingDb.FacebookAppSecrect;
            _patternCardPayloadProactive = "postback_card_8794";// + settingDb.CardID.ToString();

            //init stop word
            //_stopWord = settingDb.StopWord;
            //if (System.IO.File.Exists(_pathStopWord))
            //{
            //    string[] stopWordDefault = System.IO.File.ReadAllLines(_pathStopWord);
            //    _stopWord += string.Join(",", stopWordDefault);
            //}

            // xác nhận app facebook
            if (!VerifySignature(signature, body))
                return new HttpResponseMessage(HttpStatusCode.BadRequest);


            if (value.@object != "page")
                return new HttpResponseMessage(HttpStatusCode.OK);

            //_isHaveTimeOut = settingDb.IsProactiveMessageFacebook;
            //_timeOut = settingDb.Timeout;
            //_messageProactive = settingDb.ProactiveMessageText;
            _isSearchAI = true;// settingDb.IsMDSearch;

            //tin vắng mặt
            //_messageAbsent = settingDb.MessageMaintenance;
            //_isHaveMessageAbsent = settingDb.IsHaveMaintenance;

            //OTP
            //_timeOutOTP = settingDb.TimeoutOTP;
            //_isHaveTimeOutOTP = settingDb.IsHaveTimeoutOTP;
            //_messageOTP = settingDb.MessageTimeoutOTP;

            // mượn tạm biến OTP

            //var lstAIML = _aimlFileService.GetByBotIdAndExcludeFormQnAnwer(botId, null);
            //var lstAIMLVm = Mapper.Map<IEnumerable<AIMLFile>, IEnumerable<AIMLViewModel>>(lstAIML);
            //_botService.loadAIMLFromDatabase(lstAIMLVm);
            //string _userId = Guid.NewGuid().ToString();
            //_user = _botService.loadUserBot(_userId);

            //var lstAIML = _aimlFileService.GetByBotId(botId);
            //var lstAIMLVm = Mapper.Map<IEnumerable<AIMLFile>, IEnumerable<AIMLViewModel>>(lstAIML);
            //_botService.loadAIMLFromDatabase(lstAIMLVm);
            //string _userId = Guid.NewGuid().ToString();
            //_user = _botService.loadUserBot(_userId);

            foreach (var item in value.entry[0].messaging)
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
                    await ExcuteMessage(item.postback.payload, item.sender.id, botId, item.timestamp);
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    if (item.message.quick_reply != null)
                    {
                        await ExcuteMessage(item.message.quick_reply.payload, item.sender.id, botId, item.timestamp);
                        return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                    else if (item.message.attachments != null)
                    {
                        if (item.message.attachments[0].type == "audio")
                        {
                            string urlAudio = item.message.attachments[0].payload.url;
                            //BotLog.Info("URL: " + urlAudio);

                            //string senderActionTyping = FacebookTemplate.GetMessageSenderAction("typing_on", item.sender.id).ToString();
                            //await SendMessageTask(senderActionTyping, item.sender.id);
                            var rsAudioToTextJson = await SpeechReconitionVNService.ConvertSpeechToText(urlAudio);
                            if (String.IsNullOrEmpty(rsAudioToTextJson))
                            {
                                return new HttpResponseMessage(HttpStatusCode.OK);
                            }

                            dynamic stuff = JsonConvert.DeserializeObject(rsAudioToTextJson);
                            string status = stuff.status;
                            if (status == "0")
                            {
                                string text = stuff.hypotheses[0].utterance;
                                if (!String.IsNullOrEmpty(text))
                                {
                                    text = Regex.Replace(text, @"\.", "");
                                }
                                await ExcuteMessage(text, item.sender.id, botId, item.timestamp, "audio");
                                return new HttpResponseMessage(HttpStatusCode.OK);
                            }
                            else if (status == "1")
                            {
                                string meanTextFromAudio = FacebookTemplate.GetMessageTemplateText("Không có âm thanh", item.sender.id).ToString();
                                await SendMessage(meanTextFromAudio, item.sender.id);
                                return new HttpResponseMessage(HttpStatusCode.OK);
                            }
                            else if (status == "2")
                            {
                                string meanTextFromAudio = FacebookTemplate.GetMessageTemplateText("Xử lý âm thanh bị hủy", item.sender.id).ToString();
                                await SendMessage(meanTextFromAudio, item.sender.id);
                                return new HttpResponseMessage(HttpStatusCode.OK);
                            }
                            else if (status == "9")
                            {
                                string meanTextFromAudio = FacebookTemplate.GetMessageTemplateText("Hệ thống xử lý âm thanh đang bận, bạn vui lòng quay lại sau nhé!", item.sender.id).ToString();
                                await SendMessage(meanTextFromAudio, item.sender.id);
                                return new HttpResponseMessage(HttpStatusCode.OK);
                            }
                        }
                        return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                    else
                    {
                        await ExcuteMessage(item.message.text, item.sender.id, botId, item.timestamp);
                        return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        private async Task<HttpResponseMessage> ExcuteMessage(string text, string sender, int botId, string timeStamp = "", string audio = "")
        {
            if (String.IsNullOrEmpty(text))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            if (String.IsNullOrEmpty(sender))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            // Check duplicate request
            if (!String.IsNullOrEmpty(timeStamp))
            {
                var rs = _appFacebookUser.CheckDuplicateRequestWithTimeStamp(timeStamp, sender);
                if (rs == 4)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }

            text = HttpUtility.HtmlDecode(text);
            text = Regex.Replace(text, @"<(.|\n)*?>", "").Trim();
            text = Regex.Replace(text, @"\p{Cs}", "").Trim();// remove emoji

            ApplicationFacebookUser fbUserDb = new ApplicationFacebookUser();
            fbUserDb = _appFacebookUser.GetByUserId(sender);

            string senderActionTyping = FacebookTemplate.GetMessageSenderAction("typing_on", sender).ToString();
            int age = 10;
            //Typing ...
            if (fbUserDb != null && (fbUserDb.PredicateName != "Admin_Contact"))
            {
                if (fbUserDb.Age != 0)
                {
                    bool isAge = Regex.Match(text, NumberPattern).Success;
                    if (isAge)
                    {
                        age = fbUserDb.Age;
                    }
                }
                await SendMessageTask(senderActionTyping, sender);
            }

            if (!text.Contains("postback") && !text.Contains(_contactAdmin))
            {
                _accentService = new AccentService();
                string textAccentVN = _accentService.GetAccentVN(text);

                if (textAccentVN != text)
                {
                    string msg = FacebookTemplate.GetMessageTemplateText("Ý bạn là: " + textAccentVN + "", sender).ToString();
                    await SendMessageTask(msg, sender);
                }
                text = textAccentVN;

                AddAttributeDefault(sender, botId, "content_message", text);
                _dicAttributeUser.Remove("content_message");
                _dicAttributeUser.Add("content_message", text);
            }

            string attributeValueFromPostback = "";
            // Xét payload postback nếu postback từ quickreply sẽ chứa thêm sperator - và tiêu đề nút
            if (text.Contains("postback"))
            {
                var arrPostback = Regex.Split(text, "-");
                if (arrPostback.Length > 1)
                {
                    attributeValueFromPostback = arrPostback[1];
                }
                text = arrPostback[0];
            }

            HistoryViewModel hisVm = new HistoryViewModel();
            //hisVm.BotID = botId;
            //hisVm.CreatedDate = DateTime.Now;
            //hisVm.UserSay = text;
            //hisVm.UserName = sender;
            //hisVm.Type = CommonConstants.TYPE_FACEBOOK;

            DateTime dStartedTime = DateTime.Now;
            DateTime dTimeOut = DateTime.Now.AddSeconds(_timeOut);

            if (text == "postback_card_4031")// thẻ chào hỏi
            {
                if (fbUserDb.Age == 0)//nếu thông tin tuổi chưa có trả về thẻ hỏi thông tin
                {
                    text = "postback_card_8917";//trả về thẻ hỏi thông tin
                }               
            }
            try
            {
                if (fbUserDb != null)
                {
                    // chat với admin
                    if (fbUserDb.PredicateName == "Admin_Contact")
                    {
                        var handleAdminContact = _handleMdService.HandleIsAdminContact(text, botId);
                        hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_004;
                        //AddHistory(hisVm);
                        if (text.Contains("postback") || text.Contains(_contactAdmin))
                        {
                            fbUserDb.IsHavePredicate = false;
                            fbUserDb.PredicateName = "";
                            fbUserDb.PredicateValue = "";
                            fbUserDb.IsHaveCardCondition = false;
                            fbUserDb.CardConditionPattern = "";
                            fbUserDb.IsConditionWithAreaButton = false;
                            fbUserDb.CardConditionAreaButtonPattern = "";
                            fbUserDb.CardStepPattern = "";
                            fbUserDb.AttributeName = "";
                            fbUserDb.IsHaveSetAttributeSystem = false;
                            fbUserDb.IsConditionWithInputText = false;
                            fbUserDb.CardConditionWithInputTextPattern = "";
                            _appFacebookUser.Update(fbUserDb);
                            _appFacebookUser.Save();
                            return await ExcuteMessage(text, sender, botId);
                        }
                        if (_isHaveMessageAbsent)
                        {
                            if (HelperMethods.IsTimeInWorks() == false)
                            {
                                //await SendMessageTask(FacebookTemplate.GetMessageTemplateTextAndQuickReply(_messageAbsent, "{{senderId}}", _patternCardPayloadProactive, _titleCardPayloadProactive).ToString(),sender);
                                return new HttpResponseMessage(HttpStatusCode.OK);
                            }
                        }
                        if (handleAdminContact.Status == false)
                        {
                            string[] strArrayJson = Regex.Split(handleAdminContact.TemplateJsonFacebook, "split");
                            if (strArrayJson.Length != 0)
                            {
                                var strArray = strArrayJson.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                                foreach (var temp in strArray)
                                {
                                    string tempJson = temp;
                                    await SendMessageTask(tempJson, sender);
                                }
                                return new HttpResponseMessage(HttpStatusCode.OK);
                            }
                        }
                        return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }

                //xử lý audio
                if (!String.IsNullOrEmpty(audio))
                {
                    string meanTextFromAudio = FacebookTemplate.GetMessageTemplateText("Ý của bạn là: " + text, sender).ToString();
                    await SendMessageTask(meanTextFromAudio, sender);
                    await SendMessageTask(senderActionTyping, sender);
                }

                if (fbUserDb == null)
                {
                    ProfileUser profileUser = new ProfileUser();
                    profileUser = GetProfileUser(sender);

                    fbUserDb = new ApplicationFacebookUser();
                    ApplicationFacebookUserViewModel fbUserVm = new ApplicationFacebookUserViewModel();
                    fbUserDb.UserId = sender;
                    fbUserDb.IsHavePredicate = false;
                    fbUserDb.IsProactiveMessage = false;
                    fbUserDb.TimeOut = dTimeOut;
                    fbUserDb.CreatedDate = DateTime.Now;
                    fbUserDb.StartedOn = dStartedTime;
                    fbUserDb.FirstName = profileUser.first_name;
                    fbUserDb.Age = 0;// "N/A";
                    fbUserDb.LastName = profileUser.last_name;
                    fbUserDb.UserName = profileUser.first_name + " " + profileUser.last_name;
                    fbUserDb.Gender = true; //"N/A";
                    fbUserDb.UpdateFacebookUser(fbUserVm);
                    _appFacebookUser.Add(fbUserDb);
                    _appFacebookUser.Save();

                    // add attribute defailt user facebook
                    AddAttributeDefault(sender, botId, "sender_id", fbUserVm.UserId);
                    AddAttributeDefault(sender, botId, "sender_name", fbUserDb.UserName);
                    AddAttributeDefault(sender, botId, "sender_first_name", fbUserDb.FirstName);
                    AddAttributeDefault(sender, botId, "sender_last_name", fbUserDb.LastName);
                    AddAttributeDefault(sender, botId, "gender", "N/A");
                }
                else
                {
                    fbUserDb.StartedOn = dStartedTime;
                    fbUserDb.TimeOut = dTimeOut;

                    // Nếu có yêu cầu click thẻ để đi theo luồng
                    if (fbUserDb.IsHaveCardCondition)
                    {
                        if (!text.Contains("postback") && !text.Contains(_contactAdmin))
                        {
                            var cardDb = _cardService.GetCardByPattern(fbUserDb.CardConditionPattern);
                            if (cardDb == null)
                            {
                                return new HttpResponseMessage(HttpStatusCode.OK);
                            }
                            string tempJsonFacebook = cardDb.TemplateJsonFacebook;
                            if (!String.IsNullOrEmpty(tempJsonFacebook))
                            {
                                tempJsonFacebook = tempJsonFacebook.Trim();
                                string[] strArrayJson = Regex.Split(tempJsonFacebook, "split");
                                if (strArrayJson.Length != 0)
                                {
                                    await SendMessageTask(FacebookTemplate.GetMessageTemplateText("Anh/chị vui lòng chọn lại thông tin bên dưới", sender).ToString(), sender);
                                    var strArray = strArrayJson.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                                    foreach (var temp in strArray)
                                    {
                                        string tempJson = temp;
                                        await SendMessageTask(tempJson, sender);
                                    }
                                    return new HttpResponseMessage(HttpStatusCode.OK);
                                }
                            }
                        }
                    }

                    // AttributeFacebook nếu thẻ trước có biến cần lưu
                    if (fbUserDb.IsHaveSetAttributeSystem)
                    {
                        AttributeFacebookUser attFbUser = new AttributeFacebookUser();
                        attFbUser.AttributeKey = fbUserDb.AttributeName;
                        //attFbUser.AttributeValue = text;
                        attFbUser.BotID = botId;
                        attFbUser.UserID = sender;
                        if (!text.Contains("postback") && !text.Contains(_contactAdmin))
                        {
                            attFbUser.AttributeValue = text;
                        }
                        else if (text.Contains("postback"))
                        {
                            attFbUser.AttributeValue = attributeValueFromPostback.Trim();
                        }

                        if (attFbUser.AttributeKey == "age")
                        {
                            bool isAge = Regex.Match(text, NumberPattern).Success;
                            if (isAge)
                            {
                                attFbUser.AttributeValue = text;
                                if(Int32.Parse(text) == 0)
                                {
                                    await SendMessageTask(FacebookTemplate.GetMessageTemplateText("Anh/chị vui lòng nhập lại độ tuổi chính xác", sender).ToString(), sender);
                                    return new HttpResponseMessage(HttpStatusCode.OK);
                                }
                            }
                            else
                            {
                                await SendMessageTask(FacebookTemplate.GetMessageTemplateText("Ký tự phải là số, Anh/chị vui lòng nhập lại độ tuổi", sender).ToString(), sender);
                                return new HttpResponseMessage(HttpStatusCode.OK);
                            }
                        }

                        _dicAttributeUser.Remove(attFbUser.AttributeKey);
                        _dicAttributeUser.Add(attFbUser.AttributeKey, attFbUser.AttributeValue);

                        var att = _attributeService.CreateUpdateAttributeFacebook(attFbUser);

                        if (attFbUser.AttributeKey == "age")
                        {
                            fbUserDb.Age = Int32.Parse(text);
                            fbUserDb.IsHaveSetAttributeSystem = false;
                            fbUserDb.AttributeName = "";
                            _appFacebookUser.Update(fbUserDb);
                            _appFacebookUser.Save();

                            _dicAttributeUser.Remove("age");
                            _dicAttributeUser.Add("age", text);
                            //return await ExcuteMessage("postback_card_8927", sender, botId); //postback_card_8927 thẻ thông tin người dùng
                        }
                    }

                    // Nhập text để đi luồng tiếp theo nhưng CardStepID không được rỗng
                    if (fbUserDb.IsConditionWithInputText)
                    {
                        if (!text.Contains("postback") && !text.Contains(_contactAdmin))
                        {
                            fbUserDb.IsHaveSetAttributeSystem = false;
                            fbUserDb.AttributeName = "";
                            fbUserDb.IsConditionWithInputText = false;
                            fbUserDb.IsHaveCardCondition = false;
                            _appFacebookUser.Update(fbUserDb);
                            _appFacebookUser.Save();
                            return await ExcuteMessage(fbUserDb.CardConditionWithInputTextPattern, sender, botId); //postback_card_8927 thẻ thông tin người dùng
                        }
                    }
                    // Nếu có yêu cầu query text theo lĩnh vực button
                    // Click button -> card (tên card nên đặt như tên lĩnh vực ngắn gọn)
                    // Build lại kịch bản với từ khoán ngắn gọn + tên lĩnh vực
                    // ví dụ: thủ tục cấp phép, thủ tục giạn + tên lĩnh vực
                    if (fbUserDb.IsConditionWithAreaButton)
                    {
                        if (!text.Contains("postback") && !text.Contains(_contactAdmin))
                        {
                            var cardDb = _cardService.GetCardByPattern(fbUserDb.CardConditionAreaButtonPattern);
                            if (cardDb == null)
                            {
                                return new HttpResponseMessage(HttpStatusCode.OK);
                            }
                            string area = cardDb.Name;
                            text = text + " " + area;// + thêm tên lĩnh vực button và nội dung trong form QnA có chứa từ lĩnh vực
                        }
                    }

                    // Điều kiện xử lý module
                    if (fbUserDb.IsHavePredicate)
                    {
                        var predicateName = fbUserDb.PredicateName;
                        if (predicateName == "ApiSearch")
                        {
                            if (text.Contains("postback_card") || text.Contains(_contactAdmin))// nều còn điều kiện search mà chọn postback
                            {
                                fbUserDb.IsHavePredicate = false;
                                fbUserDb.PredicateName = "";
                                fbUserDb.PredicateValue = "";
                                fbUserDb.IsHaveCardCondition = false;
                                fbUserDb.CardConditionPattern = "";
                                fbUserDb.IsConditionWithAreaButton = false;
                                fbUserDb.CardConditionAreaButtonPattern = "";
                                fbUserDb.CardStepPattern = "";
                                fbUserDb.AttributeName = "";
                                fbUserDb.IsHaveSetAttributeSystem = false;
                                fbUserDb.IsConditionWithInputText = false;
                                fbUserDb.CardConditionWithInputTextPattern = "";
                                _appFacebookUser.Update(fbUserDb);
                                _appFacebookUser.Save();
                                return await ExcuteMessage(text, sender, botId);
                            }

                            string predicateValue = fbUserDb.PredicateValue;
                            var handleMdSearch = _handleMdService.HandleIsSearchAPI(text, predicateValue, "");

                            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_005;
                            //AddHistory(hisVm);

                            return await SendMessage(handleMdSearch.TemplateJsonFacebook, sender);

                        }
                    }
                    else // Input: Khởi tạo module được chọn
                    {
                        if (text.Contains(CommonConstants.ModuleAdminContact))
                        {
                            var handleAdminContact = _handleMdService.HandleIsAdminContact(text, botId);

                            fbUserDb.IsHavePredicate = true;
                            fbUserDb.PredicateName = "Admin_Contact";
                            fbUserDb.PredicateValue = "";
                            fbUserDb.IsHaveCardCondition = false;
                            fbUserDb.CardConditionPattern = "";
                            fbUserDb.IsConditionWithAreaButton = false;
                            fbUserDb.CardConditionAreaButtonPattern = "";
                            fbUserDb.CardStepPattern = "";
                            fbUserDb.IsHaveSetAttributeSystem = false;
                            fbUserDb.AttributeName = "";
                            fbUserDb.IsConditionWithInputText = false;
                            fbUserDb.CardConditionWithInputTextPattern = "";
                            _appFacebookUser.Update(fbUserDb);
                            _appFacebookUser.Save();

                            hisVm.UserSay = "[Chat với chuyên viên]";
                            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_003;
                            //AddHistory(hisVm);

                            // Tin nhắn vắng mặt
                            if (_isHaveMessageAbsent)
                            {
                                if (HelperMethods.IsTimeInWorks() == false)
                                {
                                    await SendMessageTask(FacebookTemplate.GetMessageTemplateTextAndQuickReply(_messageAbsent, "{{senderId}}", _patternCardPayloadProactive, _titleCardPayloadProactive).ToString(), sender);
                                    return new HttpResponseMessage(HttpStatusCode.OK);
                                }
                            }

                            string[] strArrayJson = Regex.Split(handleAdminContact.TemplateJsonFacebook, "split");//nhớ thêm bên formcard xử lý lục trên face
                            if (strArrayJson.Length != 0)
                            {
                                var strArray = strArrayJson.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                                foreach (var temp in strArray)
                                {
                                    string tempJson = temp;
                                    await SendMessageTask(tempJson, sender);
                                }
                                return new HttpResponseMessage(HttpStatusCode.OK);
                            }

                            return new HttpResponseMessage(HttpStatusCode.OK);
                        }

                        if (text.Contains(CommonConstants.ModuleSearchAPI))
                        {
                            string mdSearchId = text.Replace(".", String.Empty).Replace("postback_module_api_search_", "");
                            var handleMdSearch = _handleMdService.HandleIsSearchAPI(text, mdSearchId, "");
                            fbUserDb.IsHavePredicate = true;
                            fbUserDb.PredicateName = "ApiSearch";
                            fbUserDb.PredicateValue = mdSearchId;
                            fbUserDb.IsHaveCardCondition = false;
                            fbUserDb.CardConditionPattern = "";
                            fbUserDb.IsConditionWithAreaButton = false;
                            fbUserDb.CardConditionAreaButtonPattern = "";
                            fbUserDb.CardStepPattern = "";
                            fbUserDb.IsHaveSetAttributeSystem = false;
                            fbUserDb.AttributeName = "";
                            fbUserDb.IsConditionWithInputText = false;
                            fbUserDb.CardConditionWithInputTextPattern = "";
                            _appFacebookUser.Update(fbUserDb);
                            _appFacebookUser.Save();

                            hisVm.UserSay = "[Tra cứu]";
                            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_003;
                            //AddHistory(hisVm);

                            return await SendMessage(handleMdSearch.TemplateJsonFacebook, sender);

                        }
                    }
                }

                if (!text.Contains("postback") && !text.Contains(_contactAdmin))
                {
                    if (age <= 7)
                    {
                        text = "trẻ em " + text;
                    }
                }

                AIMLbot.Result aimlBotResult = _botService.Chat(text);
                string result = aimlBotResult.OutputSentences[0].ToString();

                // Nếu trả về là module
                if (result.Replace("\r\n", "").Trim().Contains(CommonConstants.POSTBACK_MODULE))
                {
                    if (result.Contains("<module>") != true)// k phải button module trả về
                    {
                        string txtModule = result.Replace("\r\n", "").Replace(".", "").Trim();
                        txtModule = Regex.Replace(txtModule, @"<(.|\n)*?>", "").Trim();
                        int idxModule = txtModule.IndexOf("postback_module");
                        if (idxModule != -1)
                        {
                            string strPostback = txtModule.Substring(idxModule, txtModule.Length - idxModule);
                            var punctuation = strPostback.Where(Char.IsPunctuation).Distinct().ToArray();
                            var words = strPostback.Split().Select(x => x.Trim(punctuation));
                            var contains = words.SingleOrDefault(x => x.Contains("postback_module") == true);

                            if (words.ToList().Count == 1 && (txtModule.Length == contains.Length))
                            {
                                return await ExcuteMessage(contains, sender, botId);
                            }

                            string rsHandle = "";

                            if (contains == "postback_module_api_search")
                            {
                                return await ExcuteMessage(txtModule, sender, botId);
                            }
                            if (contains == "postback_module_med_get_info_patient")
                            {
                                return await ExcuteMessage(txtModule, sender, botId);
                            }
                            if (contains == "postback_module_age")
                            {
                                fbUserDb.PredicateName = "Age";
                                var handleAge = _handleMdService.HandledIsAge(contains, botId);
                                rsHandle = handleAge.TemplateJsonFacebook;
                            }
                            if (contains == "postback_module_email")
                            {
                                fbUserDb.PredicateName = "Email";
                                var handleEmail = _handleMdService.HandledIsEmail(contains, botId);
                                rsHandle = handleEmail.TemplateJsonFacebook;
                            }
                            if (contains == "postback_module_phone")
                            {
                                fbUserDb.PredicateName = "Phone";
                                var handlePhone = _handleMdService.HandleIsPhoneNumber(contains, botId);
                                rsHandle = handlePhone.TemplateJsonFacebook;
                            }
                            fbUserDb.IsHavePredicate = true;
                            fbUserDb.PredicateValue = "";
                            fbUserDb.IsHaveCardCondition = false;
                            fbUserDb.CardConditionPattern = "";
                            fbUserDb.CardStepPattern = "";
                            fbUserDb.IsHaveSetAttributeSystem = false;
                            fbUserDb.AttributeName = "";
                            fbUserDb.IsConditionWithInputText = false;
                            fbUserDb.CardConditionWithInputTextPattern = "";
                            _appFacebookUser.Update(fbUserDb);
                            _appFacebookUser.Save();

                            return await SendMessage(rsHandle, sender);
                        }
                    }
                }

                if (result.Contains("NOT_MATCH"))
                {
                    hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_002;
                    //AddHistory(hisVm);
                    try
                    {
                        fbUserDb.IsHaveCardCondition = false;
                        fbUserDb.CardConditionPattern = "";
                        fbUserDb.IsConditionWithAreaButton = false;
                        fbUserDb.CardConditionAreaButtonPattern = "";
                        fbUserDb.CardStepPattern = "";
                        fbUserDb.IsHaveSetAttributeSystem = false;
                        fbUserDb.AttributeName = "";
                        fbUserDb.IsConditionWithInputText = false;
                        fbUserDb.CardConditionWithInputTextPattern = "";
                        _appFacebookUser.Update(fbUserDb);
                        _appFacebookUser.Save();
                    }
                    catch (Exception ex)
                    {
                        LogError("RS NOT MATCH:" + ex.Message);
                    }

                    if (_isSearchAI) //_isSearchAI
                    {
                        var systemConfigDb = _settingService.GetListSystemConfigByBotId(botId);
                        var systemConfigVm = Mapper.Map<IEnumerable<BotProject.Model.Models.SystemConfig>, IEnumerable<SystemConfigViewModel>>(systemConfigDb);
                        if (systemConfigVm.Count() == 0)
                        {
                            return await SendMessage(FacebookTemplate.GetMessageTemplateText("Tìm kiếm xử lý ngôn ngữ tự nhiên hiện không hoạt động, bạn vui lòng thử lại sau nhé!", sender));// not match
                        }
                        string nameFunctionAPI = "";
                        string number = "";
                        string field = "";
                        string valueBotId = "";
                        foreach (var item in systemConfigVm)
                        {
                            if (item.Code == "UrlAPI")
                                nameFunctionAPI = item.ValueString;
                            if (item.Code == "ParamBotID")
                                valueBotId = item.ValueString;
                            if (item.Code == "ParamAreaID")
                                field = item.ValueString;
                            if (item.Code == "ParamNumberResponse")
                                number = item.ValueString;
                        }
                        hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_006;
                        //AddHistory(hisVm);

                        // Hiển thị thêm thông tin về triệu chứng đó
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
                                string msgFAQs = "Bạn vui lòng xem thêm thông tin triệu chứng bên dưới";
                                await SendMessageSymptom(FacebookTemplate.GetMessageTemplateText(msgFAQs, sender).ToString(), sender);
                                string strTemplateGenericMedicalSymptoms = FacebookTemplate.GetMessageTemplateGenericByListMed(sender, dataSymptomp).ToString();
                                await SendMessageTask(strTemplateGenericMedicalSymptoms, sender);

                                //string msgSymptompDes = Regex.Replace(dataSymptomp[0].description, "  ", "<br/>");
                                //await SendMessageSymptom(FacebookTemplate.GetMessageTemplateText(msgSymptompDes, sender).ToString(), sender);
                                //string msgCause = Regex.Replace("Nguyên nhân: <br/>" + dataSymptomp[0].cause, "  ", "<br/>");
                                //if (!String.IsNullOrEmpty(dataSymptomp[0].cause) && dataSymptomp[0].cause != "NULL")
                                //{
                                //    await SendMessageSymptom(FacebookTemplate.GetMessageTemplateText(msgCause, sender).ToString(), sender);
                                //}
                                //string msgTreament = Regex.Replace("Chữa trị cấp thời: <br/>" + dataSymptomp[0].treatment, "  ", "<br/>");
                                //if (!String.IsNullOrEmpty(dataSymptomp[0].treatment) && dataSymptomp[0].treatment != "NULL")
                                //{
                                //    await SendMessageSymptom(FacebookTemplate.GetMessageTemplateText(msgTreament, sender).ToString(), sender);

                                //}
                                //string msgAdvice = Regex.Replace("Khi nào cần đi bác sỹ: <br/>" + dataSymptomp[0].advice, "  ", "<br/>");
                                //if (!String.IsNullOrEmpty(dataSymptomp[0].advice) && dataSymptomp[0].advice != "NULL")
                                //{
                                //    await SendMessageSymptom(FacebookTemplate.GetMessageTemplateText(msgAdvice, sender).ToString(), sender);
                                //}
                                //return new HttpResponseMessage(HttpStatusCode.OK);
                            }
                        }

                        string resultAPI = GetRelatedQuestionToFacebook(nameFunctionAPI, text, field, "5", valueBotId);
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
                            await SendMessageTask(FacebookTemplate.GetMessageTemplateText(totalFind, sender).ToString(), sender);
                            string strTemplateGenericRelatedQuestion = FacebookTemplate.GetMessageTemplateGenericByList(sender, lstQnaAPI).ToString();
                            await SendMessageTask(strTemplateGenericRelatedQuestion, sender);
                            //BotLog.Info(strTemplateGenericRelatedQuestion);
                            //return await SendMessage(strTemplateGenericRelatedQuestion, sender);
                            return new HttpResponseMessage(HttpStatusCode.OK);
                        }
                        
                        if (!String.IsNullOrEmpty(resultSymptomp))
                        {
                            return new HttpResponseMessage(HttpStatusCode.OK);
                        }
                    }

                    _dicNotMatch = new Dictionary<string, string>() {
                        {"NOT_MATCH_01", "Xin lỗi,em chưa hiểu ý anh/chị ạ!"},
                        {"NOT_MATCH_02", "Anh/chị có thể giải thích thêm được không?"},
                        {"NOT_MATCH_03", "Chưa hiểu lắm ạ, anh/chị có thể nói rõ hơn được không ạ?"},
                        {"NOT_MATCH_04", "Xin lỗi, Anh/chị có thể giải thích thêm được không?"},
                        {"NOT_MATCH_05", "Xin lỗi, Tôi chưa được học để hiểu nội dung này?"}
                    };

                    string strDefaultNotMatch = "";
                    foreach (var item in _dicNotMatch)
                    {
                        string itemNotMatch = item.Key;
                        if (itemNotMatch.Contains(result.Trim().Replace(".", String.Empty)))
                        {
                            strDefaultNotMatch = item.Value;
                        }
                    }
                    await SendMessage(FacebookTemplate.GetMessageTemplateTextAndQuickReply(strDefaultNotMatch, sender, _contactAdmin, _titlePayloadContactAdmin));// not match
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }

                // input là postback
                if (text.Contains("postback_card"))
                {
                    var cardDb = _cardService.GetCardByPattern(text.Replace(".", String.Empty));
                    if (!String.IsNullOrEmpty(cardDb.AttributeSystemName))
                    {
                        fbUserDb.IsHaveSetAttributeSystem = true;
                        fbUserDb.AttributeName = cardDb.AttributeSystemName;
                    }
                    else
                    {
                        fbUserDb.IsHaveSetAttributeSystem = false;
                        fbUserDb.AttributeName = "";
                    }

                    if (cardDb.CardStepID != null)
                    {
                        fbUserDb.CardStepPattern = "postback_card_" + cardDb.CardStepID;
                        if (cardDb.IsConditionWithInputText)// yêu cầu nhập text để chuyển sang card step
                        {
                            fbUserDb.IsConditionWithInputText = true;
                            fbUserDb.CardConditionWithInputTextPattern = fbUserDb.CardStepPattern;
                        }
                        else
                        {
                            fbUserDb.IsConditionWithInputText = false;
                            fbUserDb.CardConditionWithInputTextPattern = "";
                        }
                    }
                    else
                    {
                        fbUserDb.CardStepPattern = "";
                    }

                    if (cardDb.IsHaveCondition)
                    {
                        fbUserDb.IsHaveCardCondition = true;
                        fbUserDb.CardConditionPattern = text.Replace(".", String.Empty);
                    }
                    else
                    {
                        fbUserDb.IsHaveCardCondition = false;
                        fbUserDb.CardConditionPattern = "";
                    }

                    if (cardDb.IsConditionWithAreaButton)
                    {
                        fbUserDb.IsConditionWithAreaButton = true;
                        fbUserDb.CardConditionAreaButtonPattern = text.Replace(".", String.Empty);
                    }
                    else
                    {
                        fbUserDb.IsConditionWithAreaButton = false;
                        fbUserDb.CardConditionAreaButtonPattern = "";
                    }



                    _appFacebookUser.Update(fbUserDb);
                    _appFacebookUser.Save();
                    string tempJsonFacebook = cardDb.TemplateJsonFacebook;
                    if (!String.IsNullOrEmpty(tempJsonFacebook))
                    {
                        tempJsonFacebook = tempJsonFacebook.Trim();
                        string[] strArrayJson = Regex.Split(tempJsonFacebook, "split");//nhớ thêm bên formcard xử lý lục trên face
                        if (strArrayJson.Length != 0)
                        {
                            var strArray = strArrayJson.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                            foreach (var temp in strArray)
                            {
                                string tempJson = temp;
                                await SendMessageTask(tempJson, sender);
                                if (tempJson.Contains("Nguyên nhân") || tempJson.Contains("bác sĩ") || tempJson.Contains("Bác sĩ"))
                                {
                                    //GetMessageSymptom(_dicAttributeUser["content_message"], sender);
                                    string resultSymptomp = _apiNLR.GetListSymptoms(_dicAttributeUser["content_message"], 1);
                                    if (!String.IsNullOrEmpty(resultSymptomp))
                                    {
                                        var dataSymptomp = new JavaScriptSerializer
                                        {
                                            MaxJsonLength = Int32.MaxValue,
                                            RecursionLimit = 100
                                        }.Deserialize<List<SearchSymptomViewModel>>(resultSymptomp);
                                        if (dataSymptomp.Count() != 0)
                                        {
                                            string msgFAQs = "Bạn vui lòng xem thêm thông tin triệu chứng bên dưới";
                                            await SendMessageSymptom(FacebookTemplate.GetMessageTemplateText(msgFAQs, sender).ToString(), sender);
                                            string strTemplateGenericMedicalSymptoms = FacebookTemplate.GetMessageTemplateGenericByListMed(sender, dataSymptomp).ToString();
                                            await SendMessageTask(strTemplateGenericMedicalSymptoms, sender);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (cardDb.CardStepID != null && cardDb.IsConditionWithInputText == false)
                    {
                        fbUserDb.CardStepPattern = "postback_card_" + cardDb.CardStepID;
                        return await ExcuteMessage(fbUserDb.CardStepPattern, sender, botId);
                    }

                    return new HttpResponseMessage(HttpStatusCode.OK);
                }

                if (text.Contains(_contactAdmin))//chat admin
                {
                    fbUserDb.IsHaveCardCondition = false;
                    fbUserDb.CardConditionPattern = "";
                    fbUserDb.CardStepPattern = "";
                    fbUserDb.IsHaveSetAttributeSystem = false;

                    _appFacebookUser.Update(fbUserDb);
                    _appFacebookUser.Save();

                    string strTempPostbackContactAdmin = aimlBotResult.SubQueries[0].Template;
                    bool isPostbackContactAdmin = Regex.Match(strTempPostbackContactAdmin, "<template><srai>postback_card_(\\d+)</srai></template>").Success;
                    if (isPostbackContactAdmin)
                    {
                        strTempPostbackContactAdmin = Regex.Replace(strTempPostbackContactAdmin, @"<(.|\n)*?>", "").Trim();
                        var cardDb = _cardService.GetCardByPattern(strTempPostbackContactAdmin.Replace(".", String.Empty));
                        string tempJsonFacebook = cardDb.TemplateJsonFacebook;
                        if (!String.IsNullOrEmpty(tempJsonFacebook))
                        {
                            tempJsonFacebook = tempJsonFacebook.Trim();
                            string[] strArrayJson = Regex.Split(tempJsonFacebook, "split");//nhớ thêm bên formcard xử lý lục trên face
                            if (strArrayJson.Length != 0)
                            {
                                var strArray = strArrayJson.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                                foreach (var temp in strArray)
                                {
                                    string tempJson = temp;
                                    await SendMessageTask(tempJson, sender);
                                }
                                return new HttpResponseMessage(HttpStatusCode.OK);
                            }
                        }
                    }
                }

                // nếu nhập text -> output là postback
                string strTempPostback = aimlBotResult.SubQueries[0].Template;
                bool isPostback = Regex.Match(strTempPostback, "<template><srai>postback_card_(\\d+)</srai></template>").Success;
                if (isPostback)
                {
                    strTempPostback = Regex.Replace(strTempPostback, @"<(.|\n)*?>", "").Trim();
                    var cardDb = _cardService.GetCardByPattern(strTempPostback.Replace(".", String.Empty));
                    if (cardDb.ID == 4031)
                    {
                        if (fbUserDb.Age == 0)//nếu thông tin tuổi chưa có trả về thẻ hỏi thông tin
                        {
                            return await ExcuteMessage("postback_card_8917", sender, botId);
                        }
                    }

                    if (!String.IsNullOrEmpty(cardDb.AttributeSystemName))
                    {
                        fbUserDb.IsHaveSetAttributeSystem = true;
                        fbUserDb.AttributeName = cardDb.AttributeSystemName;
                    }
                    else
                    {
                        fbUserDb.IsHaveSetAttributeSystem = false;
                        fbUserDb.AttributeName = "";
                    }

                    if (cardDb.CardStepID != null)
                    {
                        fbUserDb.CardStepPattern = "postback_card_" + cardDb.CardStepID;
                        if (cardDb.IsConditionWithInputText)// yêu cầu nhập text để chuyển sang card step
                        {
                            fbUserDb.IsConditionWithInputText = true;
                            fbUserDb.CardConditionWithInputTextPattern = fbUserDb.CardStepPattern;
                        }
                        else
                        {
                            fbUserDb.IsConditionWithInputText = false;
                            fbUserDb.CardConditionWithInputTextPattern = "";
                        }
                    }
                    else
                    {
                        fbUserDb.CardStepPattern = "";
                    }

                    if (cardDb.IsHaveCondition)
                    {
                        fbUserDb.IsHaveCardCondition = true;
                        fbUserDb.CardConditionPattern = text.Replace(".", String.Empty);
                    }
                    else
                    {
                        fbUserDb.IsHaveCardCondition = false;
                        fbUserDb.CardConditionPattern = "";
                    }

                    if (cardDb.IsConditionWithAreaButton)
                    {
                        fbUserDb.IsConditionWithAreaButton = true;
                        fbUserDb.CardConditionAreaButtonPattern = text.Replace(".", String.Empty);
                    }
                    else
                    {
                        fbUserDb.IsConditionWithAreaButton = false;
                        fbUserDb.CardConditionAreaButtonPattern = "";
                    }
                    _appFacebookUser.Update(fbUserDb);
                    _appFacebookUser.Save();
                    string tempJsonFacebook = cardDb.TemplateJsonFacebook;
                    if (!String.IsNullOrEmpty(tempJsonFacebook))
                    {
                        tempJsonFacebook = tempJsonFacebook.Trim();
                        string[] strArrayJson = Regex.Split(tempJsonFacebook, "split");//nhớ thêm bên formcard xử lý lục trên face
                        if (strArrayJson.Length != 0)
                        {
                            var strArray = strArrayJson.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                            foreach (var temp in strArray)
                            {
                                string tempJson = temp;
                                await SendMessageTask(tempJson, sender);
                                if (tempJson.Contains("Nguyên nhân") || tempJson.Contains("bác sĩ") || tempJson.Contains("Bác sĩ"))
                                {
                                    //GetMessageSymptom(_dicAttributeUser["content_message"], sender);
                                    string resultSymptomp = _apiNLR.GetListSymptoms(_dicAttributeUser["content_message"], 1);
                                    if (!String.IsNullOrEmpty(resultSymptomp))
                                    {
                                        var dataSymptomp = new JavaScriptSerializer
                                        {
                                            MaxJsonLength = Int32.MaxValue,
                                            RecursionLimit = 100
                                        }.Deserialize<List<SearchSymptomViewModel>>(resultSymptomp);
                                        if (dataSymptomp.Count() != 0)
                                        {
                                            string msgFAQs = "Bạn vui lòng xem thêm thông tin triệu chứng bên dưới";
                                            await SendMessageSymptom(FacebookTemplate.GetMessageTemplateText(msgFAQs, sender).ToString(), sender);
                                            string strTemplateGenericMedicalSymptoms = FacebookTemplate.GetMessageTemplateGenericByListMed(sender, dataSymptomp).ToString();
                                            await SendMessageTask(strTemplateGenericMedicalSymptoms, sender);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (cardDb.CardStepID != null && cardDb.IsConditionWithInputText == false)
                    {
                        fbUserDb.CardStepPattern = "postback_card_" + cardDb.CardStepID;
                        return await ExcuteMessage(fbUserDb.CardStepPattern, sender, botId);
                    }
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }

                //trường hợp trả về câu hỏi random chứa postback
                bool isPostbackAnswer = Regex.Match(strTempPostback, "<template><srai>postback_answer_(\\d+)</srai></template>").Success;
                if (isPostbackAnswer)
                {
                    if (result.Contains("postback_card"))
                    {
                        var cardDb = _cardService.GetCardByPattern(result.Replace(".", String.Empty));
                        if (!String.IsNullOrEmpty(cardDb.AttributeSystemName))
                        {
                            fbUserDb.IsHaveSetAttributeSystem = true;
                            fbUserDb.AttributeName = cardDb.AttributeSystemName;
                        }
                        else
                        {
                            fbUserDb.IsHaveSetAttributeSystem = false;
                            fbUserDb.AttributeName = "";
                        }

                        if (cardDb.CardStepID != null)
                        {
                            fbUserDb.CardStepPattern = "postback_card_" + cardDb.CardStepID;
                            if (cardDb.IsConditionWithInputText)// yêu cầu nhập text để chuyển sang card step
                            {
                                fbUserDb.IsConditionWithInputText = true;
                                fbUserDb.CardConditionWithInputTextPattern = fbUserDb.CardStepPattern;
                            }
                            else
                            {
                                fbUserDb.IsConditionWithInputText = false;
                                fbUserDb.CardConditionWithInputTextPattern = "";
                            }
                        }
                        else
                        {
                            fbUserDb.CardStepPattern = "";
                        }

                        if (cardDb.IsHaveCondition)
                        {
                            fbUserDb.IsHaveCardCondition = true;
                            fbUserDb.CardConditionPattern = text.Replace(".", String.Empty);
                        }
                        else
                        {
                            fbUserDb.IsHaveCardCondition = false;
                            fbUserDb.CardConditionPattern = "";
                        }

                        if (cardDb.IsConditionWithAreaButton)
                        {
                            fbUserDb.IsConditionWithAreaButton = true;
                            fbUserDb.CardConditionAreaButtonPattern = text.Replace(".", String.Empty);
                        }
                        else
                        {
                            fbUserDb.IsConditionWithAreaButton = false;
                            fbUserDb.CardConditionAreaButtonPattern = "";
                        }
                        _appFacebookUser.Update(fbUserDb);
                        _appFacebookUser.Save();
                        string tempJsonFacebook = cardDb.TemplateJsonFacebook;
                        if (!String.IsNullOrEmpty(tempJsonFacebook))
                        {
                            tempJsonFacebook = tempJsonFacebook.Trim();
                            string[] strArrayJson = Regex.Split(tempJsonFacebook, "split");//nhớ thêm bên formcard xử lý lục trên face
                            if (strArrayJson.Length != 0)
                            {
                                var strArray = strArrayJson.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                                foreach (var temp in strArray)
                                {
                                    string tempJson = temp;
                                    await SendMessageTask(tempJson, sender);
                                    if (tempJson.Contains("Nguyên nhân") || tempJson.Contains("bác sĩ") || tempJson.Contains("Bác sĩ"))
                                    {
                                        //GetMessageSymptom(_dicAttributeUser["content_message"], sender);
                                        string resultSymptomp = _apiNLR.GetListSymptoms(_dicAttributeUser["content_message"], 1);
                                        if (!String.IsNullOrEmpty(resultSymptomp))
                                        {
                                            var dataSymptomp = new JavaScriptSerializer
                                            {
                                                MaxJsonLength = Int32.MaxValue,
                                                RecursionLimit = 100
                                            }.Deserialize<List<SearchSymptomViewModel>>(resultSymptomp);
                                            if (dataSymptomp.Count() != 0)
                                            {
                                                string msgFAQs = "Bạn vui lòng xem thêm thông tin triệu chứng bên dưới";
                                                await SendMessageSymptom(FacebookTemplate.GetMessageTemplateText(msgFAQs, sender).ToString(), sender);
                                                string strTemplateGenericMedicalSymptoms = FacebookTemplate.GetMessageTemplateGenericByListMed(sender, dataSymptomp).ToString();
                                                await SendMessageTask(strTemplateGenericMedicalSymptoms, sender);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (cardDb.CardStepID != null && cardDb.IsConditionWithInputText == false)
                        {
                            fbUserDb.CardStepPattern = "postback_card_" + cardDb.CardStepID;
                            return await ExcuteMessage(fbUserDb.CardStepPattern, sender, botId);
                        }
                        return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }


                // trả lời text bình thường
                return await SendMessage(FacebookTemplate.GetMessageTemplateText(result, sender));

            }
            catch (Exception ex)
            {
                LogError(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
        }

        /// <summary>
        /// send message
        /// </summary>
        /// <param name="json">json</param>
        private async Task<HttpResponseMessage> SendMessage(JObject json)
        {
            HttpResponseMessage res;
            using (HttpClient client = new HttpClient())
            {
                string templateJson = json.ToString();
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
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                res = await client.PostAsync($"https://graph.facebook.com/v3.2/me/messages?access_token=" + pageToken + "", new StringContent(templateJson, Encoding.UTF8, "application/json"));
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// send message
        /// </summary>
        /// <param name="templateJson">templateJson</param>
        private async Task<HttpResponseMessage> SendMessage(string templateJson, string sender)
        {
            HttpResponseMessage res;
            if (!String.IsNullOrEmpty(templateJson))
            {
                templateJson = templateJson.Replace("{{senderId}}", sender);
                templateJson = Regex.Replace(templateJson, "File/", Domain + "File/");
                //templateJson = Regex.Replace(templateJson, "<br />", "\\n");
                //templateJson = Regex.Replace(templateJson, "<br/>", "\\n");
                //templateJson = Regex.Replace(templateJson, @"\\n\\n", "\\n");
                //templateJson = Regex.Replace(templateJson, @"\\n\\r\\n", "\\n");
                //templateJson = Regex.Replace(templateJson, "  ", "\\n");
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
                    res = await client.PostAsync($"https://graph.facebook.com/v3.2/me/messages?access_token=" + pageToken + "", new StringContent(templateJson, Encoding.UTF8, "application/json"));
                }
            }
            return new HttpResponseMessage(HttpStatusCode.OK);

        }

        private async Task SendMessageTask(string templateJson, string sender)
        {
            if (!String.IsNullOrEmpty(templateJson))
            {
                templateJson = templateJson.Replace("{{senderId}}", sender);
                templateJson = Regex.Replace(templateJson, "File/", Domain + "File/");
                //templateJson = Regex.Replace(templateJson, "<br />", "\\n");
                //templateJson = Regex.Replace(templateJson, "<br/>", "\\n");
                //templateJson = Regex.Replace(templateJson, @"\\n\\n", "\\n");
                //templateJson = Regex.Replace(templateJson, @"\\n\\r\\n", "\\n");
                //templateJson = Regex.Replace(templateJson, "  ", "\\n");
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
                    HttpResponseMessage res = await client.PostAsync($"https://graph.facebook.com/v3.2/me/messages?access_token=" + pageToken + "", new StringContent(templateJson, Encoding.UTF8, "application/json"));
                }               
            }
        }

        private async Task SendMessageSymptom(string templateJson, string sender)
        {
            if (!String.IsNullOrEmpty(templateJson))
            {
                templateJson = templateJson.Replace("{{senderId}}", sender);
                templateJson = Regex.Replace(templateJson, "File/", Domain + "File/");
                //templateJson = Regex.Replace(templateJson, "<br />", "\\n");
                //templateJson = Regex.Replace(templateJson, "<br/>", "\\n");
                //templateJson = Regex.Replace(templateJson, @"\\n\\n", "\\n");
                //templateJson = Regex.Replace(templateJson, @"\\n\\r\\n", "\\n");
                //templateJson = Regex.Replace(templateJson, "  ", "\\n");
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage res = await client.PostAsync($"https://graph.facebook.com/v3.2/me/messages?access_token=" + pageToken + "", new StringContent(templateJson, Encoding.UTF8, "application/json"));
                }
            }
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

        #region --DATA SOURCE API--

        private string apiRelateQA = "/api/qa_for_all/get_related_pair";

        private string ApiAddUpdateQA(string NameFuncAPI, object T, string Type = "Post")
        {
            string result = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlAPI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("x-api-key", KeyAPI);
                HttpResponseMessage response = new HttpResponseMessage();
                string json = JsonConvert.SerializeObject(T);
                StringContent httpContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
                try
                {
                    switch (Type)
                    {
                        case "Post":
                            response = client.PostAsync(NameFuncAPI, httpContent).Result;
                            break;
                        case "Get":
                            string requestUri = NameFuncAPI + "?" + httpContent;
                            response = client.GetAsync(requestUri).Result;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                }
            }
            return result;
        }
        private string GetRelatedQuestionToFacebook(string nameFuncAPI, string question, string field, string number, string botId)
        {
            string result = "";
            if (String.IsNullOrEmpty(nameFuncAPI))
            {
                nameFuncAPI = apiRelateQA;
            }
            else
            {
                nameFuncAPI = nameFuncAPI.Replace("http://172.16.7.71:80", "").Trim();
            }
            var param = new
            {
                question = question,
                number = number,
                field = field,
                botid = botId
            };
            string responseString = ApiAddUpdateQA(nameFuncAPI, param, "Post");

            return responseString;
        }

        #endregion
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
        private void AddHistory(HistoryViewModel hisVm)
        {
            History hisDb = new History();
            hisDb.UpdateHistory(hisVm);
            _historyService.Create(hisDb);
            _historyService.Save();
        }
        private static async Task Schedule(string UserId, string strMessage, string pageToken, DateTime dTimeOut, string modulePayload)
        {
            try
            {
                // Grab the Scheduler instance from the Factory
                System.Collections.Specialized.NameValueCollection props = new System.Collections.Specialized.NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = await factory.GetScheduler();
                // and start it off
                await scheduler.Start();

                IJobDetail job = JobBuilder.Create<ProactiveMessageJob>()
                     //.WithIdentity("ProactiveMsgJob", "ProactiveMsgJob") 
                     //.UsingJobData("userId", "0")
                     .Build();
                ITrigger trigger = TriggerBuilder.Create()
                        //.WithIdentity(triggerName, "ProactiveMsgJob")
                        .UsingJobData("UserId", UserId)
                        .UsingJobData("Message", strMessage)
                        .UsingJobData("PageToken", pageToken)
                        .UsingJobData("Payload", modulePayload)
                        .UsingJobData("TimeOut", dTimeOut.ToLocalTime().ToString())
                        .StartAt(dTimeOut.ToLocalTime())
                        //.WithSimpleSchedule(x => x
                        //    .WithIntervalInSeconds(5)
                        //    .WithRepeatCount(0))
                        //.ForJob("ProactiveMsgJob", group)
                        .Build();

                // Tell quartz to schedule the job using our trigger
                await scheduler.ScheduleJob(job, trigger);

                //await scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {

            }
        }

        public class ProactiveMessageJob : IJob
        {
            private readonly string Domain = Helper.ReadString("Domain");
            private readonly string _sqlConnection = Helper.ReadString("SqlConnection");
            public async Task Execute(IJobExecutionContext context)
            {
                JobKey key = context.JobDetail.Key;
                //JobDataMap dataMapDefault = context.JobDetail.JobDataMap;
                JobDataMap dataMap = context.MergedJobDataMap;
                string userId = dataMap.GetString("UserId");
                string message = dataMap.GetString("Message");
                string pageToken = dataMap.GetString("PageToken");
                string TimeOut = dataMap.GetString("TimeOut");
                string payLoad = dataMap.GetString("Payload");
                DateTime dTimeOut = Convert.ToDateTime(TimeOut);

                DateTime timeOutDb;
                int resultTimeCompare = 3;
                var sqlConnection = new SqlConnection(_sqlConnection);
                sqlConnection.Open();

                SqlCommand command = new SqlCommand("Select TimeOut from [ApplicationFacebookUsers] where UserId=@userId", sqlConnection);
                command.Parameters.AddWithValue("@userId", userId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        timeOutDb = (DateTime)reader["TimeOut"];
                        resultTimeCompare = DateTime.Compare(DateTime.Now, timeOutDb);
                    }
                }
                command.ExecuteNonQuery();
                sqlConnection.Close();

                if (resultTimeCompare == 1)
                {
                    if (payLoad != CommonConstants.ModuleAdminContact)
                    {
                        await SendProactiveMessage(message, userId, pageToken, dTimeOut);

                        var sqlConnection2 = new SqlConnection(_sqlConnection);
                        sqlConnection2.Open();

                        SqlCommand command2 = new SqlCommand("UPDATE ApplicationFacebookUsers SET PredicateName = @predicateName, PredicateValue = @predicateValue, IsHavePredicate = @isHavePredicate,IsHaveCardCondition = @isHaveCardCondition,CardConditionPattern = @cardConditionPattern Where UserId=@userId", sqlConnection2);
                        command2.Parameters.AddWithValue("@userId", userId);
                        command2.Parameters.AddWithValue("@predicateName", "");
                        command2.Parameters.AddWithValue("@predicateValue", "");
                        command2.Parameters.AddWithValue("@isHavePredicate", "0");
                        command2.Parameters.AddWithValue("@isHaveCardCondition", "0");
                        command2.Parameters.AddWithValue("@cardConditionPattern", "");
                        command2.ExecuteNonQuery();
                        sqlConnection2.Close();
                    }
                }
            }
            private async Task<HttpResponseMessage> SendProactiveMessage(string templateJson, string sender, string pageToken, DateTime dTimeOut)
            {
                HttpResponseMessage res;
                if (!String.IsNullOrEmpty(templateJson))
                {
                    templateJson = templateJson.Replace("{{senderId}}", sender);
                    templateJson = Regex.Replace(templateJson, "File/", Domain + "File/");
                    templateJson = Regex.Replace(templateJson, "<br />", "\\n");
                    templateJson = Regex.Replace(templateJson, "<br/>", "\\n");
                    templateJson = Regex.Replace(templateJson, @"\\n\\n", "\\n");
                    templateJson = Regex.Replace(templateJson, @"\\n\\r\\n", "\\n");
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        res = await client.PostAsync($"https://graph.facebook.com/v3.2/me/messages?access_token=" + pageToken + "", new StringContent(templateJson, Encoding.UTF8, "application/json"));
                    }
                }
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
        }

        private static async Task ScheduleOTP(string UserId, string telePhoneNumber, string mdVoucherId, string pageToken, DateTime dTimeOut)
        {
            try
            {
                // Grab the Scheduler instance from the Factory
                System.Collections.Specialized.NameValueCollection props = new System.Collections.Specialized.NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = await factory.GetScheduler();
                // and start it off
                await scheduler.Start();

                IJobDetail job = JobBuilder.Create<CancleCodeOtpJob>()
                     .Build();
                ITrigger trigger = TriggerBuilder.Create()
                        .UsingJobData("UserId", UserId)
                        .UsingJobData("PageToken", pageToken)
                        .UsingJobData("PhoneNumber", telePhoneNumber)
                        .UsingJobData("MdVoucherID", mdVoucherId)
                        .UsingJobData("TimeOut", dTimeOut.ToLocalTime().ToString())
                        .StartAt(dTimeOut.ToLocalTime())
                        .Build();
                // Tell quartz to schedule the job using our trigger
                await scheduler.ScheduleJob(job, trigger);
            }
            catch (SchedulerException se)
            {

            }
        }

        public class CancleCodeOtpJob : IJob
        {
            private readonly string Domain = Helper.ReadString("Domain");
            private readonly string _sqlConnection = Helper.ReadString("SqlConnection");
            public async Task Execute(IJobExecutionContext context)
            {
                JobKey key = context.JobDetail.Key;
                //JobDataMap dataMapDefault = context.JobDetail.JobDataMap;
                JobDataMap dataMap = context.MergedJobDataMap;
                string userId = dataMap.GetString("UserId");
                string pageToken = dataMap.GetString("PageToken");
                string phoneNumber = dataMap.GetString("PhoneNumber");
                string mdVoucherId = dataMap.GetString("MdVoucherID");
                string TimeOut = dataMap.GetString("TimeOut");
                DateTime dTimeOut = Convert.ToDateTime(TimeOut);

                bool isReceived = false;
                var sqlConnection = new SqlConnection(_sqlConnection);
                sqlConnection.Open();

                SqlCommand command = new SqlCommand("Select top 1 * from [UserTelePhones] where TelephoneNumber = @phone and MdVoucherID = @voucherID", sqlConnection);
                command.Parameters.AddWithValue("@phone", phoneNumber);
                command.Parameters.AddWithValue("@voucherID", mdVoucherId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        isReceived = (bool)reader["IsReceive"];
                    }
                }
                command.ExecuteNonQuery();
                sqlConnection.Close();


                if (isReceived == false)
                {
                    var sqlConnection2 = new SqlConnection(_sqlConnection);
                    sqlConnection2.Open();

                    SqlCommand command2 = new SqlCommand("UPDATE UserTelePhones SET Code = @code Where TelephoneNumber = @phone and MdVoucherID = @voucherID", sqlConnection2);
                    command2.Parameters.AddWithValue("@phone", phoneNumber);
                    command2.Parameters.AddWithValue("@code", "timeout");
                    command2.Parameters.AddWithValue("@voucherID", mdVoucherId);
                    command2.ExecuteNonQuery();
                    sqlConnection2.Close();
                }

            }
        }

        public class ProfileUser
        {
            public string first_name { set; get; }
            public string last_name { set; get; }
            public string profile_pic { set; get; }
            public string id { set; get; }
            public string gender { set; get; }
        }
    }
}
