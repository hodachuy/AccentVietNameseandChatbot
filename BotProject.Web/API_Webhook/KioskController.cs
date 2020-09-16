using AIMLbot;
using AutoMapper;
using BotProject.Common;
using BotProject.Common.AppThird3PartyTemplate;
using BotProject.Common.ViewModels;
using BotProject.Model.Models;
using BotProject.Service;
using BotProject.Web.Infrastructure.Core;
using BotProject.Web.Infrastructure.Extensions;
using BotProject.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace BotProject.Web.API_Webhook
{
    public class KioskController : ApiControllerBase
    {
        // Thẻ chat với chuyên viên
        public string _contactAdmin = Helper.ReadString("AdminContact");
        public string _titlePayloadContactAdmin = Helper.ReadString("TitlePayloadAdminContact");

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

        // Tin nhắn vắng mặt
        public string _messageAbsent = "";
        public bool _isHaveMessageAbsent = false;

        // Thời gian phản hồi lại sau khoảng timeOut
        public int _timeOut = 60;

        // Domain, API Search Engine
        private readonly string Domain = Helper.ReadString("Domain");
        private readonly string UrlAPI = Helper.ReadString("UrlAPI");
        private readonly string KeyAPI = Helper.ReadString("KeyAPI");
        private string pathAIML = PathServer.PathAIML;
        private string pathSetting = PathServer.PathAIML + "config";

        // Pattern kiểm tra là số
        private const string NumberPattern = @"^\d+$";

        // Nút thoát
        string _patternCardPayloadProactive = "quay về";
        string _titleCardPayloadProactive = "🔙 Quay về";

        // Điều kiện có mở search engine
        bool _isSearchAI = true;


        // Model user
        ApplicationPlatformUser _plUserDb;

        // Services
        private ApiQnaNLRService _apiNLR;
        private IErrorService _errorService;
        private BotServiceMedical _botService;
        private ISettingService _settingService;
        private ICardService _cardService;
        private IAIMLFileService _aimlFileService;
        private IBotService _botDbService;
        private User _user;
        private Dictionary<string, string> _dicNotMatch;
        private Dictionary<string, string> _dicAttributeUser;
        private IApplicationPlatformUserService _appPlatformUser;
        private IAttributeSystemService _attributeService;
        private IHandleModuleServiceService _handleMdService;
        private IHistoryService _historyService;

        //Accent Vietnamese
        private AccentService _accentService;

        public List<string> _lstBotReplyResponse = new List<string>();
        public KioskController(IErrorService errorService,
                                      IBotService botDbService,
                                      ISettingService settingService,
                                      ICardService cardService,
                                      IAIMLFileService aimlFileService,
                                      IApplicationPlatformUserService appPlatformUser,
                                      IAttributeSystemService attributeService,
                                      IHandleModuleServiceService handleMdService,
                                      IHistoryService historyService) : base(errorService)
        {
            _errorService = errorService;
            _botService = BotServiceMedical.BotInstance;
            _botDbService = botDbService;
            _settingService = settingService;
            _cardService = cardService;
            _aimlFileService = aimlFileService;
            _appPlatformUser = appPlatformUser;
            _attributeService = attributeService;
            _handleMdService = handleMdService;
            _historyService = historyService;
            _apiNLR = new ApiQnaNLRService();
            _plUserDb = new ApplicationPlatformUser();
        }

        [HttpGet]
        [Route("getStarted")]
        public async Task<HttpResponseMessage> GetStarted(HttpRequestMessage request, int botId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var tempStarted = _settingService.GetSettingByBotID(botId);
                response = request.CreateResponse(HttpStatusCode.OK, new
                {
                    status = "2",
                    message = "Thành công",
                    data = "postback_card_" + tempStarted.CardID
                });
                return response;
            });
        }

        [HttpPost]
        [Route("receive")]
        public async Task<HttpResponseMessage> Receive(HttpRequestMessage request, Message message)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string text = message.text;
                string senderId = message.senderId;
                int botId = Int32.Parse(message.botId);
                if (String.IsNullOrEmpty(text))
                {
                    response = request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "-1",
                        message = "Vui lòng nhập nội dung",
                        data = new string[] { }
                    });
                    return response;
                }
                if (String.IsNullOrEmpty(message.botId))
                {
                    response = request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "-1",
                        message = "Không có thông tin Bot",
                        data = new string[] { }
                    });
                    return response;
                }
                if (String.IsNullOrEmpty(message.senderId))
                {
                    response = request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "-1",
                        message = "Không có thông tin người dùng",
                        data = new string[] { }
                    });
                    return response;
                }

                var botDb = _botDbService.GetByID(botId);
                var settingDb = _settingService.GetSettingByBotID(botId);
                var systemConfig = _settingService.GetListSystemConfigByBotId(botId);
                var lstAttribute = _attributeService.GetListAttributePlatform(senderId, botId).ToList();
                if (lstAttribute.Count() != 0)
                {
                    _dicAttributeUser = new Dictionary<string, string>();
                    foreach (var attr in lstAttribute)
                    {
                        _dicAttributeUser.Add(attr.AttributeKey, attr.AttributeValue);
                    }
                }
                string typeRequest = "text";
                if (text.Contains("postback"))
                {
                    typeRequest = "payload_postback";
                }
                // get list message response
                var lstMsgResponse = MessageResponse(text, senderId, botId, typeRequest).Result;
                if (lstMsgResponse.Count() == 0)
                {
                    response = request.CreateResponse(HttpStatusCode.OK, new
                    {
                        status = "3",
                        message = "Không có dữ liệu",
                        data = new string[] { }
                    });
                    return response;
                }
                List<dynamic> lstResult = new List<dynamic>();
                foreach (var msg in lstMsgResponse)
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(msg);
                    lstResult.Add(result);
                }
                // TH chỉ trả về 1 thẻ cuối cùng yêu cầu đi gặp bác sĩ hoặc trả ra có chứa nguyên nhân,
                // gọi tiếp api trả ra thông tin triệu chứng
                if (lstMsgResponse.Count() == 1)
                {
                    foreach (var msg in lstMsgResponse)
                    {
                        if (botId == 3019)
                        {
                            if (msg.Contains("Nguyên nhân") || msg.Contains("bác sĩ") || msg.Contains("Bác sĩ"))
                            {
                                List<string> lstSymtom = new List<string>();
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
                                        string msgFAQs = FacebookTemplate.GetMessageTemplateText("Bạn vui lòng xem thêm thông tin triệu chứng bên dưới", senderId).ToString();
                                        lstSymtom.Add(msgFAQs);
                                        string strTemplateGenericRelatedMedical = FacebookTemplate.GetMessageTemplateGenericByListMed(senderId, dataSymptomp).ToString();
                                        lstSymtom.Add(HandleMessageJson(strTemplateGenericRelatedMedical, senderId));
                                    }
                                    if (lstSymtom.Count() != 0)
                                    {
                                        foreach (var item in lstSymtom)
                                        {
                                            var result = JsonConvert.DeserializeObject<dynamic>(item);
                                            lstResult.Add(result);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                response = request.CreateResponse(HttpStatusCode.OK, new
                {
                    status = "2",
                    message = "Thành công",
                    data = lstResult
                });
                return response;
            });
        }
        private async Task<List<string>> MessageResponse(string text, string senderId, int botId, string typeRequest, bool isHavePreviousResponse = false)
        {
            if (!isHavePreviousResponse)
            {
                _lstBotReplyResponse = new List<string>();
            }

            text = HttpUtility.HtmlDecode(text);
            text = Regex.Replace(text, @"<(.|\n)*?>", "").Trim();
            text = Regex.Replace(text, @"\p{Cs}", "").Trim(); //remove emoji

            _plUserDb = _appPlatformUser.GetByUserId(senderId);

            int age = 10;

            if (_plUserDb != null && (_plUserDb.PredicateName != "Admin_Contact"))
            {
                if (_plUserDb.Age != 0)
                {
                    bool isAge = Regex.Match(text, NumberPattern).Success;
                    if (isAge)
                    {
                        age = _plUserDb.Age;
                    }
                }
                //await SendMessageTask(senderActionTyping, sender);
            }

            // Input text
            if (typeRequest == CommonConstants.BOT_REQUEST_TEXT)
            {
                // Thêm dấu tiếng việt
                bool isActive = true;
                string textAccentVN = GetPredictAccentVN(text, isActive);
                if (textAccentVN != text)
                {
                    string msg = FacebookTemplate.GetMessageTemplateText("Ý bạn là: " + textAccentVN + "", senderId).ToString();
                    _lstBotReplyResponse.Add(HandleMessageJson(msg, senderId));
                }
                text = textAccentVN;
            }

            //if (!text.Contains("postback") && !text.Contains(_contactAdmin))
            //{
            //    //_accentService = new AccentService(); 
            //    _accentService = AccentService.SingleInstance;
            //    string textAccentVN = _accentService.GetAccentVN(text);
            //    if (textAccentVN != text)
            //    {
            //        string msg = FacebookTemplate.GetMessageTemplateText("Ý bạn là: " + textAccentVN + "", senderId).ToString();
            //        _lstBotReplyResponse.Add(HandleMessageJson(msg, senderId));
            //    }
            //    text = textAccentVN;
            //    AddAttributeDefault(senderId, botId, "content_message", text);
            //    _dicAttributeUser.Remove("content_message");
            //    _dicAttributeUser.Add("content_message", text);
            //}

            //// Xét payload postback nếu postback từ quickreply sẽ chứa thêm sperator - và tiêu đề nút
            //string attributeValueFromPostback = "";
            //if (text.Contains("postback"))
            //{
            //    var arrPostback = Regex.Split(text, "-");
            //    if (arrPostback.Length > 1)
            //    {
            //        attributeValueFromPostback = arrPostback[1];
            //    }
            //    text = arrPostback[0];
            //}

            //HistoryViewModel hisVm = new HistoryViewModel();
            //hisVm.BotID = botId;
            //hisVm.CreatedDate = DateTime.Now;
            //hisVm.UserSay = text;
            //hisVm.UserName = senderId;
            //hisVm.Type = CommonConstants.TYPE_KIOSK;

            //DateTime dStartedTime = DateTime.Now;
            //DateTime dTimeOut = DateTime.Now.AddSeconds(_timeOut);

            //if (text == "postback_card_4031")// thẻ chào hỏi
            //{
            //    if (_plUserDb.Age == 0)//nếu thông tin tuổi chưa có trả về thẻ hỏi thông tin
            //    {
            //        text = "postback_card_8917";//trả về thẻ hỏi thông tin
            //    }
            //}
            //try
            //{
            //    if (_plUserDb != null)
            //    {
            //        // Chat với admin
            //        if (_plUserDb.PredicateName == "Admin_Contact")
            //        {
            //            var handleAdminContact = _handleMdService.HandleIsAdminContact(text, botId);
            //            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_004;
            //            AddHistory(hisVm);
            //            if (text.Contains("postback") || text.Contains(_contactAdmin))
            //            {
            //                _plUserDb.IsHavePredicate = false;
            //                _plUserDb.PredicateName = "";
            //                _plUserDb.PredicateValue = "";
            //                _plUserDb.IsHaveCardCondition = false;
            //                _plUserDb.CardConditionPattern = "";
            //                _plUserDb.IsConditionWithAreaButton = false;
            //                _plUserDb.CardConditionAreaButtonPattern = "";
            //                _plUserDb.CardStepPattern = "";
            //                _plUserDb.AttributeName = "";
            //                _plUserDb.IsHaveSetAttributeSystem = false;
            //                _plUserDb.IsConditionWithInputText = false;
            //                _plUserDb.CardConditionWithInputTextPattern = "";
            //                _plUserDb.TypeDevice = "kiosk";
            //                _appPlatformUser.Update(_plUserDb);
            //                _appPlatformUser.Save();
            //                return await MessageResponse(text, senderId, botId, typeRequest);
            //            }
            //            // Tin nhắn vắng mặt
            //            if (_isHaveMessageAbsent)
            //            {
            //                if (HelperMethods.IsTimeInWorks() == false)
            //                {
            //                    //await SendMessageTask(FacebookTemplate.GetMessageTemplateTextAndQuickReply(_messageAbsent, "{{senderId}}", _patternCardPayloadProactive, _titleCardPayloadProactive).ToString(),sender);
            //                    //return new HttpResponseMessage(HttpStatusCode.OK);

            //                    return await Task.FromResult<List<string>>(_lstBotReplyResponse);
            //                }
            //            }
            //            if (handleAdminContact.Status == false)
            //            {
            //                string[] strArrayJson = Regex.Split(handleAdminContact.TemplateJsonFacebook, "split");
            //                if (strArrayJson.Length != 0)
            //                {
            //                    var strArray = strArrayJson.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            //                    foreach (var temp in strArray)
            //                    {
            //                        string tempJson = HandleMessageJson(temp, senderId);
            //                        _lstBotReplyResponse.Add(tempJson);
            //                        //await SendMessageTask(tempJson, sender);
            //                    }
            //                    //return new HttpResponseMessage(HttpStatusCode.OK);
            //                }
            //            }
            //            return await Task.FromResult<List<string>>(_lstBotReplyResponse);
            //        }
            //    }
            //    else
            //    {
            //        _plUserDb = new ApplicationPlatformUser();
            //        _plUserDb.UserId = senderId;
            //        _plUserDb.IsHavePredicate = false;
            //        _plUserDb.IsProactiveMessage = false;
            //        _plUserDb.TimeOut = dTimeOut;
            //        _plUserDb.CreatedDate = DateTime.Now;
            //        _plUserDb.StartedOn = dStartedTime;
            //        _plUserDb.FirstName = "N/A"; //profileUser.first_name;
            //        _plUserDb.Age = 0; //"N/A";
            //        _plUserDb.LastName = "N/A"; //profileUser.last_name;
            //        _plUserDb.UserName = "bạn"; //profileUser.first_name + " " + profileUser.last_name;
            //        _plUserDb.Gender = true; //"N/A";
            //        _appPlatformUser.Add(_plUserDb);
            //        _appPlatformUser.Save();

            //        // add attribute default user platform
            //        AddAttributeDefault(senderId, botId, "sender_id", _plUserDb.UserId);
            //        AddAttributeDefault(senderId, botId, "sender_name", _plUserDb.UserName);
            //        AddAttributeDefault(senderId, botId, "sender_first_name", _plUserDb.FirstName);
            //        AddAttributeDefault(senderId, botId, "sender_last_name", _plUserDb.LastName);
            //        AddAttributeDefault(senderId, botId, "gender", "N/A");
            //    }

            //    _plUserDb.StartedOn = dStartedTime;
            //    _plUserDb.TimeOut = dTimeOut;
            //    // Nếu có yêu cầu click thẻ để đi theo luồng
            //    if (_plUserDb.IsHaveCardCondition)
            //    {
            //        if (!text.Contains("postback") && !text.Contains(_contactAdmin))
            //        {
            //            var cardDb = _cardService.GetCardByPattern(_plUserDb.CardConditionPattern);
            //            if (cardDb == null)
            //            {
            //                return await Task.FromResult<List<string>>(_lstBotReplyResponse);
            //            }
            //            string tempJsonFacebook = cardDb.TemplateJsonFacebook;
            //            if (!String.IsNullOrEmpty(tempJsonFacebook))
            //            {
            //                tempJsonFacebook = tempJsonFacebook.Trim();
            //                string[] strArrayJson = Regex.Split(tempJsonFacebook, "split");
            //                if (strArrayJson.Length != 0)
            //                {
            //                    //await SendMessageTask(FacebookTemplate.GetMessageTemplateText("Anh/chị vui lòng chọn lại thông tin bên dưới", sender).ToString(), sender);
            //                    string msg = FacebookTemplate.GetMessageTemplateText("Anh/chị vui lòng chọn lại thông tin bên dưới", senderId).ToString();
            //                    _lstBotReplyResponse.Add(msg);
            //                    var strArray = strArrayJson.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            //                    foreach (var temp in strArray)
            //                    {
            //                        //string tempJson = temp;
            //                        string tempJson = HandleMessageJson(temp, senderId);
            //                        _lstBotReplyResponse.Add(tempJson);
            //                        //await SendMessageTask(tempJson, sender);
            //                    }
            //                    //return new HttpResponseMessage(HttpStatusCode.OK);
            //                    return await Task.FromResult<List<string>>(_lstBotReplyResponse);
            //                }
            //            }
            //        }
            //    }

            //    // AttributePlatform nếu thẻ trước có biến cần lưu
            //    if (_plUserDb.IsHaveSetAttributeSystem)
            //    {
            //        AttributePlatformUser attPlUser = new AttributePlatformUser();
            //        attPlUser.AttributeKey = _plUserDb.AttributeName;
            //        //attFbUser.AttributeValue = text;
            //        attPlUser.BotID = botId;
            //        attPlUser.UserID = senderId;
            //        if (!text.Contains("postback") && !text.Contains(_contactAdmin))
            //        {
            //            attPlUser.AttributeValue = text;
            //        }
            //        else if (text.Contains("postback"))
            //        {
            //            attPlUser.AttributeValue = attributeValueFromPostback.Trim();
            //        }
            //        if (attPlUser.AttributeKey == "age")
            //        {
            //            bool isAge = Regex.Match(text, NumberPattern).Success;
            //            if (isAge)
            //            {
            //                attPlUser.AttributeValue = text;
            //            }
            //            else
            //            {
            //                //await SendMessageTask(FacebookTemplate.GetMessageTemplateText("Ký tự phải là số, Anh/chị vui lòng nhập lại độ tuổi", sender).ToString(), sender);
            //                //return new HttpResponseMessage(HttpStatusCode.OK);
            //                string msg = FacebookTemplate.GetMessageTemplateText("Ký tự phải là số, Anh/chị vui lòng nhập lại độ tuổi", senderId).ToString();
            //                _lstBotReplyResponse.Add(HandleMessageJson(msg, senderId));
            //                return await Task.FromResult<List<string>>(_lstBotReplyResponse);
            //            }
            //        }

            //        _dicAttributeUser.Remove(attPlUser.AttributeKey);
            //        _dicAttributeUser.Add(attPlUser.AttributeKey, attPlUser.AttributeValue);

            //        var att = _attributeService.CreateUpdateAttributePlatform(attPlUser);

            //        if (attPlUser.AttributeKey == "age")
            //        {
            //            _plUserDb.Age = Int32.Parse(text);
            //            _plUserDb.IsHaveSetAttributeSystem = false;
            //            _plUserDb.AttributeName = "";
            //            _appPlatformUser.Update(_plUserDb);
            //            _appPlatformUser.Save();

            //            _dicAttributeUser.Remove("age");
            //            _dicAttributeUser.Add("age", text);
            //            //return await ExcuteMessage("postback_card_8927", sender, botId); //postback_card_8927 thẻ thông tin người dùng
            //        }
            //    }

            //    // Nhập text để đi luồng tiếp theo nhưng CardStepID không được rỗng
            //    if (_plUserDb.IsConditionWithInputText)
            //    {
            //        if (!text.Contains("postback") && !text.Contains(_contactAdmin))
            //        {
            //            _plUserDb.IsHaveSetAttributeSystem = false;
            //            _plUserDb.AttributeName = "";
            //            _plUserDb.IsConditionWithInputText = false;
            //            _plUserDb.IsHaveCardCondition = false;
            //            _appPlatformUser.Update(_plUserDb);
            //            _appPlatformUser.Save();
            //            return await MessageResponse(_plUserDb.CardConditionWithInputTextPattern, senderId, botId, typeRequest);
            //            //return await ExcuteMessage(fbUserDb.CardConditionWithInputTextPattern, sender, botId); //postback_card_8927 thẻ thông tin người dùng
            //        }
            //    }

            //    // Nếu có yêu cầu query text theo lĩnh vực button
            //    // Click button -> card (tên card nên đặt như tên lĩnh vực ngắn gọn)
            //    // Build lại kịch bản với từ khoán ngắn gọn + tên lĩnh vực
            //    // ví dụ: thủ tục cấp phép, thủ tục giạn + tên lĩnh vực
            //    if (_plUserDb.IsConditionWithAreaButton)
            //    {
            //        if (!text.Contains("postback") && !text.Contains(_contactAdmin))
            //        {
            //            var cardDb = _cardService.GetCardByPattern(_plUserDb.CardConditionAreaButtonPattern);
            //            if (cardDb == null)
            //            {
            //                _lstBotReplyResponse = new List<string>();
            //                return await Task.FromResult<List<string>>(_lstBotReplyResponse);
            //                //return new HttpResponseMessage(HttpStatusCode.OK);
            //            }
            //            string area = cardDb.Name;
            //            text = text + " " + area;// + thêm tên lĩnh vực button và nội dung trong form QnA có chứa từ lĩnh vực
            //        }
            //    }

            //    // Điều kiện xử lý module
            //    if (_plUserDb.IsHavePredicate)
            //    {
            //        var predicateName = _plUserDb.PredicateName;
            //        if (predicateName == "ApiSearch")
            //        {
            //            if (text.Contains("postback_card") || text.Contains(_contactAdmin))// nều còn điều kiện search mà chọn postback
            //            {
            //                _plUserDb.IsHavePredicate = false;
            //                _plUserDb.PredicateName = "";
            //                _plUserDb.PredicateValue = "";
            //                _plUserDb.IsHaveCardCondition = false;
            //                _plUserDb.CardConditionPattern = "";
            //                _plUserDb.IsConditionWithAreaButton = false;
            //                _plUserDb.CardConditionAreaButtonPattern = "";
            //                _plUserDb.CardStepPattern = "";
            //                _plUserDb.AttributeName = "";
            //                _plUserDb.IsHaveSetAttributeSystem = false;
            //                _plUserDb.IsConditionWithInputText = false;
            //                _plUserDb.CardConditionWithInputTextPattern = "";
            //                _appPlatformUser.Update(_plUserDb);
            //                _appPlatformUser.Save();
            //                return await MessageResponse(text, senderId, botId, typeRequest);
            //                //return await ExcuteMessage(text, sender, botId);
            //            }

            //            string predicateValue = _plUserDb.PredicateValue;
            //            var handleMdSearch = _handleMdService.HandleIsSearchAPI(text, predicateValue, "");

            //            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_005;
            //            AddHistory(hisVm);

            //            _lstBotReplyResponse.Add(HandleMessageJson(handleMdSearch.TemplateJsonFacebook, senderId));

            //            return await Task.FromResult<List<string>>(_lstBotReplyResponse);
            //            //return await SendMessage(handleMdSearch.TemplateJsonFacebook, sender);
            //        }
            //    }
            //    else // Input: Khởi tạo module được chọn
            //    {
            //        if (text.Contains(CommonConstants.ModuleAdminContact))
            //        {
            //            var handleAdminContact = _handleMdService.HandleIsAdminContact(text, botId);

            //            _plUserDb.IsHavePredicate = true;
            //            _plUserDb.PredicateName = "Admin_Contact";
            //            _plUserDb.PredicateValue = "";
            //            _plUserDb.IsHaveCardCondition = false;
            //            _plUserDb.CardConditionPattern = "";
            //            _plUserDb.IsConditionWithAreaButton = false;
            //            _plUserDb.CardConditionAreaButtonPattern = "";
            //            _plUserDb.CardStepPattern = "";
            //            _plUserDb.IsHaveSetAttributeSystem = false;
            //            _plUserDb.AttributeName = "";
            //            _plUserDb.IsConditionWithInputText = false;
            //            _plUserDb.CardConditionWithInputTextPattern = "";
            //            _appPlatformUser.Update(_plUserDb);
            //            _appPlatformUser.Save();

            //            hisVm.UserSay = "[Chat với chuyên viên]";
            //            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_003;
            //            AddHistory(hisVm);

            //            // Tin nhắn vắng mặt
            //            if (_isHaveMessageAbsent)
            //            {
            //                if (HelperMethods.IsTimeInWorks() == false)
            //                {
            //                    string msg = FacebookTemplate.GetMessageTemplateTextAndQuickReply(_messageAbsent, "{{senderId}}", _patternCardPayloadProactive, _titleCardPayloadProactive).ToString();
            //                    _lstBotReplyResponse.Add(HandleMessageJson(msg, senderId));
            //                    //await SendMessageTask(FacebookTemplate.GetMessageTemplateTextAndQuickReply(_messageAbsent, "{{senderId}}", _patternCardPayloadProactive, _titleCardPayloadProactive).ToString(), sender);
            //                    //return new HttpResponseMessage(HttpStatusCode.OK);
            //                    return await Task.FromResult<List<string>>(_lstBotReplyResponse);
            //                }
            //            }
            //            string[] strArrayJson = Regex.Split(handleAdminContact.TemplateJsonFacebook, "split");//nhớ thêm bên formcard xử lý lục trên face
            //            if (strArrayJson.Length != 0)
            //            {
            //                var strArray = strArrayJson.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            //                foreach (var temp in strArray)
            //                {
            //                    //string tempJson = temp;
            //                    //await SendMessageTask(tempJson, sender);
            //                    string tempJson = HandleMessageJson(temp, senderId);
            //                    _lstBotReplyResponse.Add(tempJson);
            //                }
            //            }
            //            return await Task.FromResult<List<string>>(_lstBotReplyResponse);
            //            //return new HttpResponseMessage(HttpStatusCode.OK);
            //        }

            //        if (text.Contains(CommonConstants.ModuleSearchAPI))
            //        {
            //            string mdSearchId = text.Replace(".", String.Empty).Replace("postback_module_api_search_", "");
            //            var handleMdSearch = _handleMdService.HandleIsSearchAPI(text, mdSearchId, "");
            //            _plUserDb.IsHavePredicate = true;
            //            _plUserDb.PredicateName = "ApiSearch";
            //            _plUserDb.PredicateValue = mdSearchId;
            //            _plUserDb.IsHaveCardCondition = false;
            //            _plUserDb.CardConditionPattern = "";
            //            _plUserDb.IsConditionWithAreaButton = false;
            //            _plUserDb.CardConditionAreaButtonPattern = "";
            //            _plUserDb.CardStepPattern = "";
            //            _plUserDb.IsHaveSetAttributeSystem = false;
            //            _plUserDb.AttributeName = "";
            //            _plUserDb.IsConditionWithInputText = false;
            //            _plUserDb.CardConditionWithInputTextPattern = "";
            //            _appPlatformUser.Update(_plUserDb);
            //            _appPlatformUser.Save();

            //            hisVm.UserSay = "[Tra cứu]";
            //            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_003;
            //            AddHistory(hisVm);

            //            //return await SendMessage(handleMdSearch.TemplateJsonFacebook, sender);
            //            string msg = HandleMessageJson(handleMdSearch.TemplateJsonFacebook, senderId);
            //            _lstBotReplyResponse.Add(msg);
            //            return await Task.FromResult<List<string>>(_lstBotReplyResponse);
            //        }
            //    }

            //    if (!text.Contains("postback") && !text.Contains(_contactAdmin))
            //    {
            //        if (age <= 7)
            //        {
            //            text = "trẻ em " + text;
            //        }
            //    }

            //    AIMLbot.Result aimlBotResult = _botServiceMedical.Chat(text);
            //    string result = aimlBotResult.OutputSentences[0].ToString();

            //    // Nếu trả về là module
            //    if (result.Replace("\r\n", "").Trim().Contains(CommonConstants.POSTBACK_MODULE))
            //    {
            //        if (result.Contains("<module>") != true)// k phải button module trả về
            //        {
            //            string txtModule = result.Replace("\r\n", "").Replace(".", "").Trim();
            //            txtModule = Regex.Replace(txtModule, @"<(.|\n)*?>", "").Trim();
            //            int idxModule = txtModule.IndexOf("postback_module");
            //            if (idxModule != -1)
            //            {
            //                string strPostback = txtModule.Substring(idxModule, txtModule.Length - idxModule);
            //                var punctuation = strPostback.Where(Char.IsPunctuation).Distinct().ToArray();
            //                var words = strPostback.Split().Select(x => x.Trim(punctuation));
            //                var contains = words.SingleOrDefault(x => x.Contains("postback_module") == true);

            //                if (words.ToList().Count == 1 && (txtModule.Length == contains.Length))
            //                {
            //                    return await MessageResponse(contains, senderId, botId, typeRequest);
            //                    //return await ExcuteMessage(contains, sender, botId);
            //                }

            //                string rsHandle = "";

            //                if (contains == "postback_module_api_search")
            //                {
            //                    return await MessageResponse(txtModule, senderId, botId, typeRequest);
            //                    //return await ExcuteMessage(txtModule, sender, botId);
            //                }
            //                if (contains == "postback_module_med_get_info_patient")
            //                {
            //                    return await MessageResponse(txtModule, senderId, botId, typeRequest);

            //                    //return await ExcuteMessage(txtModule, sender, botId);
            //                }
            //                if (contains == "postback_module_age")
            //                {
            //                    _plUserDb.PredicateName = "Age";
            //                    var handleAge = _handleMdService.HandledIsAge(contains, botId);
            //                    rsHandle = handleAge.TemplateJsonFacebook;
            //                }
            //                if (contains == "postback_module_email")
            //                {
            //                    _plUserDb.PredicateName = "Email";
            //                    var handleEmail = _handleMdService.HandledIsEmail(contains, botId);
            //                    rsHandle = handleEmail.TemplateJsonFacebook;
            //                }
            //                if (contains == "postback_module_phone")
            //                {
            //                    _plUserDb.PredicateName = "Phone";
            //                    var handlePhone = _handleMdService.HandleIsPhoneNumber(contains, botId);
            //                    rsHandle = handlePhone.TemplateJsonFacebook;
            //                }
            //                _plUserDb.IsHavePredicate = true;
            //                _plUserDb.PredicateValue = "";
            //                _plUserDb.IsHaveCardCondition = false;
            //                _plUserDb.CardConditionPattern = "";
            //                _plUserDb.CardStepPattern = "";
            //                _plUserDb.IsHaveSetAttributeSystem = false;
            //                _plUserDb.AttributeName = "";
            //                _plUserDb.IsConditionWithInputText = false;
            //                _plUserDb.CardConditionWithInputTextPattern = "";
            //                _appPlatformUser.Update(_plUserDb);
            //                _appPlatformUser.Save();

            //                string msg = HandleMessageJson(rsHandle, senderId);
            //                _lstBotReplyResponse.Add(msg);
            //                return await Task.FromResult<List<string>>(_lstBotReplyResponse);
            //                //return await SendMessage(rsHandle, sender);
            //            }
            //        }
            //    }

            //    if (result.Contains("NOT_MATCH"))
            //    {
            //        hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_002;
            //        AddHistory(hisVm);
            //        try
            //        {
            //            _plUserDb.IsHaveCardCondition = false;
            //            _plUserDb.CardConditionPattern = "";
            //            _plUserDb.IsConditionWithAreaButton = false;
            //            _plUserDb.CardConditionAreaButtonPattern = "";
            //            _plUserDb.CardStepPattern = "";
            //            _plUserDb.IsHaveSetAttributeSystem = false;
            //            _plUserDb.AttributeName = "";
            //            _plUserDb.IsConditionWithInputText = false;
            //            _plUserDb.CardConditionWithInputTextPattern = "";
            //            _appPlatformUser.Update(_plUserDb);
            //            _appPlatformUser.Save();
            //        }
            //        catch (Exception ex)
            //        {
            //            LogError("RS NOT MATCH:" + ex.Message);
            //        }

            //        if (_isSearchAI) //_isSearchAI
            //        {
            //            var systemConfigDb = _settingService.GetListSystemConfigByBotId(botId);
            //            var systemConfigVm = Mapper.Map<IEnumerable<BotProject.Model.Models.SystemConfig>, IEnumerable<SystemConfigViewModel>>(systemConfigDb);
            //            if (systemConfigVm.Count() == 0)
            //            {
            //                string msgTemp = FacebookTemplate.GetMessageTemplateText("Tìm kiếm xử lý ngôn ngữ tự nhiên hiện không hoạt động, bạn vui lòng thử lại sau nhé!", senderId).ToString();

            //                _lstBotReplyResponse.Add(HandleMessageJson(msgTemp, senderId));
            //                return await Task.FromResult<List<string>>(_lstBotReplyResponse);
            //                //return await SendMessage(FacebookTemplate.GetMessageTemplateText("Tìm kiếm xử lý ngôn ngữ tự nhiên hiện không hoạt động, bạn vui lòng thử lại sau nhé!", sender));// not match
            //            }
            //            string nameFunctionAPI = "";
            //            string number = "";
            //            string field = "";
            //            string valueBotId = "";
            //            foreach (var item in systemConfigVm)
            //            {
            //                if (item.Code == "UrlAPI")
            //                    nameFunctionAPI = item.ValueString;
            //                if (item.Code == "ParamBotID")
            //                    valueBotId = item.ValueString;
            //                if (item.Code == "ParamAreaID")
            //                    field = item.ValueString;
            //                if (item.Code == "ParamNumberResponse")
            //                    number = item.ValueString;
            //            }
            //            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_006;
            //            AddHistory(hisVm);

            //            // Hiển thị thêm thông tin về triệu chứng đó
            //            if (botId == 3019)
            //            {
            //                string resultSymptomp = _apiNLR.GetListSymptoms(_dicAttributeUser["content_message"], 1);
            //                if (!String.IsNullOrEmpty(resultSymptomp))
            //                {
            //                    var dataSymptomp = new JavaScriptSerializer
            //                    {
            //                        MaxJsonLength = Int32.MaxValue,
            //                        RecursionLimit = 100
            //                    }.Deserialize<List<SearchSymptomViewModel>>(resultSymptomp);
            //                    if (dataSymptomp.Count() != 0)
            //                    {
            //                        string msgFAQs = FacebookTemplate.GetMessageTemplateText("Bạn vui lòng xem thêm thông tin triệu chứng bên dưới", senderId).ToString();
            //                        _lstBotReplyResponse.Add(msgFAQs);
            //                        string strTemplateGenericRelatedMedical = FacebookTemplate.GetMessageTemplateGenericByListMed(senderId, dataSymptomp).ToString();
            //                        _lstBotReplyResponse.Add(HandleMessageJson(strTemplateGenericRelatedMedical, senderId));
            //                    }
            //                }
            //            }

            //            string resultAPI = GetRelatedQuestionToFacebook(nameFunctionAPI, text, field, "5", valueBotId);
            //            if (!String.IsNullOrEmpty(resultAPI))
            //            {
            //                var lstQnaAPI = new JavaScriptSerializer
            //                {
            //                    MaxJsonLength = Int32.MaxValue,
            //                    RecursionLimit = 100
            //                }.Deserialize<List<SearchNlpQnAViewModel>>(resultAPI);
            //                // render template json generic
            //                int totalQnA = lstQnaAPI.Count();
            //                string totalFind = "Tôi tìm thấy " + totalQnA + " câu hỏi liên quan đến câu hỏi của bạn";
            //                string msgTempTotalSearch = FacebookTemplate.GetMessageTemplateText(totalFind, senderId).ToString();
            //                _lstBotReplyResponse.Add(HandleMessageJson(msgTempTotalSearch, senderId));
            //                //await SendMessageTask(FacebookTemplate.GetMessageTemplateText(totalFind, sender).ToString(), sender);
            //                string strTemplateGenericRelatedQuestion = FacebookTemplate.GetMessageTemplateGenericByList(senderId, lstQnaAPI).ToString();
            //                _lstBotReplyResponse.Add(HandleMessageJson(strTemplateGenericRelatedQuestion, senderId));
            //                //BotLog.Info(strTemplateGenericRelatedQuestion);
            //                //return await Task.FromResult<List<string>>(_lstBotReplyResponse);
            //                //return await SendMessage(strTemplateGenericRelatedQuestion, sender);
            //            }

            //            if (_lstBotReplyResponse.Count() != 0)
            //            {
            //                return await Task.FromResult<List<string>>(_lstBotReplyResponse);
            //            }
            //        }

            //        _dicNotMatch = new Dictionary<string, string>() {
            //            {"NOT_MATCH_01", "Xin lỗi,em chưa hiểu ý anh/chị ạ!"},
            //            {"NOT_MATCH_02", "Anh/chị có thể giải thích thêm được không?"},
            //            {"NOT_MATCH_03", "Chưa hiểu lắm ạ, anh/chị có thể nói rõ hơn được không ạ?"},
            //            {"NOT_MATCH_04", "Xin lỗi, Anh/chị có thể giải thích thêm được không?"},
            //            {"NOT_MATCH_05", "Xin lỗi, Tôi chưa được học để hiểu nội dung này?"}
            //        };

            //        string strDefaultNotMatch = "";
            //        foreach (var item in _dicNotMatch)
            //        {
            //            string itemNotMatch = item.Key;
            //            if (itemNotMatch.Contains(result.Trim().Replace(".", String.Empty)))
            //            {
            //                strDefaultNotMatch = item.Value;
            //            }
            //        }

            //        string msg = FacebookTemplate.GetMessageTemplateTextAndQuickReply(strDefaultNotMatch, senderId, _contactAdmin, _titlePayloadContactAdmin).ToString();
            //        _lstBotReplyResponse.Add(HandleMessageJson(msg, senderId));
            //        return await Task.FromResult<List<string>>(_lstBotReplyResponse);
            //        //await SendMessage(FacebookTemplate.GetMessageTemplateTextAndQuickReply(strDefaultNotMatch, sender, _contactAdmin, _titlePayloadContactAdmin));// not match
            //        //return new HttpResponseMessage(HttpStatusCode.OK);
            //    }


            //    // input là postback
            //    if (text.Contains("postback_card"))
            //    {
            //        var cardDb = _cardService.GetCardByPattern(text.Replace(".", String.Empty));
            //        if (!String.IsNullOrEmpty(cardDb.AttributeSystemName))
            //        {
            //            _plUserDb.IsHaveSetAttributeSystem = true;
            //            _plUserDb.AttributeName = cardDb.AttributeSystemName;
            //        }
            //        else
            //        {
            //            _plUserDb.IsHaveSetAttributeSystem = false;
            //            _plUserDb.AttributeName = "";
            //        }

            //        if (cardDb.CardStepID != null)
            //        {
            //            _plUserDb.CardStepPattern = "postback_card_" + cardDb.CardStepID;
            //            if (cardDb.IsConditionWithInputText)// yêu cầu nhập text để chuyển sang card step
            //            {
            //                _plUserDb.IsConditionWithInputText = true;
            //                _plUserDb.CardConditionWithInputTextPattern = _plUserDb.CardStepPattern;
            //            }
            //            else
            //            {
            //                _plUserDb.IsConditionWithInputText = false;
            //                _plUserDb.CardConditionWithInputTextPattern = "";
            //            }
            //        }
            //        else
            //        {
            //            _plUserDb.CardStepPattern = "";
            //        }
            //        if (cardDb.IsHaveCondition)
            //        {
            //            _plUserDb.IsHaveCardCondition = true;
            //            _plUserDb.CardConditionPattern = text.Replace(".", String.Empty);
            //        }
            //        else
            //        {
            //            _plUserDb.IsHaveCardCondition = false;
            //            _plUserDb.CardConditionPattern = "";
            //        }

            //        if (cardDb.IsConditionWithAreaButton)
            //        {
            //            _plUserDb.IsConditionWithAreaButton = true;
            //            _plUserDb.CardConditionAreaButtonPattern = text.Replace(".", String.Empty);
            //        }
            //        else
            //        {
            //            _plUserDb.IsConditionWithAreaButton = false;
            //            _plUserDb.CardConditionAreaButtonPattern = "";
            //        }
            //        _appPlatformUser.Update(_plUserDb);
            //        _appPlatformUser.Save();
            //        string tempJsonFacebook = cardDb.TemplateJsonFacebook;
            //        if (!String.IsNullOrEmpty(tempJsonFacebook))
            //        {
            //            tempJsonFacebook = tempJsonFacebook.Trim();
            //            string[] strArrayJson = Regex.Split(tempJsonFacebook, "split");//nhớ thêm bên formcard xử lý lục trên face
            //            if (strArrayJson.Length != 0)
            //            {
            //                var strArray = strArrayJson.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            //                foreach (var temp in strArray)
            //                {
            //                    string tempJson = temp;
            //                    string msg = HandleMessageJson(tempJson, senderId);
            //                    _lstBotReplyResponse.Add(msg);
            //                    //await SendMessageTask(tempJson, sender);
            //                }
            //            }
            //        }
            //        if (cardDb.CardStepID != null && cardDb.IsConditionWithInputText == false)
            //        {
            //            _plUserDb.CardStepPattern = "postback_card_" + cardDb.CardStepID;
            //            return await MessageResponse(_plUserDb.CardStepPattern, senderId, botId, typeRequest, true);
            //        }
            //        //_lstBotReplyResponse = new List<string>();
            //        return await Task.FromResult<List<string>>(_lstBotReplyResponse);
            //    }
            //    if (text.Contains(_contactAdmin))//chat admin
            //    {
            //        _plUserDb.IsHaveCardCondition = false;
            //        _plUserDb.CardConditionPattern = "";
            //        _plUserDb.CardStepPattern = "";
            //        _plUserDb.IsHaveSetAttributeSystem = false;

            //        _appPlatformUser.Update(_plUserDb);
            //        _appPlatformUser.Save();

            //        string strTempPostbackContactAdmin = aimlBotResult.SubQueries[0].Template;
            //        bool isPostbackContactAdmin = Regex.Match(strTempPostbackContactAdmin, "<template><srai>postback_card_(\\d+)</srai></template>").Success;
            //        if (isPostbackContactAdmin)
            //        {
            //            strTempPostbackContactAdmin = Regex.Replace(strTempPostbackContactAdmin, @"<(.|\n)*?>", "").Trim();
            //            var cardDb = _cardService.GetCardByPattern(strTempPostbackContactAdmin.Replace(".", String.Empty));
            //            string tempJsonFacebook = cardDb.TemplateJsonFacebook;
            //            if (!String.IsNullOrEmpty(tempJsonFacebook))
            //            {
            //                tempJsonFacebook = tempJsonFacebook.Trim();
            //                string[] strArrayJson = Regex.Split(tempJsonFacebook, "split");//nhớ thêm bên formcard xử lý lục trên face
            //                if (strArrayJson.Length != 0)
            //                {
            //                    var strArray = strArrayJson.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            //                    foreach (var temp in strArray)
            //                    {
            //                        string tempJson = temp;
            //                        string msg = HandleMessageJson(tempJson, senderId);
            //                        _lstBotReplyResponse.Add(msg);
            //                        //await SendMessageTask(tempJson, sender);
            //                    }
            //                    return await Task.FromResult<List<string>>(_lstBotReplyResponse);
            //                    //return new HttpResponseMessage(HttpStatusCode.OK);
            //                }
            //            }
            //        }
            //    }

            //    // nếu nhập text -> output là postback
            //    string strTempPostback = aimlBotResult.SubQueries[0].Template;
            //    bool isPostback = Regex.Match(strTempPostback, "<template><srai>postback_card_(\\d+)</srai></template>").Success;
            //    if (isPostback)
            //    {
            //        strTempPostback = Regex.Replace(strTempPostback, @"<(.|\n)*?>", "").Trim();
            //        var cardDb = _cardService.GetCardByPattern(strTempPostback.Replace(".", String.Empty));
            //        if (cardDb.ID == 4031)
            //        {
            //            if (_plUserDb.Age == 0)//nếu thông tin tuổi chưa có trả về thẻ hỏi thông tin
            //            {
            //                return await MessageResponse("postback_card_8917", senderId, botId, typeRequest);
            //                //return await ExcuteMessage("postback_card_8917", sender, botId);
            //            }
            //        }

            //        if (!String.IsNullOrEmpty(cardDb.AttributeSystemName))
            //        {
            //            _plUserDb.IsHaveSetAttributeSystem = true;
            //            _plUserDb.AttributeName = cardDb.AttributeSystemName;
            //        }
            //        else
            //        {
            //            _plUserDb.IsHaveSetAttributeSystem = false;
            //            _plUserDb.AttributeName = "";
            //        }

            //        if (cardDb.CardStepID != null)
            //        {
            //            _plUserDb.CardStepPattern = "postback_card_" + cardDb.CardStepID;
            //            if (cardDb.IsConditionWithInputText)// yêu cầu nhập text để chuyển sang card step
            //            {
            //                _plUserDb.IsConditionWithInputText = true;
            //                _plUserDb.CardConditionWithInputTextPattern = _plUserDb.CardStepPattern;
            //            }
            //            else
            //            {
            //                _plUserDb.IsConditionWithInputText = false;
            //                _plUserDb.CardConditionWithInputTextPattern = "";
            //            }
            //        }
            //        else
            //        {
            //            _plUserDb.CardStepPattern = "";
            //        }

            //        if (cardDb.IsHaveCondition)
            //        {
            //            _plUserDb.IsHaveCardCondition = true;
            //            _plUserDb.CardConditionPattern = text.Replace(".", String.Empty);
            //        }
            //        else
            //        {
            //            _plUserDb.IsHaveCardCondition = false;
            //            _plUserDb.CardConditionPattern = "";
            //        }

            //        if (cardDb.IsConditionWithAreaButton)
            //        {
            //            _plUserDb.IsConditionWithAreaButton = true;
            //            _plUserDb.CardConditionAreaButtonPattern = text.Replace(".", String.Empty);
            //        }
            //        else
            //        {
            //            _plUserDb.IsConditionWithAreaButton = false;
            //            _plUserDb.CardConditionAreaButtonPattern = "";
            //        }
            //        _appPlatformUser.Update(_plUserDb);
            //        _appPlatformUser.Save();
            //        string tempJsonFacebook = cardDb.TemplateJsonFacebook;
            //        if (!String.IsNullOrEmpty(tempJsonFacebook))
            //        {
            //            tempJsonFacebook = tempJsonFacebook.Trim();
            //            string[] strArrayJson = Regex.Split(tempJsonFacebook, "split");//nhớ thêm bên formcard xử lý lục trên face
            //            if (strArrayJson.Length != 0)
            //            {
            //                var strArray = strArrayJson.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            //                foreach (var temp in strArray)
            //                {
            //                    string tempJson = temp;
            //                    string msg = HandleMessageJson(tempJson, senderId);
            //                    _lstBotReplyResponse.Add(msg);
            //                    //await SendMessageTask(tempJson, sender);
            //                }
            //            }
            //        }
            //        if (cardDb.CardStepID != null && cardDb.IsConditionWithInputText == false)
            //        {
            //            _plUserDb.CardStepPattern = "postback_card_" + cardDb.CardStepID;
            //            return await MessageResponse(_plUserDb.CardStepPattern, senderId, botId, typeRequest, true);
            //            //return await ExcuteMessage(fbUserDb.CardStepPattern, sender, botId);
            //        }

            //        //_lstBotReplyResponse = new List<string>();
            //        return await Task.FromResult<List<string>>(_lstBotReplyResponse);
            //        //return new HttpResponseMessage(HttpStatusCode.OK);
            //    }

            //    //trường hợp trả về câu hỏi random chứa postback
            //    bool isPostbackAnswer = Regex.Match(strTempPostback, "<template><srai>postback_answer_(\\d+)</srai></template>").Success;
            //    if (isPostbackAnswer)
            //    {
            //        if (result.Contains("postback_card"))
            //        {
            //            var cardDb = _cardService.GetCardByPattern(result.Replace(".", String.Empty));
            //            if (!String.IsNullOrEmpty(cardDb.AttributeSystemName))
            //            {
            //                _plUserDb.IsHaveSetAttributeSystem = true;
            //                _plUserDb.AttributeName = cardDb.AttributeSystemName;
            //            }
            //            else
            //            {
            //                _plUserDb.IsHaveSetAttributeSystem = false;
            //                _plUserDb.AttributeName = "";
            //            }

            //            if (cardDb.CardStepID != null)
            //            {
            //                _plUserDb.CardStepPattern = "postback_card_" + cardDb.CardStepID;
            //                if (cardDb.IsConditionWithInputText)// yêu cầu nhập text để chuyển sang card step
            //                {
            //                    _plUserDb.IsConditionWithInputText = true;
            //                    _plUserDb.CardConditionWithInputTextPattern = _plUserDb.CardStepPattern;
            //                }
            //                else
            //                {
            //                    _plUserDb.IsConditionWithInputText = false;
            //                    _plUserDb.CardConditionWithInputTextPattern = "";
            //                }
            //            }
            //            else
            //            {
            //                _plUserDb.CardStepPattern = "";
            //            }

            //            if (cardDb.IsHaveCondition)
            //            {
            //                _plUserDb.IsHaveCardCondition = true;
            //                _plUserDb.CardConditionPattern = text.Replace(".", String.Empty);
            //            }
            //            else
            //            {
            //                _plUserDb.IsHaveCardCondition = false;
            //                _plUserDb.CardConditionPattern = "";
            //            }

            //            if (cardDb.IsConditionWithAreaButton)
            //            {
            //                _plUserDb.IsConditionWithAreaButton = true;
            //                _plUserDb.CardConditionAreaButtonPattern = text.Replace(".", String.Empty);
            //            }
            //            else
            //            {
            //                _plUserDb.IsConditionWithAreaButton = false;
            //                _plUserDb.CardConditionAreaButtonPattern = "";
            //            }
            //            _appPlatformUser.Update(_plUserDb);
            //            _appPlatformUser.Save();
            //            string tempJsonFacebook = cardDb.TemplateJsonFacebook;
            //            if (!String.IsNullOrEmpty(tempJsonFacebook))
            //            {
            //                tempJsonFacebook = tempJsonFacebook.Trim();
            //                string[] strArrayJson = Regex.Split(tempJsonFacebook, "split");//nhớ thêm bên formcard xử lý lục trên face
            //                if (strArrayJson.Length != 0)
            //                {
            //                    var strArray = strArrayJson.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            //                    foreach (var temp in strArray)
            //                    {
            //                        string tempJson = temp;
            //                        string msg = HandleMessageJson(tempJson, senderId);
            //                        _lstBotReplyResponse.Add(msg);
            //                        //await SendMessageTask(tempJson, sender);
            //                    }
            //                }
            //            }
            //            if (cardDb.CardStepID != null && cardDb.IsConditionWithInputText == false)
            //            {
            //                //_plUserDb.CardStepPattern = "postback_card_" + cardDb.CardStepID;
            //                //return await ExcuteMessage(fbUserDb.CardStepPattern, sender, botId);
            //                _plUserDb.CardStepPattern = "postback_card_" + cardDb.CardStepID;
            //                return await MessageResponse(_plUserDb.CardStepPattern, senderId, botId, typeRequest, true);
            //            }

            //            //_lstBotReplyResponse = new List<string>();
            //            return await Task.FromResult<List<string>>(_lstBotReplyResponse);
            //            //return new HttpResponseMessage(HttpStatusCode.OK);
            //        }
            //    }
            //    // trả lời text bình thường
            //    string msgText = HandleMessageJson(FacebookTemplate.GetMessageTemplateText(result, senderId).ToString(), senderId);
            //    _lstBotReplyResponse.Add(msgText);
            //    return await Task.FromResult<List<string>>(_lstBotReplyResponse);
                ////return await SendMessage(FacebookTemplate.GetMessageTemplateText(result, sender));
            //}
            //catch (Exception ex)
            //{
            //    _lstBotReplyResponse = new List<string>();
            //}
            return await Task.FromResult<List<string>>(_lstBotReplyResponse);
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
            _plUserDb.PredicateName = "";
            _plUserDb.PredicateValue = "";
            _plUserDb.IsHaveSetAttributeSystem = false;
            _plUserDb.AttributeName = "";
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
                _plUserDb.IsHaveSetAttributeSystem = true;
                _plUserDb.AttributeName = cardDb.AttributeSystemName;
            }
            if (cardDb.IsHaveCondition)
            {
                _plUserDb.PredicateName = "REQUIRE_CLICK_BUTTON_TO_NEXT_CARD";
                _plUserDb.PredicateValue = patternCard;
            }
            if (cardDb.CardStepID != null && cardDb.IsConditionWithInputText)
            {
                _plUserDb.PredicateName = "REQUIRE_INPUT_TEXT_TO_NEXT_CARD";
                _plUserDb.PredicateValue = CommonConstants.PostBackCard + cardDb.CardStepID;
            }
            if (cardDb.CardStepID != null && cardDb.IsConditionWithInputText == false)
            {
                _plUserDb.PredicateName = "AUTO_NEXT_CARD";
                _plUserDb.PredicateValue = CommonConstants.PostBackCard + cardDb.CardStepID;
            }
            if (cardDb.IsConditionWithAreaButton)
            {
                _plUserDb.PredicateName = "VERIFY_TEXT_WITH_AREA_BUTTON";
            }
            UpdateStatusAppUser(_plUserDb);
            return tempCardFb;
        }
        private string HandlePostbackModule(string postbackModule, string text, int botId, bool isFristRequest)
        {
            string templateHandle = "";
            _plUserDb.IsHaveSetAttributeSystem = false;
            _plUserDb.AttributeName = "";
            _plUserDb.PredicateName = "POSTBACK_MODULE";
            if (postbackModule.Contains(CommonConstants.ModuleSearchAPI))
            {
                string mdSearchId = postbackModule.Replace("postback_module_api_search_", "");
                if (isFristRequest)
                {
                    var handleMdSearch = _handleMdService.HandleIsSearchAPI(postbackModule, mdSearchId, "");
                    templateHandle = handleMdSearch.TemplateJsonFacebook;
                }
                else
                {
                    var handleMdSearch = _handleMdService.HandleIsSearchAPI(text, mdSearchId, "");
                    templateHandle = handleMdSearch.TemplateJsonFacebook;
                }
                _plUserDb.PredicateValue = postbackModule;
            }
            if (postbackModule.Contains(CommonConstants.ModuleAdminContact))
            {
                var handleAdminContact = _handleMdService.HandleIsAdminContact(text, botId);
                templateHandle = handleAdminContact.TemplateJsonFacebook;
                _plUserDb.PredicateValue = postbackModule;
            }

            UpdateStatusAppUser(_plUserDb);
            return templateHandle;
        }
        #endregion

        private ApplicationPlatformUser UpdateStatusAppUser(ApplicationPlatformUser appUserVm)
        {
            _appPlatformUser.Update(appUserVm);
            _appPlatformUser.Save();
            return appUserVm;
        }
        private string HandleMessageJson(string msgJson, string senderId)
        {
            msgJson = msgJson.Replace("{{senderId}}", senderId);
            //msgJson = Regex.Replace(msgJson, "File/", Domain + "File/");
            msgJson = Regex.Replace(msgJson, "<br />", "\\n");
            msgJson = Regex.Replace(msgJson, "<br/>", "\\n");
            msgJson = Regex.Replace(msgJson, @"\\n\\n", "\\n");
            msgJson = Regex.Replace(msgJson, @"\\n\\r\\n", "\\n");
            if (msgJson.Contains("{{"))
            {
                if (_dicAttributeUser != null && _dicAttributeUser.Count() != 0)
                {
                    foreach (var item in _dicAttributeUser)
                    {
                        string val = String.IsNullOrEmpty(item.Value) == true ? "N/A" : item.Value;
                        msgJson = msgJson.Replace("{{" + item.Key + "}}", val);
                    }
                }
            }
            return msgJson;
        }

        private string HandleMessageSymptomp(string msgJson, string senderId)
        {
            msgJson = msgJson.Replace("{{senderId}}", senderId);
            //msgJson = Regex.Replace(msgJson, "File/", Domain + "File/");
            msgJson = Regex.Replace(msgJson, "<br />", "\\n");
            msgJson = Regex.Replace(msgJson, "<br/>", "\\n");
            msgJson = Regex.Replace(msgJson, @"\\n\\n", "\\n");
            msgJson = Regex.Replace(msgJson, @"\\n\\r\\n", "\\n");
            return msgJson;
        }

        private void AddAttributeDefault(string userId, int botId, string key, string value)
        {
            AttributePlatformUser attPlatformUser = new AttributePlatformUser();
            attPlatformUser.UserID = userId;
            attPlatformUser.BotID = botId;
            attPlatformUser.AttributeKey = key;
            attPlatformUser.AttributeValue = value;
            attPlatformUser.TypeDevice = "kiosk";
            _attributeService.CreateUpdateAttributePlatform(attPlatformUser);
            _attributeService.Save();
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
            catch (Exception ex)
            {

            }
        }
        private void AddHistory(HistoryViewModel hisVm)
        {
            History hisDb = new History();
            hisDb.UpdateHistory(hisVm);
            hisDb.Type = "kiosk";
            _historyService.Create(hisDb);
            _historyService.Save();
        }

        public class Message
        {
            public string senderId { set; get; }//K2542621755855091
            public string botId { set; get; }
            public string text { set; get; }
        }

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
        public class ResultBot
        {
            public string Type { set; get; }
            public int Total { set; get; }
            public string PatternPayload { set; get; }
        }
    }
}
