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

        private string apiAddQA = "/api/qa_for_all/pair_add";
        private string apiUpdateQA = "/api/qa_for_all/pair_update";
        private string apiDetailQA = "/api/qa_for_all/pair_get";
        private string apiRelateQA = "/api/qa_for_all/get_related_pairs";

        /// <summary>
        /// API KNOWLEDGE BASE CHATBOT
        /// </summary>
        private string apiKnowledgeBaseAddQA = "/api/chatbot/pair_add";
        private string apiKnowledgeBaseDeleteQA = "/api/chatbot/bot_delete_formqnaid";
        private string apiKnowledgeBasePrecidictTextClass = "/api/chatbot/text_class";

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
                            string requestUri = NameFuncAPI;
                            response = client.GetAsync(requestUri).Result;
                            break;
                        case "Delete":
                            string requestUriDelete = NameFuncAPI;
                            response = client.DeleteAsync(requestUriDelete).Result;
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

        public string AddQues(string QuesID, string QuestionContent, string AnswerContent, string AreaId,string QuestionHtml, string AnswerHtml,string BotId)
        {
            var param = new { id = QuesID, question = QuestionContent, answer = AnswerContent, field = AreaId, botid = BotId, question_html = QuestionHtml , answer_html = AnswerHtml };
            return ApiAddUpdateQA(apiAddQA, param, "Post");
        }
        public string UpdateQues(string QuesID, string QuestionContent, string AnswerContent, string AreaId, string QuestionHtml, string AnswerHtml, string BotId)
        {
            var param = new { id = QuesID, question = QuestionContent, answer = AnswerContent, field = AreaId, botid = BotId, question_html = QuestionHtml, answer_html = AnswerHtml };
            return ApiAddUpdateQA(apiUpdateQA, param, "Put");
        }

        // Function ADD KNOWLEDGE BASE - TEXT CLASSIFICATION
        public string AddKnowledgeQuestion(int botId, int formQnaId, int quesId, string question, string target)
        {
            var param = new {
                botid = botId,
                formQnAid = formQnaId,
                id = quesId,
                feature = question,
                target = target,
            };
            return ApiAddUpdateQA(apiKnowledgeBaseAddQA, param, "Post");
        }
        public string DeleteAllKnowledgeByFormId(int formQnaId)
        {
            string urlDelete = apiKnowledgeBaseDeleteQA + "?formQnAid=" + formQnaId;
            return ApiAddUpdateQA(urlDelete, null, "Delete");
        }
        public string GetPrecidictTextClass(string text, int botId)
        {
            var param = new
            {
                input = text,
                botid = botId
            };
            return ApiAddUpdateQA(apiKnowledgeBaseDeleteQA, param, "Post");
        }
    }
}