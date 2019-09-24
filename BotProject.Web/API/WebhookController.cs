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
using System.Text.RegularExpressions;
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

        private readonly string Domain = Helper.ReadString("Domain");
        private readonly string UrlAPI = Helper.ReadString("UrlAPI");
        private readonly string KeyAPI = Helper.ReadString("KeyAPI");
        private string pathAIML = PathServer.PathAIML;
        private string pathSetting = PathServer.PathAIML + "config";

        private Dictionary<string, string> _dicNotMatch;

        private IApplicationFacebookUserService _appFacebookUser;
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
                              ICardService cardService,
                              IApplicationFacebookUserService appFacebookUser)
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
            _appFacebookUser = appFacebookUser;
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
            //    return new HttpResponseMessage(HttpStatusCode.BadRequest);

            var value = JsonConvert.DeserializeObject<FacebookBotRequest>(body);

            ////Test
            //var botRequest = new JavaScriptSerializer().Serialize(value);
            //LogError(botRequest);

            if (value.@object != "page")
                return new HttpResponseMessage(HttpStatusCode.OK);

            int botId = 5028;
            var lstAIML = _aimlFileService.GetByBotId(botId);
            var lstAIMLVm = Mapper.Map<IEnumerable<AIMLFile>, IEnumerable<AIMLViewModel>>(lstAIML);
            _botService.loadAIMLFromDatabase(lstAIMLVm);
            _user = _botService.loadUserBot(value.entry[0].messaging[0].sender.id);
            //_user.Predicates.addSetting("agecheck", "false");

            foreach (var item in value.entry[0].messaging)
            {
                if (item.message == null && item.postback == null)
                {
                    continue;
                }
                else if(item.message == null && item.postback != null)
                {
                    await ExcuteMessage(item.postback.payload, item.sender.id, botId);
                }
                else
                {
                    if(item.message.quick_reply != null)
                    {
                        await ExcuteMessage(item.message.quick_reply.payload, item.sender.id, botId);
                    }
                    else
                    {
                        await ExcuteMessage(item.message.text, item.sender.id, botId);
                    }
                }
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> ExcuteMessage(string text, string sender, int botId)
        {
            text = Regex.Replace(text, @"<(.|\n)*?>", "").Trim();

            HistoryViewModel hisVm = new HistoryViewModel();
            hisVm.BotID = botId;
            hisVm.CreatedDate = DateTime.Now;
            hisVm.UserSay = text;
            hisVm.UserName = sender;
            hisVm.Type = CommonConstants.TYPE_FACEBOOK;

            try
            {
                ApplicationFacebookUser fbUserDb = new ApplicationFacebookUser();
                fbUserDb = _appFacebookUser.GetByUserId(sender);
                if (fbUserDb == null)
                {
                    fbUserDb = new ApplicationFacebookUser();
                    ApplicationFacebookUserViewModel fbUserVm = new ApplicationFacebookUserViewModel();
                    fbUserVm.UserId = sender;
                    fbUserVm.IsHavePredicate = false;
                    fbUserVm.IsProactiveMessage = false;
                    fbUserDb.UpdateFacebookUser(fbUserVm);
                    _appFacebookUser.Add(fbUserDb);
                    _appFacebookUser.Save();
                }
                else
                {
                    fbUserDb.StartedOn = DateTime.Now;
                    // Điều kiện xử lý module
                    if (fbUserDb.IsHavePredicate)
                    {
                        var predicateName = fbUserDb.PredicateName;
                        if (predicateName == "ApiSearch")
                        {
                            if (text.Contains("postback_card"))// nều còn điều kiện search mà chọn postback
                            {
                                fbUserDb.IsHavePredicate = false;
                                fbUserDb.PredicateName = "";
                                fbUserDb.PredicateValue = "";
                                _appFacebookUser.Update(fbUserDb);
                                _appFacebookUser.Save();
                                return await ExcuteMessage(text, sender, botId);
                            }

                            string predicateValue = fbUserDb.PredicateValue;
                            var handleMdSearch = _handleMdService.HandleIsSearchAPI(text, predicateValue, "");

                            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_005;
                            AddHistory(hisVm);

                            return await SendMessage(handleMdSearch.TemplateJsonFacebook, sender);

                        }
                        if (predicateName == "Age")
                        {
                            var handleAge = _handleMdService.HandledIsAge(text, botId);
                            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_004;
                            AddHistory(hisVm);
                            if (handleAge.Status)// đúng age
                            {
                                fbUserDb.IsHavePredicate = false;
                                fbUserDb.PredicateName = "";
                                fbUserDb.PredicateValue = "";
                                fbUserDb.PhoneNumber = text;
                                _appFacebookUser.Update(fbUserDb);
                                _appFacebookUser.Save();
                                if (!String.IsNullOrEmpty(handleAge.Postback))
                                {
                                    return await ExcuteMessage(handleAge.Postback, sender, botId);
                                }
                            }
                            return await SendMessage(handleAge.TemplateJsonFacebook, sender);
                        }
                        if (predicateName == "Phone")
                        {
                            var handlePhone = _handleMdService.HandleIsPhoneNumber(text, botId);

                            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_004;
                            AddHistory(hisVm);
                            if (handlePhone.Status)// đúng số dt
                            {
                                fbUserDb.IsHavePredicate = false;
                                fbUserDb.PredicateName = "";
                                fbUserDb.PredicateValue = "";
                                fbUserDb.PhoneNumber = text;
                                _appFacebookUser.Update(fbUserDb);
                                _appFacebookUser.Save();

                                // Nếu đúng số điện thoại sẽ trả về thẻ tiếp theo nếu có
                                if (!String.IsNullOrEmpty(handlePhone.Postback))
                                {
                                    return await ExcuteMessage(handlePhone.Postback, sender, botId);
                                }
                            }
                            return await SendMessage(handlePhone.TemplateJsonFacebook, sender);
                        }
                        if (predicateName == "Email")
                        {
                            var handleEmail = _handleMdService.HandledIsEmail(text, botId);
                            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_004;
                            AddHistory(hisVm);
                            if (handleEmail.Status)// đúng email
                            {
                                fbUserDb.IsHavePredicate = false;
                                fbUserDb.PredicateName = "";
                                fbUserDb.PredicateValue = "";
                                _appFacebookUser.Update(fbUserDb);
                                _appFacebookUser.Save();

                                if (!String.IsNullOrEmpty(handleEmail.Postback))
                                {
                                    return await ExcuteMessage(handleEmail.Postback, sender, botId);
                                }
                            }
                            return await SendMessage(handleEmail.TemplateJsonFacebook, sender);
                        }
                        if (predicateName == "Voucher")
                        {
                            string mdVoucherId = fbUserDb.PredicateValue;
                            if (text.Contains("postback_card"))
                            {
                                fbUserDb.IsHavePredicate = false;
                                fbUserDb.PredicateName = "";
                                fbUserDb.PredicateValue = "";
                                _appFacebookUser.Update(fbUserDb);
                                _appFacebookUser.Save();
                                return await ExcuteMessage(text, sender, botId);
                            }

                            var handleMdVoucher = _handleMdService.HandleIsVoucher(text, mdVoucherId, hisVm.Type);

                            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_007;
                            AddHistory(hisVm);
                            if (handleMdVoucher.Status)
                            {
                                fbUserDb.IsHavePredicate = true;
                                fbUserDb.PredicateName = "IsVoucherOTP";
                                fbUserDb.PredicateValue = mdVoucherId;// voucherId
                                fbUserDb.PhoneNumber = text;
                                _appFacebookUser.Update(fbUserDb);
                                _appFacebookUser.Save();
                            }
                            return await SendMessage(handleMdVoucher.TemplateJsonFacebook, sender);
                        }
                        if (predicateName == "IsVoucherOTP")
                        {
                            string mdVoucherId = fbUserDb.PredicateValue;
                            string phoneNumber = fbUserDb.PhoneNumber;
                            if (text.Contains("postback_card"))
                            {
                                fbUserDb.IsHavePredicate = false;
                                fbUserDb.PredicateName = "";
                                fbUserDb.PredicateValue = "";
                                _appFacebookUser.Update(fbUserDb);
                                _appFacebookUser.Save();
                                return await ExcuteMessage(text, sender, botId);
                            }
                            var handleOTP = _handleMdService.HandleIsCheckOTP(text, phoneNumber, mdVoucherId);
                            if (handleOTP.Status)
                            {
                                fbUserDb.IsHavePredicate = false;
                                fbUserDb.PredicateName = "";
                                fbUserDb.PredicateValue = "";
                                _appFacebookUser.Update(fbUserDb);
                                _appFacebookUser.Save();
                            }
                            return await SendMessage(handleOTP.TemplateJsonFacebook, sender);
                        }
                    }
                    else // Input: Khởi tạo module được chọn
                    {
                        if (text.Contains(CommonConstants.ModuleSearchAPI))
                        {
                            string mdSearchId = text.Replace(".", String.Empty).Replace("postback_module_api_search_", "");
                            var handleMdSearch = _handleMdService.HandleIsSearchAPI(text, mdSearchId, "");
                            fbUserDb.IsHavePredicate = true;
                            fbUserDb.PredicateName = "ApiSearch";
                            fbUserDb.PredicateValue = mdSearchId;
                            _appFacebookUser.Update(fbUserDb);
                            _appFacebookUser.Save();

                            hisVm.UserSay = "[Tra cứu]";
                            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_003;
                            AddHistory(hisVm);

                            return await SendMessage(handleMdSearch.TemplateJsonFacebook, sender);

                        }
                        if (text.Contains(CommonConstants.ModuleAge))
                        {
                            var handleAge = _handleMdService.HandledIsAge(text, botId);

                            fbUserDb.IsHavePredicate = true;
                            fbUserDb.PredicateName = "Age";
                            _appFacebookUser.Update(fbUserDb);
                            _appFacebookUser.Save();

                            hisVm.UserSay = "[Tuổi]";
                            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_003;
                            AddHistory(hisVm);

                            return await SendMessage(handleAge.TemplateJsonFacebook, sender);
                        }
                        if (text.Contains(CommonConstants.ModulePhone))
                        {
                            var handlePhone = _handleMdService.HandleIsPhoneNumber(text, botId);
                            fbUserDb.IsHavePredicate = true;
                            fbUserDb.PredicateName = "Phone";
                            _appFacebookUser.Update(fbUserDb);
                            _appFacebookUser.Save();

                            hisVm.UserSay = "[Số điện thoại]";
                            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_003;
                            AddHistory(hisVm);

                            return await SendMessage(handlePhone.TemplateJsonFacebook, sender);
                        }
                        if (text.Contains(CommonConstants.ModuleEmail))
                        {
                            var handleEmail = _handleMdService.HandledIsEmail(text, botId);
                            fbUserDb.IsHavePredicate = true;
                            fbUserDb.PredicateName = "Email";
                            _appFacebookUser.Update(fbUserDb);
                            _appFacebookUser.Save();

                            hisVm.UserSay = "[Email]";
                            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_003;
                            AddHistory(hisVm);

                            return await SendMessage(handleEmail.TemplateJsonFacebook, sender);
                        }

                        if (text.Contains(CommonConstants.ModuleVoucher))
                        {
                            string mdVoucherId = text.Replace(".", String.Empty).Replace("postback_module_voucher_", "");
                            var handleMdVoucher = _handleMdService.HandleIsVoucher(text, mdVoucherId, hisVm.Type);

                            fbUserDb.IsHavePredicate = true;
                            fbUserDb.PredicateName = "Voucher";
                            fbUserDb.PredicateValue = mdVoucherId;
                            _appFacebookUser.Update(fbUserDb);
                            _appFacebookUser.Save();

                            hisVm.UserSay = "[Voucher]";
                            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_003;
                            AddHistory(hisVm);

                            return await SendMessage(handleMdVoucher.TemplateJsonFacebook, sender);
                        }

                    }
                }

                // Lấy target from knowledge base QnA trained mongodb
                if (text.Contains("postback") == false || text.Contains("module") == false)
                {
                    string target = _apiNLR.GetPrecidictTextClass(text, botId);
                    if (!String.IsNullOrEmpty(target))
                    {
                        target = Regex.Replace(target, "\n", "").Replace("\"", "");
                        QuesTargetViewModel quesTarget = new QuesTargetViewModel();
                        quesTarget = _qnaService.GetQuesByTarget(target, botId);
                        if (quesTarget != null)
                        {
                            text = quesTarget.ContentText;
                        }
                        hisVm.BotUnderStands = target;
                    }
                }

                AIMLbot.Result aimlBotResult = _botService.Chat(text, _user);
                string result = aimlBotResult.OutputSentences[0].ToString();
                // lưu lịch sử
                if (text.Contains("postback_card"))
                {
                    var cardDb = _cardService.GetSingleCondition(text);
                    if (cardDb != null)
                    {
                        hisVm.UserSay = "[" + cardDb.Name + "]";
                        hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_001;
                        AddHistory(hisVm);
                    }
                }
                else if (text.Contains("postback_module") == false && result.Contains("NOT_MATCH") == false)
                {
                    hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_003;
                    AddHistory(hisVm);
                }

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
                            _appFacebookUser.Update(fbUserDb);
                            _appFacebookUser.Save();

                            return await SendMessage(rsHandle, sender);
                        }
                    }
                }
                if (result.Contains("NOT_MATCH"))
                {
                    _dicNotMatch = new Dictionary<string, string>() {
                        {"NOT_MATCH_01", "Xin lỗi, Tôi không hiểu"},
                        {"NOT_MATCH_02", "Bạn có thể giải thích thêm được không?"},
                        {"NOT_MATCH_03", "Tôi không thể tìm thấy, bạn có thể nói rõ hơn?"},
                        {"NOT_MATCH_04", "Xin lỗi, Bạn có thể giải thích thêm được không?"},
                        {"NOT_MATCH_05", "Tôi không thể tìm thấy"}
                    };

                    // Chuyển tới tìm kiếm Search NLP
                    var systemConfigDb = _settingService.GetListSystemConfigByBotId(botId);
                    var systemConfigVm = Mapper.Map<IEnumerable<BotProject.Model.Models.SystemConfig>, IEnumerable<SystemConfigViewModel>>(systemConfigDb);
                    if (systemConfigVm.Count() == 0)
                    {
                        return await SendMessage(FacebookTemplate.GetMessageTemplateText("Vui lòng kích hoạt tìm kiếm API", sender));// not match
                    }
                    string nameFunctionAPI = "";
                    string number = "";
                    string field = "";
                    foreach (var item in systemConfigVm)
                    {
                        if (item.Code == "UrlAPI")
                            nameFunctionAPI = item.ValueString;
                        if (item.Code == "ParamAreaID")
                            field = item.ValueString;
                        if (item.Code == "ParamNumberResponse")
                            number = item.ValueString;
                    }
                    hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_006;
                    AddHistory(hisVm);
                    string resultAPI = GetRelatedQuestionToFacebook(nameFunctionAPI, text, field, number, botId.ToString());
                    if (!String.IsNullOrEmpty(resultAPI))
                    {
                        var lstQnaAPI = new JavaScriptSerializer
                        {
                            MaxJsonLength = Int32.MaxValue,
                            RecursionLimit = 100
                        }.Deserialize<List<SearchNlpQnAViewModel>>(resultAPI);
                        // render template json generic

                    }
                    else
                    {
                        string strDefaultNotMatch = "Xin lỗi! Tôi không hiểu";
                        foreach (var item in _dicNotMatch)
                        {
                            string itemNotMatch = item.Key;
                            if (itemNotMatch.Contains(result.Trim().Replace(".", String.Empty)))
                            {
                                strDefaultNotMatch = item.Value;
                            }
                        }
                        return await SendMessage(FacebookTemplate.GetMessageTemplateText(strDefaultNotMatch, sender));// not match
                    }
                }
                // input là postback
                if (text.Contains("postback_card"))
                {
                    var cardDb = _cardService.GetSingleCondition(text.Replace(".", String.Empty));
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

                // output là postback
                string strTempPostback = aimlBotResult.SubQueries[0].Template;
                bool isPostback = Regex.Match(strTempPostback, "<template><srai>postback_card_(\\d+)</srai></template>").Success;
                if (isPostback)
                {
                    strTempPostback = Regex.Replace(strTempPostback, @"<(.|\n)*?>", "").Trim();
                    var cardDb = _cardService.GetSingleCondition(strTempPostback.Replace(".", String.Empty));
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

                return await SendMessage(FacebookTemplate.GetMessageTemplateText(result, sender));

            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }          
        }

        /// <summary>
        /// get text message template
        /// </summary>
        /// <param name="text">text</param>
        /// <param name="sender">sender id</param>
        /// <returns>json</returns>
        private JObject GetMessageTemplate(string text, string sender,int botId)
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
        private async Task<HttpResponseMessage> SendMessage(JObject json)
        {
            HttpResponseMessage res;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                res = await client.PostAsync($"https://graph.facebook.com/v3.2/me/messages?access_token={pageToken}", new StringContent(json.ToString(), Encoding.UTF8, "application/json"));
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
                templateJson = Regex.Replace(templateJson, "<br />", "\\n");
                templateJson = Regex.Replace(templateJson, "<br/>", "\\n");
                templateJson = Regex.Replace(templateJson, @"\\n\\n", "\\n");
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    res = await client.PostAsync($"https://graph.facebook.com/v3.2/me/messages?access_token={pageToken}", new StringContent(templateJson, Encoding.UTF8, "application/json"));
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
                templateJson = Regex.Replace(templateJson, "<br />", "\\n");
                templateJson = Regex.Replace(templateJson, "<br/>", "\\n");
                templateJson = Regex.Replace(templateJson, @"\\n\\n", "\\n");
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage res = await client.PostAsync($"https://graph.facebook.com/v3.2/me/messages?access_token={pageToken}", new StringContent(templateJson, Encoding.UTF8, "application/json"));
                }
            }
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

    }
}
