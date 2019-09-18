using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BotProject.Service;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using BotProject.Model.Models;
using BotProject.Web.Infrastructure.Extensions;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Text;

namespace BotProject.Web.API
{
    public class ZaloWebhookController : ApiController
    {
        string pageToken = "OUQpPbeaQqbjuhvZLIjhJstikczICc8EDPBCEaCHQKStjV4gJ1fqPJEwnaeBPsSmOjhq7rL485CgnvKRSa97SYpW_bXRIMe04el3A2e9Rb9ca-C63ZKn9dcAbbeV0oDqMgos31yhEqLhhvfhD1HC45sWnKKR51GAUwEWHaS_0Xbvzhf94qu34LxQXKOVQI8gGwdxGpKILZjqrS1cAK5hAHA7oNvA3qzIVPpUA1quUtTTaDms2YflNsxgvNCMIZmDV8EVTZCa7ITJPWxmu1bSE657";
        string appSecret = Helper.ReadString("AppSecret");
        string verifytoken = Helper.ReadString("VerifyTokenWebHook");

        private IErrorService _errorService;
        public ZaloWebhookController(IErrorService errorService)
        {
            _errorService = errorService;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post()
        {
            var body = await Request.Content.ReadAsStringAsync();
            if (body.Contains("user_send_text"))
            {
                var value = JsonConvert.DeserializeObject<ZaloBotRequest>(body);
                LogError(body);
                await SendMessage(GetMessageTemplate("", value.sender.id));
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
           
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        private JObject GetMessageTemplate(string text, string sender)
        {
            return JObject.FromObject(
                new
                {
                    recipient = new { user_id = sender },
                    message = new {
                        attachment = new
                        {
                            type = "template",
                            payload = new
                            {
                                template_type = "list",
                                elements = new[]
                                {
                                    new
                                    {
                                        title = "Trung tâm chăm sóc khách hàng Digipro.vn",
                                        subtitle = "Zalo API cung cấp các công cụ để bạn có thể kết nối thanh chóng và hiệu quả",
                                        image_url = "https://bot.surelrn.vn/File/Images/Card/134a16f1-7c56-4eca-a61b-1bbe5a23a42b-Logo_DGP_EN_1600-800_5.png",
                                        default_action = new
                                        {
                                            type = "oa.open.url",
                                            payload = "abc",
                                            url = "https://digipro.vn/"
                                        }
                                    },
                                    new
                                    {
                                        title = "💻 Bảo hành dòng máy Dell",
                                        subtitle = "Bảo hành dòng máy Dell",
                                        image_url =  "https://developers.zalo.me/web/static/zalo.png",
                                        default_action = new
                                        {
                                            type = "oa.query.hide",
                                            payload = "abc878",
                                            url = "https://digipro.vn/"
                                        }
                                    },
                                    new
                                    {
                                        title = "🔍 Tra cứu máy bảo hành",
                                        subtitle = "Tra cứu máy bảo hành",
                                        image_url =  "https://developers.zalo.me/web/static/zalo.png",
                                        default_action = new
                                        {
                                            type = "oa.query.hide",
                                            payload = "abc123",
                                            url = "https://digipro.vn/"
                                        }
                                    },
                                    new
                                    {
                                        title = "📞 Thông tin hỗ trợ",
                                        subtitle = "Thông tin hỗ trợ",
                                        image_url =  "https://developers.zalo.me/web/static/zalo.png",
                                        default_action = new
                                        {
                                            type = "oa.query.hide",
                                            payload = "abc12543",
                                            url = "https://digipro.vn/"
                                        }
                                    }
                                }
                            }
                        }
                    },
                });
        }
        private async Task SendMessage(JObject json)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.PostAsync($"https://openapi.zalo.me/v2.0/oa/message?access_token="+ pageToken + "", new StringContent(json.ToString(), Encoding.UTF8, "application/json"));
            }
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
    }
}
