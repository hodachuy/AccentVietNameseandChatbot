using BotProject.Service;
using BotProject.Web.Infrastructure.Core;
using Newtonsoft.Json;
using SearchEngine.Data;
using SearchEngine.Service;
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
		private ElasticSearch _elastic;
        private IBotService _botDbService;

		public Apiv1Controller(IBotService botDbService)
        {
			_elastic = new ElasticSearch();
			_accentService = AccentService.AccentInstance;
            _botDbService = botDbService;
            _botService = BotService.BotInstance;
        }
        // GET: Apiv1
        public ActionResult Index()
        {
            return View();
        }


        #region CHATBOT
        public ActionResult FormChat(string token, string botId)
        {

            int id = Int32.Parse(botId);
            var botDb = _botDbService.GetByID(id);
            string nameBotAIML = "User_" + token + "_BotID_" + botId;
            string fullPathAIML = pathAIML + nameBotAIML;
            _botService.loadAIMLFromFiles(fullPathAIML);
            return View(botDb);
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
            },JsonRequestBehavior.AllowGet);
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

		#region --SEARCH ENGINE ELASTICSEARCH--
		public JsonResult GetAll(int page = 1, int pageSize = 10)
		{
			int totalRow = 0;
			int from = (page - 1) * pageSize;

			var lstData = _elastic.GetAll(from, pageSize);

			if (lstData.Count() != 0)
			{
				totalRow = lstData[0].Total;
			}

			var paginationSet = new PaginationSet<Question>()
			{
				Items = lstData,
				Page = page,
				TotalCount = totalRow,
				MaxPage = pageSize,
				TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
			};

			return Json(paginationSet, JsonRequestBehavior.AllowGet);
		}

		public JsonResult Search(string text, bool isAccentVN = false)
		{
			if (!String.IsNullOrEmpty(text))
				text = Regex.Replace(HttpUtility.HtmlDecode(text), @"<(.|\n)*?>", "");

			if (isAccentVN)
			{
				text = _accentService.GetAccentVN(text);
			}

			var lstData = _elastic.Search(text);

			var paginationSet = new PaginationSet<Question>()
			{
				Items = lstData,
				Page = 1,
				TotalCount = 1,
				TotalPages = 1
			};

			return Json(paginationSet, JsonRequestBehavior.AllowGet);
		}

		public JsonResult Suggest(string text, bool isAccentVN = false)
		{
			if (!String.IsNullOrEmpty(text))
				text = Regex.Replace(HttpUtility.HtmlDecode(text), @"<(.|\n)*?>", "");

			if (isAccentVN)
			{
				text = _accentService.GetAccentVN(text);
			}

			var lstSuggest = _elastic.AutoComplete(text);
			return Json(lstSuggest, JsonRequestBehavior.AllowGet);
		}

		public JsonResult AddQnA(string question, string answer)
		{
			string message = "";
			if (!String.IsNullOrEmpty(question))
			{
				return Json(message, JsonRequestBehavior.AllowGet);
			}
			var result = _elastic.Create(question, answer);
			return Json(result, JsonRequestBehavior.AllowGet);
		}

		//public JsonResult ImportExcelQnA()
		//{
		//	if (Request.Files.Count > 0)
		//		try
		//		{
		//			HttpPostedFileBase file = Request.Files[0];
		//			string pathtempt = Helper.ReadString("ExcelTemplateTemptPath1");
		//			if (!Directory.Exists(pathtempt))
		//			{
		//				Directory.CreateDirectory(pathtempt);
		//			}
		//			string path = System.IO.Path.Combine(pathtempt, file.FileName + "_" + DateTime.Now.Ticks.ToString());
		//			file.SaveAs(path);
		//			DataTable dt = ReadExcelFileToDataTable(path);
		//			return ReadFileExcelQnA(dt);
		//		}
		//		catch (Exception ex)
		//		{
		//			return Json("ERROR:" + ex.Message.ToString());
		//		}
		//	else
		//	{
		//		return Json("Bạn chưa chọn file để tải lên.");
		//	}
		//}

		//private DataTable ReadExcelFileToDataTable(string path)
		//{
		//	string POCpath = @"" + path;
		//	POCpath = POCpath.Replace("\\\\", "\\");
		//	IExcelDataReader dataReader;
		//	FileStream fileStream = new FileStream(POCpath, FileMode.Open);
		//	if (path.EndsWith(".xls"))
		//	{
		//		dataReader = ExcelReaderFactory.CreateBinaryReader(fileStream);
		//	}
		//	else
		//	{
		//		dataReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
		//	}
		//	DataSet result = dataReader.AsDataSet();

		//	DataTable dt = result.Tables[0];
		//	return dt;
		//}

		//public JsonResult ReadFileExcelQnA(DataTable dt)
		//{
		//	try
		//	{
		//		if (dt.Rows.Count > 0)
		//		{
		//			var quesList = new List<QuestionViewModel>();
		//			for (var i = 1; i < dt.Rows.Count; i++)
		//			{
		//				var question = new QuestionViewModel();
		//				var item = dt.Rows[i];
		//				question.Id = i;
		//				question.Score = 1;
		//				question.CreationDate = DateTime.Now;
		//				question.Body = item[1] == null ? "" : item[1].ToString();
		//				string mess = "";
		//				bool flag = false;

		//				if (string.IsNullOrEmpty(question.Body))
		//				{
		//					mess += "Câu hỏi không được để trống";
		//					flag = true;
		//				}
		//				if (flag)
		//				{
		//					return Json("File Excel có lỗi ở dòng thứ " + (i + 1) + ":<br/>" + mess);
		//				}

		//				quesList.Add(question);
		//				//CreateQuesForImport("", ques.AreaTitle, ques.ContentsText, ques.AnsContents, null, null, null, null
		//				//, null, null, null);
		//			}
		//			return Json(new { listques = quesList, status = "Thêm dữ liệu thành công!" });
		//		}
		//		else
		//		{
		//			return Json("File không có dữ liệu");
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		return Json("Lỗi: " + ex.Message.ToString());
		//	}
		//}
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