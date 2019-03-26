using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BotProject.Web.Controllers
{
    public class Apiv1Controller : Controller
    {
        private readonly string UrlAPI = Helper.ReadString("UrlAPI");
        private readonly string KeyAPI = Helper.ReadString("KeyAPI");
        private string pathAIML = HostingEnvironment.MapPath("~/File/AIML/");
        private AccentService _accentService;
        private BotService _botService;

        public Apiv1Controller()
        {
            _accentService = AccentService.AccentInstance;
            _botService = BotService.BotInstance;
        }
        // GET: Apiv1
        public ActionResult Index()
        {
            return View();
        }


        #region CHATBOT
        public ActionResult FormChat()
        {
            string token = "4d1d77aa-42a7-4e88-97a6-baed104c2e60";
            string botId = "2014";
            //string token, string botId
            string nameBotAIML = "User_" + token + "_BotID_" + botId;
            string fullPathAIML = pathAIML + nameBotAIML;
            _botService.loadAIMLFromFiles(fullPathAIML);
            return View();
        }

        public JsonResult chatbot(string text, string group, string color, string logo)
        {
            string result = "";
            AIMLbot.Result res = _botService.Chat(text, color, logo);
            bool isMatch = true;

            result = res.OutputSentences[0].ToString();
            if (result.Contains("NOT_MATCH"))
            {
                isMatch = false;
                result = GetRelatedQuestion(text, group);
                if (String.IsNullOrEmpty(result))
                {
                    //result = NOT_MATCH[res.OutputSentences[0]];
                    result = res.OutputSentences[0].ToString();
                }
            }
            return Json(new
            {
                message = res.OutputHtmlMessage,
                postback = res.OutputHtmlPostback,
                messageai = result,
                isCheck = isMatch
            },
                            JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ACCENT VN
        public JsonResult ConvertVN(string text)
        {
            string textVN = "";
            if (!String.IsNullOrEmpty(text))
                text = Regex.Replace(HttpUtility.HtmlDecode(text), @"<(.|\n)*?>", "");
            try
            {
                textVN = _accentService.GetAccentVN(text);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            return Json(textVN, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAccentVN(string text)
        {
            Result rs = new Result();
            if (!String.IsNullOrEmpty(text))
                text = Regex.Replace(HttpUtility.HtmlDecode(text), @"<(.|\n)*?>", "");
            try
            {
                string textVN = _accentService.GetAccentVN(text);
                string arrTextVN = _accentService.GetMultiMatchesAccentVN(text, 5);

                rs.Item = textVN;
                rs.ArrItems = arrTextVN.Split(',').Distinct().Skip(1).ToArray();

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMultiMatchesAccentVN(string text)
        {
            string[] ArrItems;
            if (!String.IsNullOrEmpty(text))
                text = Regex.Replace(HttpUtility.HtmlDecode(text), @"<(.|\n)*?>", "");
            try
            {
                string arrTextVN = _accentService.GetMultiMatchesAccentVN(text, 20);
                ArrItems = arrTextVN.Split(',').Distinct().ToArray();

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            return Json(ArrItems, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region --DATA SOURCE API--

        private string apiRelateQA = "/api/get_related_pairs";

        protected string ApiAddUpdateQA(string NameFuncAPI, object T, string Type = "Post")
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
        public string GetRelatedQuestion(string QuestionContent, string GroupQues = "leg")
        {
            var param = new
            {
                question = QuestionContent,
                type = GroupQues,
                number = "10"
            };
            string responseString = ApiAddUpdateQA(apiRelateQA, param, "Post");
            if (responseString != null)
            {
                var lstQues = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 }.Deserialize<List<dynamic>>(responseString);
            }
            return responseString;
        }

        #endregion
        public class Result
        {
            public string Item { get; set; }
            public string[] ArrItems { get; set; }
        }
    }
}