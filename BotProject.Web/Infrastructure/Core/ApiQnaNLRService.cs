using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace BotProject.Web.Infrastructure.Core
{
    public class ApiQnaNLRService
    {
        private readonly string UrlAPI = Helper.ReadString("UrlAPI");
        private readonly string KeyAPI = Helper.ReadString("KeyAPI");

        private string apiAddQA = "/api/pair_add";
        private string apiUpdateQA = "/api/pair_update";
        private string apiDetailQA = "/api/pair_get";
        private string apiRelateQA = "/api/get_related_pairs";


        /// <summary>
        /// Gọi api chung
        /// </summary>
        /// <param name="NameFuncAPI"></param>
        /// <param name="T"></param>
        /// <param name="Type">
        ///     Add, Update, Delete, Get
        /// </param>
        /// <returns></returns>
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
                        case "Put":
                            response = client.PutAsJsonAsync(NameFuncAPI, httpContent).Result;
                            break;
                        case "Get":
                            string requestUri = NameFuncAPI + "?" + httpContent;
                            response = client.GetAsync(requestUri).Result;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    //LMSLog.Error("ApiAddUpdateQA exception. Error message:\r\n", ex);
                    return null;
                }
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                }
            }
            return result;
        }
        public string AddQues(string QuesID, string QuestionContent, string AnswerContent, string AreaTitle, string AnswerHtml, string GroupQues = "leg")
        {
            var param = new { id = QuesID, question = QuestionContent, answer = AnswerContent, field = AreaTitle, html = AnswerHtml, type = GroupQues };
            return ApiAddUpdateQA(apiAddQA, param, "Post");
        }
        public string UpdateQues(string QuesID, string QuestionContent, string AnswerContent, string AreaTitle, string AnswerHtml, string GroupQues = "leg")
        {
            var param = new { id = QuesID, question = QuestionContent, answer = AnswerContent, field = AreaTitle, html = AnswerHtml, type = GroupQues };
            return ApiAddUpdateQA(apiUpdateQA, param, "Put");
        }   
    }
}