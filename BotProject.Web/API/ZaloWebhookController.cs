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
    public class ZaloWebhookController : ApiController
    {
        string pageToken = "OUQpPbeaQqbjuhvZLIjhJstikczICc8EDPBCEaCHQKStjV4gJ1fqPJEwnaeBPsSmOjhq7rL485CgnvKRSa97SYpW_bXRIMe04el3A2e9Rb9ca-C63ZKn9dcAbbeV0oDqMgos31yhEqLhhvfhD1HC45sWnKKR51GAUwEWHaS_0Xbvzhf94qu34LxQXKOVQI8gGwdxGpKILZjqrS1cAK5hAHA7oNvA3qzIVPpUA1quUtTTaDms2YflNsxgvNCMIZmDV8EVTZCa7ITJPWxmu1bSE657";
        //string appSecret = Helper.ReadString("AppSecret");
        //string verifytoken = Helper.ReadString("VerifyTokenWebHook");

        private readonly string Domain = Helper.ReadString("Domain");
        private readonly string UrlAPI = Helper.ReadString("UrlAPI");
        private readonly string KeyAPI = Helper.ReadString("KeyAPI");
        private string pathAIML = PathServer.PathAIML;
        private string pathSetting = PathServer.PathAIML + "config";

        private Dictionary<string, string> _dicNotMatch;

        private IApplicationZaloUserService _appZaloUser;
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

        public ZaloWebhookController(IErrorService errorService,
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
                              IApplicationZaloUserService appZaloUser)
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
            _appZaloUser = appZaloUser;
        }

        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post()
        {
            var body = await Request.Content.ReadAsStringAsync();
            //LogError(body);
            if (body.Contains("user_send_text"))
            {
                var value = JsonConvert.DeserializeObject<ZaloBotRequest>(body);
                //LogError(body);

                int botId = 5028;
                var lstAIML = _aimlFileService.GetByBotId(botId);
                var lstAIMLVm = Mapper.Map<IEnumerable<AIMLFile>, IEnumerable<AIMLViewModel>>(lstAIML);
                _botService.loadAIMLFromDatabase(lstAIMLVm);
                _user = _botService.loadUserBot(value.sender.id);

                await ExcuteMessage(value.message.text, value.sender.id, botId);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            // sự kiện người dùng quan tâm
            if(body.Contains("follower"))
            {
                int botId = 5028;
                var value = JsonConvert.DeserializeObject<ZaloBotRequest>(body);
                var settingDb = _settingService.GetSettingByBotID(botId);
                var settingVm = Mapper.Map<BotProject.Model.Models.Setting, BotSettingViewModel>(settingDb);
                if (settingVm.CardID.HasValue)
                {
                    var lstAIML = _aimlFileService.GetByBotId(botId);
                    var lstAIMLVm = Mapper.Map<IEnumerable<AIMLFile>, IEnumerable<AIMLViewModel>>(lstAIML);
                    _botService.loadAIMLFromDatabase(lstAIMLVm);
                    _user = _botService.loadUserBot(value.follower.id);
                    string getStartCardPayload = "postback_card_" + settingVm.CardID;
                    await ExcuteMessage(getStartCardPayload, value.follower.id, botId);

                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }


        private async Task<HttpResponseMessage> ExcuteMessage(string text, string sender, int botId)
        {
            text = Regex.Replace(text, @"<(.|\n)*?>", "").Trim();
            text = Regex.Replace(text, @"\p{Cs}", "").Trim();// remove emoji
            text = Regex.Replace(text, @"#", "").Trim();

            HistoryViewModel hisVm = new HistoryViewModel();
            hisVm.BotID = botId;
            hisVm.CreatedDate = DateTime.Now;
            hisVm.UserSay = text;
            hisVm.UserName = sender;
            hisVm.Type = CommonConstants.TYPE_ZALO;

            try
            {
                ApplicationZaloUser zlUserDb = new ApplicationZaloUser();
                zlUserDb = _appZaloUser.GetByUserId(sender);
                if (zlUserDb == null)
                {
                    zlUserDb = new ApplicationZaloUser();
                    ApplicationZaloUserViewModel zlUserVm = new ApplicationZaloUserViewModel();
                    zlUserVm.UserId = sender;
                    zlUserVm.IsHavePredicate = false;
                    zlUserVm.IsProactiveMessage = false;
                    zlUserDb.UpdateZaloUser(zlUserVm);
                    _appZaloUser.Add(zlUserDb);
                    _appZaloUser.Save();
                }
                else
                {
                    zlUserDb.StartedOn = DateTime.Now;
                    // Điều kiện xử lý module
                    if (zlUserDb.IsHavePredicate)
                    {
                        var predicateName = zlUserDb.PredicateName;
                        if (predicateName == "ApiSearch")
                        {
                            if (text.Contains("postback_card"))// nều còn điều kiện search mà chọn postback
                            {
                                zlUserDb.IsHavePredicate = false;
                                zlUserDb.PredicateName = "";
                                zlUserDb.PredicateValue = "";
                                _appZaloUser.Update(zlUserDb);
                                _appZaloUser.Save();
                                return await ExcuteMessage(text, sender, botId);
                            }

                            string predicateValue = zlUserDb.PredicateValue;
                            var handleMdSearch = _handleMdService.HandleIsSearchAPI(text, predicateValue, "");

                            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_005;
                            AddHistory(hisVm);

                            return await SendMessage(handleMdSearch.TemplateJsonZalo, sender);

                        }
                        if (predicateName == "Age")
                        {
                            var handleAge = _handleMdService.HandledIsAge(text, botId);
                            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_004;
                            AddHistory(hisVm);
                            if (handleAge.Status)// đúng age
                            {
                                zlUserDb.IsHavePredicate = false;
                                zlUserDb.PredicateName = "";
                                zlUserDb.PredicateValue = "";
                                zlUserDb.PhoneNumber = text;
                                _appZaloUser.Update(zlUserDb);
                                _appZaloUser.Save();
                                if (!String.IsNullOrEmpty(handleAge.Postback))
                                {
                                    return await ExcuteMessage(handleAge.Postback, sender, botId);
                                }
                            }
                            return await SendMessage(handleAge.TemplateJsonZalo, sender);
                        }
                        if (predicateName == "Phone")
                        {
                            var handlePhone = _handleMdService.HandleIsPhoneNumber(text, botId);

                            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_004;
                            AddHistory(hisVm);
                            if (handlePhone.Status)// đúng số dt
                            {
                                zlUserDb.IsHavePredicate = false;
                                zlUserDb.PredicateName = "";
                                zlUserDb.PredicateValue = "";
                                zlUserDb.PhoneNumber = text;
                                _appZaloUser.Update(zlUserDb);
                                _appZaloUser.Save();

                                // Nếu đúng số điện thoại sẽ trả về thẻ tiếp theo nếu có
                                if (!String.IsNullOrEmpty(handlePhone.Postback))
                                {
                                    return await ExcuteMessage(handlePhone.Postback, sender, botId);
                                }
                            }
                            return await SendMessage(handlePhone.TemplateJsonZalo, sender);
                        }
                        if (predicateName == "Email")
                        {
                            var handleEmail = _handleMdService.HandledIsEmail(text, botId);
                            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_004;
                            AddHistory(hisVm);
                            if (handleEmail.Status)// đúng email
                            {
                                zlUserDb.IsHavePredicate = false;
                                zlUserDb.PredicateName = "";
                                zlUserDb.PredicateValue = "";
                                _appZaloUser.Update(zlUserDb);
                                _appZaloUser.Save();

                                if (!String.IsNullOrEmpty(handleEmail.Postback))
                                {
                                    return await ExcuteMessage(handleEmail.Postback, sender, botId);
                                }
                            }
                            return await SendMessage(handleEmail.TemplateJsonZalo, sender);
                        }
						if (predicateName == "Engineer_Name")
						{
							var handleEngineerName = _handleMdService.HandleIsEngineerName(text, botId);
							hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_004;
							AddHistory(hisVm);
							if (handleEngineerName.Status)
							{
								zlUserDb.IsHavePredicate = false;
								zlUserDb.PredicateName = "";
								zlUserDb.PredicateValue = "";
								zlUserDb.EngineerName = text;
								if (text.Contains("postback"))
								{
									zlUserDb.EngineerName = "";
								}
								_appZaloUser.Update(zlUserDb);
								_appZaloUser.Save();

								if (!String.IsNullOrEmpty(handleEngineerName.Postback))
								{
									return await ExcuteMessage(handleEngineerName.Postback, sender, botId);
								}
							}
							return await SendMessage(handleEngineerName.TemplateJsonZalo, sender);
						}
						if (predicateName == "Voucher")
                        {
                            string mdVoucherId = zlUserDb.PredicateValue;
                            if (text.Contains("postback_card"))
                            {
                                zlUserDb.IsHavePredicate = false;
                                zlUserDb.PredicateName = "";
                                zlUserDb.PredicateValue = "";
                                _appZaloUser.Update(zlUserDb);
                                _appZaloUser.Save();
                                return await ExcuteMessage(text, sender, botId);
                            }

                            var handleMdVoucher = _handleMdService.HandleIsVoucher(text, mdVoucherId, zlUserDb.EngineerName, hisVm.Type);

                            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_007;
                            AddHistory(hisVm);
                            if (handleMdVoucher.Status)
                            {
								string telePhoneNumber = text;
								string[] strArrSpecial = new string[] { "-", " ", ",", ":" };
								//check phonenumber có kèm theo serialnumber không
								foreach (var item in strArrSpecial)
								{
									if (text.Contains(item))
									{
										var arrStrPhone = Regex.Split(text, item);
										telePhoneNumber = arrStrPhone[0];
										break;
									}
								}

								zlUserDb.IsHavePredicate = true;
                                zlUserDb.PredicateName = "IsVoucherOTP";
                                zlUserDb.PredicateValue = mdVoucherId;// voucherId
                                zlUserDb.PhoneNumber = telePhoneNumber;
                                _appZaloUser.Update(zlUserDb);
                                _appZaloUser.Save();
								// send otp
								await SendMessageTask(handleMdVoucher.TemplateJsonZalo, sender);
                                //return await SendMessage(ZaloTemplate.GetMessageTemplateText(("Mã OTP đang được gửi, Anh/Chị chờ tí nhé...").ToString(), sender));
                            }
                            return await SendMessage(handleMdVoucher.TemplateJsonZalo, sender);
                        }
                        if (predicateName == "IsVoucherOTP")
                        {
                            string mdVoucherId = zlUserDb.PredicateValue;
                            string phoneNumber = zlUserDb.PhoneNumber;
                            if (text.Contains("postback_card"))
                            {
                                zlUserDb.IsHavePredicate = false;
                                zlUserDb.PredicateName = "";
                                zlUserDb.PredicateValue = "";
                                _appZaloUser.Update(zlUserDb);
                                _appZaloUser.Save();
                                return await ExcuteMessage(text, sender, botId);
                            }
                            var handleOTP = _handleMdService.HandleIsCheckOTP(text, phoneNumber, mdVoucherId);
                            if (handleOTP.Status)
                            {
                                zlUserDb.IsHavePredicate = false;
                                zlUserDb.PredicateName = "";
                                zlUserDb.PredicateValue = "";
                                _appZaloUser.Update(zlUserDb);
                                _appZaloUser.Save();

								// trả về image voucher + text message end
								string[] arrMsgHandleOTP = Regex.Split(handleOTP.TemplateJsonZalo, "split");
								foreach (var itemMessageJson in arrMsgHandleOTP)
								{
									await SendMessageTask(itemMessageJson, sender);
								}
								return new HttpResponseMessage(HttpStatusCode.OK);

							}
                            return await SendMessage(handleOTP.TemplateJsonZalo, sender);
                        }
                    }
                    else // Input: Khởi tạo module được chọn
                    {
                        if (text.Contains(CommonConstants.ModuleSearchAPI))
                        {
                            string mdSearchId = text.Replace(".", String.Empty).Replace("postback_module_api_search_", "");
                            var handleMdSearch = _handleMdService.HandleIsSearchAPI(text, mdSearchId, "");
                            zlUserDb.IsHavePredicate = true;
                            zlUserDb.PredicateName = "ApiSearch";
                            zlUserDb.PredicateValue = mdSearchId;
                            _appZaloUser.Update(zlUserDb);
                            _appZaloUser.Save();

                            hisVm.UserSay = "[Tra cứu]";
                            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_003;
                            AddHistory(hisVm);

                            return await SendMessage(handleMdSearch.TemplateJsonZalo, sender);

                        }
						if (text.Contains(CommonConstants.ModuleEngineerName))
						{
							var handleEngineerName = _handleMdService.HandleIsEngineerName(text, botId);

							zlUserDb.IsHavePredicate = true;
							zlUserDb.PredicateName = "Engineer_Name";
							_appZaloUser.Update(zlUserDb);
							_appZaloUser.Save();

							hisVm.UserSay = "[Tên hoặc mã kỹ sư]";
							hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_003;
							AddHistory(hisVm);

							return await SendMessage(handleEngineerName.TemplateJsonZalo, sender);
						}
						if (text.Contains(CommonConstants.ModuleAge))
                        {
                            var handleAge = _handleMdService.HandledIsAge(text, botId);

                            zlUserDb.IsHavePredicate = true;
                            zlUserDb.PredicateName = "Age";
                            _appZaloUser.Update(zlUserDb);
                            _appZaloUser.Save();

                            hisVm.UserSay = "[Tuổi]";
                            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_003;
                            AddHistory(hisVm);

                            return await SendMessage(handleAge.TemplateJsonZalo, sender);
                        }
                        if (text.Contains(CommonConstants.ModulePhone))
                        {
                            var handlePhone = _handleMdService.HandleIsPhoneNumber(text, botId);
                            zlUserDb.IsHavePredicate = true;
                            zlUserDb.PredicateName = "Phone";
                            _appZaloUser.Update(zlUserDb);
                            _appZaloUser.Save();

                            hisVm.UserSay = "[Số điện thoại]";
                            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_003;
                            AddHistory(hisVm);

                            return await SendMessage(handlePhone.TemplateJsonZalo, sender);
                        }
                        if (text.Contains(CommonConstants.ModuleEmail))
                        {
                            var handleEmail = _handleMdService.HandledIsEmail(text, botId);
                            zlUserDb.IsHavePredicate = true;
                            zlUserDb.PredicateName = "Email";
                            _appZaloUser.Update(zlUserDb);
                            _appZaloUser.Save();

                            hisVm.UserSay = "[Email]";
                            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_003;
                            AddHistory(hisVm);

                            return await SendMessage(handleEmail.TemplateJsonZalo, sender);
                        }

                        if (text.Contains(CommonConstants.ModuleVoucher))
                        {
                            string mdVoucherId = text.Replace(".", String.Empty).Replace("postback_module_voucher_", "");
                            var handleMdVoucher = _handleMdService.HandleIsVoucher(text, mdVoucherId,zlUserDb.EngineerName, hisVm.Type);

                            zlUserDb.IsHavePredicate = true;
                            zlUserDb.PredicateName = "Voucher";
                            zlUserDb.PredicateValue = mdVoucherId;
                            _appZaloUser.Update(zlUserDb);
                            _appZaloUser.Save();

                            hisVm.UserSay = "[Voucher]";
                            hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_003;
                            AddHistory(hisVm);

                            return await SendMessage(handleMdVoucher.TemplateJsonZalo, sender);
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
                                zlUserDb.PredicateName = "Age";
                                var handleAge = _handleMdService.HandledIsAge(contains, botId);
                                rsHandle = handleAge.TemplateJsonZalo;
                            }
                            if (contains == "postback_module_email")
                            {
                                zlUserDb.PredicateName = "Email";
                                var handleEmail = _handleMdService.HandledIsEmail(contains, botId);
                                rsHandle = handleEmail.TemplateJsonZalo;
                            }
                            if (contains == "postback_module_phone")
                            {
                                zlUserDb.PredicateName = "Phone";
                                var handlePhone = _handleMdService.HandleIsPhoneNumber(contains, botId);
                                rsHandle = handlePhone.TemplateJsonZalo;
                            }
                            zlUserDb.IsHavePredicate = true;
                            zlUserDb.PredicateValue = "";
                            _appZaloUser.Update(zlUserDb);
                            _appZaloUser.Save();

                            return await SendMessage(rsHandle, sender);
                        }
                    }
                }
                if (result.Contains("NOT_MATCH"))
                {
                    hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_002;
                    AddHistory(hisVm);

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
                        return await SendMessage(ZaloTemplate.GetMessageTemplateText("Tìm kiếm xử lý ngôn ngữ tự nhiên hiện không hoạt động, bạn vui lòng thử lại sau nhé!", sender));// not match
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
                    string resultAPI = GetRelatedQuestionToZalo(nameFunctionAPI, text, field, number, botId.ToString());
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
                        await SendMessageTask(ZaloTemplate.GetMessageTemplateText(totalFind, sender).ToString(), sender);
                        string strTemplateGenericRelatedQuestion = ZaloTemplate.GetMessageTemplateGenericByList(sender, lstQnaAPI).ToString();
                        return await SendMessage(strTemplateGenericRelatedQuestion, sender);
                    }
                    else
                    {
                        hisVm.BotHandle = MessageBot.BOT_HISTORY_HANDLE_008;
                        AddHistory(hisVm);
                        string strDefaultNotMatch = "Xin lỗi! Tôi không hiểu";
                        foreach (var item in _dicNotMatch)
                        {
                            string itemNotMatch = item.Key;
                            if (itemNotMatch.Contains(result.Trim().Replace(".", String.Empty)))
                            {
                                strDefaultNotMatch = item.Value;
                            }
                        }
                        return await SendMessage(ZaloTemplate.GetMessageTemplateText(strDefaultNotMatch, sender));// not match
                    }
                }
                // input là postback
                if (text.Contains("postback_card"))
                {
                    var cardDb = _cardService.GetSingleCondition(text.Replace(".", String.Empty));
                    string tempJsonZalo = cardDb.TemplateJsonZalo;
                    if (!String.IsNullOrEmpty(tempJsonZalo))
                    {
                        tempJsonZalo = tempJsonZalo.Trim();
                        string[] strArrayJson = Regex.Split(tempJsonZalo, "split");//nhớ thêm bên formcard xử lý lục trên face
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
                    string tempJsonZalo = cardDb.TemplateJsonZalo;
                    if (!String.IsNullOrEmpty(tempJsonZalo))
                    {
                        tempJsonZalo = tempJsonZalo.Trim();
                        string[] strArrayJson = Regex.Split(tempJsonZalo, "split");//nhớ thêm bên formcard xử lý lục trên face
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
                return await SendMessage(ZaloTemplate.GetMessageTemplateText(result, sender));

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
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                res = await client.PostAsync($"https://openapi.zalo.me/v2.0/oa/message?access_token=" + pageToken + "", new StringContent(json.ToString(), Encoding.UTF8, "application/json"));
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
                templateJson = Regex.Replace(templateJson, @"\\n\\r\\n", "\\n");
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    res = await client.PostAsync($"https://openapi.zalo.me/v2.0/oa/message?access_token=" + pageToken + "", new StringContent(templateJson, Encoding.UTF8, "application/json"));
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
                templateJson = Regex.Replace(templateJson, @"\\n\\r\\n", "\\n");
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage res = await client.PostAsync($"https://openapi.zalo.me/v2.0/oa/message?access_token=" + pageToken + "", new StringContent(templateJson, Encoding.UTF8, "application/json"));
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
        private string GetRelatedQuestionToZalo(string nameFuncAPI, string question, string field, string number, string botId)
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
