using BotProject.Common.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace BotProject.Service
{
    public interface IHandleModuleServiceService
    {
        HandleResultBotViewModel HandleIsPhoneNumber(string number, int botID);
        HandleResultBotViewModel HandledIsEmail(string email, int botID);
        HandleResultBotViewModel HandledIsAge(string age, int botID);
        HandleResultBotViewModel HandleIsName(string name, int botID);
        HandleResultBotViewModel HandleIsModuleKnowledgeInfoPatient(string mdName, int botID, string notFound);
        HandleResultBotViewModel HandleIsSearchAPI(string mdName, string mdSearchID, string notFound);
    }
    public class HandleModuleService : IHandleModuleServiceService
    {
        private const string CharacterPattern = @"^[A-Za-z]+";
        private const string NumberPattern = @"^\d+$";
        private const string PhonePattern = @"^(\+[0-9]{9})$";
        private const string EmailPattern =
        @"^\s*[\w\-\+_']+(\.[\w\-\+_']+)*\@[A-Za-z0-9]([\w\.-]*[A-Za-z0-9])?\.[A-Za-z][A-Za-z\.]*[A-Za-z]$";

        private IMdPhoneService _mdPhoneService;
        private IMdEmailService _mdEmailService;
        private IMdAgeService _mdAgeService;
        private IModuleKnowledegeService _mdKnowledegeService;
        private IMdSearchService _mdSearchService;

        public HandleModuleService(IMdPhoneService mdPhoneService,
                                    IMdEmailService mdEmailService,
                                    IMdAgeService mdAgeService,
                                    IModuleKnowledegeService mdKnowledegeService,
                                    IMdSearchService mdSearchService)
        {
            _mdPhoneService = mdPhoneService;
            _mdEmailService = mdEmailService;
            _mdAgeService = mdAgeService;
            _mdKnowledegeService = mdKnowledegeService;
            _mdSearchService = mdSearchService;
        }
        public HandleResultBotViewModel HandleIsPhoneNumber(string number, int botID)
        {
            HandleResultBotViewModel rsHandle = new HandleResultBotViewModel();
            var mdPhoneDb = _mdPhoneService.GetByBotID(botID);
            rsHandle.Postback = mdPhoneDb.Payload;
            if (number.Contains(Common.CommonConstants.ModulePhone))
            {
                rsHandle.Status = false;
                rsHandle.Message = tempText(mdPhoneDb.MessageStart);// sau này phát triển thêm random nhiều message, tạo aiml random li(thẻ error phone)
                return rsHandle;
            }
            bool isNumber = ValidatePhoneNumber(number, true);
            if (!isNumber)
            {
                rsHandle.Status = false;
                rsHandle.Message = tempText(mdPhoneDb.MessageError);
                return rsHandle;
            }
            rsHandle.Status = true;
            rsHandle.Message = tempText(mdPhoneDb.MessageEnd);
            return rsHandle;
        }

        public HandleResultBotViewModel HandledIsEmail(string email, int botID)
        {
            HandleResultBotViewModel rsHandle = new HandleResultBotViewModel();
            var mdEmailDb = _mdEmailService.GetByBotID(botID);
            rsHandle.Postback = mdEmailDb.Payload;

            rsHandle.Status = true;
            if (email.Contains(Common.CommonConstants.ModuleEmail))
            {
                rsHandle.Status = false;
                rsHandle.Message = tempText(mdEmailDb.MessageStart);
                return rsHandle;
            }
            bool isEmail = Regex.Match(email, EmailPattern).Success;
            if (!isEmail)
            {
                rsHandle.Status = false;
                rsHandle.Message = tempText(mdEmailDb.MessageError);
                return rsHandle;
            }
            rsHandle.Status = true;
            rsHandle.Message = tempText(mdEmailDb.MessageEnd);// nếu call tới follow thẻ khác trả về postback id card
            return rsHandle;
        }

        public HandleResultBotViewModel HandledIsAge(string age, int botID)
        {
            HandleResultBotViewModel rsHandle = new HandleResultBotViewModel();
            var mdAgeDb = _mdAgeService.GetByBotID(botID);

            rsHandle.Postback = mdAgeDb.Payload;
            rsHandle.Status = true;
            if (age.Contains(Common.CommonConstants.ModuleAge))
            {
                rsHandle.Status = false;
                rsHandle.Message = tempText(mdAgeDb.MessageStart);
                return rsHandle;
            }
            bool isAge = Regex.Match(age, NumberPattern).Success;
            if (!isAge)
            {
                rsHandle.Status = false;
                rsHandle.Message = tempText(mdAgeDb.MessageError);
                return rsHandle;
            }
            else
            {
                if (Int32.Parse(age) < 5)
                {
                    rsHandle.Status = false;
                    rsHandle.Message = tempText("Bạn còn quá nhỏ để tôi đưa ra tư vấn.");
                    return rsHandle;
                }
                if (Int32.Parse(age) > 110)
                {
                    rsHandle.Status = false;
                    rsHandle.Message = tempText("Xin lỗi tôi không thể đưa ra tư vấn hợp lý với độ tuổi này.");
                    return rsHandle;
                }
            }
            rsHandle.Status = true;
            rsHandle.Message = tempText(mdAgeDb.MessageEnd);// nếu call tới follow thẻ khác trả về postback id card
            return rsHandle;
        }

        public HandleResultBotViewModel HandleIsName(string name, int botID)
        {
            HandleResultBotViewModel rsHandle = new HandleResultBotViewModel();
            //rsHandle.Postback = postbackCard;
            if (name.Contains(Common.CommonConstants.ModuleName))
            {
                rsHandle.Status = false;
                rsHandle.Message = tempText("Bạn tên là gì?");// sau này phát triển thêm random nhiều message, tạo aiml random li(thẻ error phone)
                return rsHandle;
            }
            bool isName = Regex.Match(name, CharacterPattern).Success;
            if (!isName)
            {
                rsHandle.Status = false;
                rsHandle.Message = tempText("Hình như không giống tên cho lắm?");
                return rsHandle;
            }
            rsHandle.Status = true;
            rsHandle.Message = tempText("Cảm ơn bạn đã cho biết tên!");
            return rsHandle;
        }

        public HandleResultBotViewModel HandleIsModuleKnowledgeInfoPatient(string mdName, int botID, string notFound)
        {
            HandleResultBotViewModel rsHandle = new HandleResultBotViewModel();
            string mdInfoPatientID = mdName.Replace(".", String.Empty).Replace("postback_module_med_get_info_patient_", "");
            var mdGetInfoPatientDb = _mdKnowledegeService.GetByMdMedInfoPatientID(Int32.Parse(mdInfoPatientID));
            if (mdGetInfoPatientDb != null)
            {
                if (!String.IsNullOrEmpty(mdGetInfoPatientDb.OptionText))
                {
                    var arrOpt = mdGetInfoPatientDb.OptionText.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                    rsHandle.Message = TemplateOptionBot(arrOpt, mdGetInfoPatientDb.Title, mdGetInfoPatientDb.Payload, mdInfoPatientID, notFound);
                }
            }
            return rsHandle;
        }

        private static string TemplateOptionBot(string[] arrOpt, string title, string postback, string mdInfoPatientID, string notFound)
        {
            if (!String.IsNullOrEmpty(notFound))
            {
                title = notFound;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div class=\"_4xkn clearfix\">");
            sb.AppendLine("<div class=\"profilePictureColumn\" style=\"bottom: 0px;\">");
            sb.AppendLine("<div class=\"_4cqr\">");
            sb.AppendLine("<img class=\"profilePicture img\" src=\"{{image_logo}}\"/>");
            sb.AppendLine("<div class=\"clearfix\"></div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"messages\">");
            sb.AppendLine("<div class=\"_21c3\">");
            sb.AppendLine("<div class=\"clearfix _2a0-\">");
            sb.AppendLine("<div class=\"_4xko _4xkr\" tabindex=\"0\" role=\"button\" style=\"background-color: rgb(241, 240, 240);font-family: Segoe UI;\">");
            sb.AppendLine("<span>");
            sb.AppendLine("<span>" + title + "</span>");
            sb.AppendLine("</span>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"_4xko _4xkr\" tabindex=\"0\" role=\"button\" style=\"background-color: rgb(241, 240, 240);font-family: Segoe UI; width:100%\">");
            sb.AppendLine("<ul>");
            foreach (var item in arrOpt)
            {
                sb.AppendLine("<li><input type=\"checkbox\" value=\"" + item + "\" class=\"chk-opt-module-" + mdInfoPatientID + "\"/>" + item + "</li>");
            }
            sb.AppendLine("</ul>");
            sb.AppendLine("</div>");
            if (!String.IsNullOrEmpty(postback))
            {
                sb.AppendLine("<div class=\"_4xko _2k7w _4xkr\">");
                sb.AppendLine("<div class=\"_2k7x\">");
                sb.AppendLine("<div class=\"_6b7s\">");
                sb.AppendLine("<div class=\"_6ir5\">");
                sb.AppendLine("<div class=\"_4bqf _6ir3\">");
                sb.AppendLine("<a class=\"_6ir4 _6ir4_module\" data-id=\"" + mdInfoPatientID + "\" data-postback =\"module_patient_" + postback + "\" href=\"#\" style=\"color: {{color}}\">Tiếp tục</a>");
                sb.AppendLine("</div>");
                sb.AppendLine("</div>");
                sb.AppendLine("</div>");
                sb.AppendLine("</div>");
                sb.AppendLine("</div>");
            }
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");

            return sb.ToString();
        }

        public HandleResultBotViewModel HandleIsSearchAPI(string text, string mdSearchID, string notFound)
        {
            HandleResultBotViewModel rsHandle = new HandleResultBotViewModel();
            var mdSearchDb = _mdSearchService.GetByID(Int32.Parse(mdSearchID));
            try
            {
                // Đối với module tìm kiếm , tắt mở check cho thẻ tiếp theo, khi có message error hoặc không.
                if (!mdSearchDb.Equals(null))
                {
                    if (text.Contains(Common.CommonConstants.ModuleSearchAPI))
                    {
                        rsHandle.Status = false;
                        rsHandle.Message = tempText(mdSearchDb.MessageStart);
                    }
                    else
                    {
                        rsHandle.Status = false;
                        rsHandle.ResultAPI = GetModuleSearchAPI(text, mdSearchDb.ParamAPI, mdSearchDb.UrlAPI, mdSearchDb.KeyAPI, mdSearchDb.MethodeAPI);
                        if (String.IsNullOrEmpty(rsHandle.ResultAPI))
                        {
                            rsHandle.Message = tempText(mdSearchDb.MessageError);
                        }else
                        {
                            rsHandle.Message = "NOT_MATCH_01";
                        }
                        if (!String.IsNullOrEmpty(mdSearchDb.TitlePayload))
                        {
                            rsHandle.Postback = tempNodeBtnModule(mdSearchDb.Payload, mdSearchDb.TitlePayload);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return rsHandle;
        }

        /// <summary>
        /// Template text UI BOT
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string tempText(string text)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div class=\"_4xkn clearfix\">");
            sb.AppendLine("     <div class=\"profilePictureColumn\" style=\"bottom: 0px;\">");
            sb.AppendLine("         <div class=\"_4cqr\">");
            sb.AppendLine("             <img class=\"profilePicture img\" src=\"{{image_logo}}\" alt=\"\">");
            sb.AppendLine("             <div class=\"clearfix\"></div>");
            sb.AppendLine("         </div>");
            sb.AppendLine("     </div>");
            sb.AppendLine("     <div class=\"messages\">");
            sb.AppendLine("         <div class=\"_21c3\">");
            sb.AppendLine("             <div class=\"clearfix _2a0-\">");

            sb.AppendLine("<div class=\"_4xko _4xkr\" tabindex=\"0\" role=\"button\" style=\"background-color:rgb(241, 240, 240); font-family: Segoe UI\">");
            sb.AppendLine("     <span>");
            sb.AppendLine("         <span>" + text + "</span>");
            sb.AppendLine("     </span>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            return sb.ToString();
        }

        private static string tempNodeBtnModule(string payload, string titlePayload)
        {
            StringBuilder sbPostback = new StringBuilder();
            sbPostback.AppendLine("<div class=\"_6biu\">");
            sbPostback.AppendLine("                                                                    <div  class=\"_23n- form_carousel\">");
            sbPostback.AppendLine("                                                                        <div class=\"_4u-c\">");
            sbPostback.AppendLine("                                                                            <div index=\"0\" class=\"_a28 lst_btn_carousel\">");
            sbPostback.AppendLine("                                                                                <div class=\"_a2e\">");
            sbPostback.AppendLine(" <div class=\"_2zgz _2zgz_postback\">");
            sbPostback.AppendLine("      <div class=\"_4bqf _6biq _6bir\" tabindex=\"0\" role=\"button\" data-postback =\"" + payload + "\" style=\"border-color: {{color}} color: {{color}}\">" + titlePayload + "</div>");
            sbPostback.AppendLine(" </div>");
            sbPostback.AppendLine("                                                                                </div>");
            sbPostback.AppendLine("                                                                            </div>");
            sbPostback.AppendLine("                                                                            <div class=\"_4u-f\">");
            sbPostback.AppendLine("                                                                                <iframe aria-hidden=\"true\" class=\"_1_xb\" tabindex=\"-1\"></iframe>");
            sbPostback.AppendLine("                                                                            </div>");
            sbPostback.AppendLine("                                                                        </div>");
            return sbPostback.ToString();
        }

        private static bool ValidatePhoneNumber(string phone, bool IsRequired)
        {
            if (string.IsNullOrEmpty(phone) & !IsRequired)
                return true;

            if (string.IsNullOrEmpty(phone) & IsRequired)
                return false;

            var cleaned = RemoveNonNumeric(phone);
            if (IsRequired)
            {
                if (cleaned.Length == 10)
                    return true;
                else
                    return false;
            }
            else
            {
                if (cleaned.Length == 0)
                    return true;
                else if (cleaned.Length > 0 & cleaned.Length < 10)
                    return false;
                else if (cleaned.Length == 10)
                    return true;
                else
                    return false; // should never get here
            }
        }

        private static string RemoveNonNumeric(string phone)
        {
            return Regex.Replace(phone, @"[^0-9]+", "");
        }

        #region --DATA SOURCE API--
        private string apiRelateQA = "/api/get_related_pairs";
        private string ExcuteModuleSearchAPI(string NameFuncAPI, string param, string UrlAPI, string KeySecrectAPI, string Type = "Post")
        {
            string result = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlAPI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (!String.IsNullOrEmpty(KeySecrectAPI))
                {
                    string[] key = KeySecrectAPI.Split(':');
                    client.DefaultRequestHeaders.Add(key[0], key[1]);
                }
                HttpResponseMessage response = new HttpResponseMessage();
                param = Uri.UnescapeDataString(param);
                var dict = HttpUtility.ParseQueryString(param);
                string json = JsonConvert.SerializeObject(dict.Cast<string>().ToDictionary(k => k, v => dict[v]));

                StringContent httpContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
                try
                {
                    if (Type.ToUpper().Equals(Common.CommonConstants.MethodeHTTP_POST))
                    {
                        response = client.PostAsync(NameFuncAPI, httpContent).Result;
                    }
                    else if (Type.ToUpper().Equals(Common.CommonConstants.MethodeHTTP_GET))
                    {
                        string requestUri = NameFuncAPI + "?" + httpContent;
                        response = client.GetAsync(requestUri).Result;
                    }
                }
                catch (Exception ex)
                {
                    return String.Empty;
                }
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                }else
                {
                    result = String.Empty;
                }
            }
            return result;
        }
        private string GetModuleSearchAPI(string contentText, string param, string urlAPI, string keyAPI, string methodeHttp)
        {
            string responseString;
            if (String.IsNullOrEmpty(param))
            {
                param = "question=" + contentText + "&type=leg&number=10";
            }
            responseString = ExcuteModuleSearchAPI(apiRelateQA, param, urlAPI, keyAPI, methodeHttp);
            if (responseString != null)
            {
                var lstQues = new JavaScriptSerializer
                {
                    MaxJsonLength = Int32.MaxValue,
                    RecursionLimit = 100
                }
                .Deserialize<List<dynamic>>(responseString);
            }
            return responseString;
        }

        #endregion
    }
}
