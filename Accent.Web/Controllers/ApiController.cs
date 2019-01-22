using AIMLbot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using SearchEngine.Data;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using SearchEngine.Service;
using System.Xml;
using Accent.Web.Infrastructure.Core;

namespace Accent.Web.Controllers
{
    public class ApiController : Controller
    {
        private AccentService _accent;
        private readonly string UrlAPI = Helper.ReadString("UrlAPI");
        private readonly string KeyAPI = Helper.ReadString("KeyAPI");
        private Bot bot;
        private User user;
        private string pathAIML = HostingEnvironment.MapPath("~/Datasets_BOT/aiml_legal");
        Dictionary<string, string> NOT_MATCH;

        private ElasticSearch _elastic;

        public ApiController() : base(){

            _elastic = new ElasticSearch();

            _accent = AccentService.AccentInstance;

            bot = new Bot();
            string userName = "user" + Guid.NewGuid();
            user = new User(userName, bot);
            string pathSetting = HostingEnvironment.MapPath("~/Datasets_BOT/config/Settings.xml");
            bot.loadSettings(pathSetting);
            bot.loadAIMLFromFiles(pathAIML);

            bot.isAcceptingUserInput = false;
            bot.isAcceptingUserInput = true;

            NOT_MATCH = new Dictionary<string, string>();
            NOT_MATCH.Add("NOT_MATCH_01", "Xin lỗi, Tôi không hiểu");
            NOT_MATCH.Add("NOT_MATCH_02", "Bạn có thể giải thích thêm được không?");
            NOT_MATCH.Add("NOT_MATCH_03", "Tôi không thể tìm thấy, bạn có thể nói rõ hơn?");
            NOT_MATCH.Add("NOT_MATCH_04", "Xin lỗi, Bạn có thể giải thích thêm được không?");
            NOT_MATCH.Add("NOT_MATCH_05", "Tôi không thể tìm thấy");
            NOT_MATCH.Add("NOT_MATCH_06", "Tôi chưa hiểu");
        }
        public ActionResult Index()
        {
            return View();
        }

        #region --VietName Accent Prediction--

        /// <summary>
        /// Chuyển đổi Tiếng Việt không dấu tới có dấu.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public JsonResult ConvertVN(string text)
        {
            string textVN = "";
            if (!String.IsNullOrEmpty(text))
                text = Regex.Replace(HttpUtility.HtmlDecode(text), @"<(.|\n)*?>", "");
            try
            {
                textVN = _accent.GetAccentVN(text);
            }
            catch (Exception ex)
            {
                string message = "ERROR_400";
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
                string textVN = _accent.GetAccentVN(text);
                string arrTextVN = _accent.GetMultiMatchesAccentVN(text, 5);

                rs.Item = textVN;
                rs.ArrItems = arrTextVN.Split(',').Distinct().Skip(1).ToArray();

            }
            catch(Exception ex)
            {
                string message = "ERROR_400";
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
                string arrTextVN = _accent.GetMultiMatchesAccentVN(text, 20);
                ArrItems = arrTextVN.Split(',').Distinct().ToArray();

            }
            catch (Exception ex)
            {
                string message = "ERROR_400";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            return Json(ArrItems, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region --CHATBOT--
        public JsonResult chatbot(string text,string group)
        {
            string result = "";
            AIMLbot.Request r = new Request(text, user, bot);
            AIMLbot.Result res = bot.Chat(r);
            result = res.OutputSentences[0].ToString();
            if (result.Contains("NOT_MATCH"))
            {
                result = CauHoiLienQuan(text, group);
                if (String.IsNullOrEmpty(result))
                {
                    result = NOT_MATCH[res.OutputSentences[0]];
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public string LoadAIML()
        {
            string fileIn = HostingEnvironment.MapPath("~/Datasets_BOT/aiml_legal/legal.aiml");
            var file = new FileInfo(fileIn);
            var content = System.IO.File.ReadAllText(file.FullName, Encoding.UTF8);

            return content;
        }

        [ValidateInput(false)]
        [HttpPost]
        public string SaveAIML()//string folderAIML, string nameAIML
        {
            string textAIML = "";
            string message = "";
            try
            {
                var contentAIML = System.Web.HttpContext.Current.Request.Unvalidated.Form["formAIML"];
                if (contentAIML != null)
                {
                    var content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 }.Deserialize<string>(contentAIML);
                    textAIML = HttpUtility.HtmlDecode(content);

                }
                string pathAIML = HostingEnvironment.MapPath("~/Datasets_BOT/aiml_legal");
                System.IO.File.WriteAllText(Path.Combine(pathAIML, "legal.aiml"), textAIML);
                message = "success";
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return message;
            }

            Thread.Sleep(1000);
            bot.loadAIMLFromFiles(pathAIML);

            return message;
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
        public string CauHoiLienQuan(string QuestionContent, string GroupQues = "leg")
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

        #region --SEARCH ENGINE ELASTICSEARCH--
        public JsonResult GetAll(int page = 0, int pageSize = 10)
        {
            int totalRow = 0;
            int from = page * pageSize;

            var lstData = _elastic.GetAll(from, pageSize);

            if(lstData.Count() != 0)
            {
                totalRow = lstData[0].Total;
            }

            var paginationSet = new PaginationSet<Question>()
            {
                Items = lstData,
                Page = page,
                TotalCount = totalRow,
                TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
            };

            return Json(paginationSet, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }

    public class Result
    {
        public string Item { get; set; }
        public string[] ArrItems { get; set; }
    }
   

}