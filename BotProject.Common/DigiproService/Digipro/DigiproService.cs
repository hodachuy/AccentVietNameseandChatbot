using BotProject.Common.DigiproService.Digipro.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace BotProject.Common.DigiproService.Digipro
{
    public class DigiproService
    {
        private static string UrlDigipro = ConfigHelper.ReadString("UrlDigipro");
        public static string GetDetailServiceDigiproByRofOrSvtag(string nameFunction, string rofNumberOrSvTag)
        {
            String URL = String.Format("{0}?id={1}&idsla=4",nameFunction, rofNumberOrSvTag);
            string jsonResult = GetAsync(URL).Result;
            if(jsonResult == "null")
            {
                return null;
            }

            var rsDgpModel = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 }.
                                                    Deserialize<DigiproServiceModel>(jsonResult);

            string dateReceive = rsDgpModel.datereceive.HasValue == true ? rsDgpModel.datereceive.Value.ToString("dd/MM/yyyy") : "";
            string dateTest = rsDgpModel.datetest.HasValue == true ? rsDgpModel.datetest.Value.ToString("dd/MM/yyyy") : "";
            string dateEta = rsDgpModel.dateeta.HasValue == true ? rsDgpModel.dateeta.Value.ToString("dd/MM/yyyy") : "";
            string dateComplete = rsDgpModel.datecomplete.HasValue == true ? rsDgpModel.datecomplete.Value.ToString("dd/MM/yyyy") : "";
            string dateClose = rsDgpModel.dateclose.HasValue == true ? rsDgpModel.dateclose.Value.ToString("dd/MM/yyyy") : "";
            string rs = GenDataServiceDigiproToString(rsDgpModel.customername,
                                                    rsDgpModel.phonenumber,
                                                    rsDgpModel.email,
                                                    rsDgpModel.servicetag,
                                                    rsDgpModel.rofnumber.ToString(),
                                                    dateReceive,
                                                    dateTest,
                                                    dateEta,
                                                    dateComplete,
                                                    dateClose);
            return rs;
        }

        private static async Task<String> GetAsync(String url)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(UrlDigipro);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage httpResponseMessage = httpClient.GetAsync(url).Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return await httpResponseMessage.Content.ReadAsStringAsync();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                //Logger.SlackApplication.PutExceptionMessage(exception, Characters.SlackWebhookURL.DGP_API_SentSMS);
                return null;
            }
        }

        private static string GenDataServiceDigiproToString(string customerName,
                                                            string phoneNumber,
                                                            string email,
                                                            string serviceTag,
                                                            string rofNumber,
                                                            string dateReveive,
                                                            string dateTest,
                                                            string dateeta,
                                                            string dateComplete,
                                                            string dateClose)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Khách hàng: " + customerName + "<br/>");
            sb.AppendLine("Điện thoại: " + phoneNumber + "<br/>");
            sb.AppendLine("Email: " + email + "<br/>");
            sb.AppendLine("ServiceTag: " + serviceTag + "<br/>");
            sb.AppendLine("Số phiếu: " + rofNumber + "<br/>");
            sb.AppendLine("Ngày nhận máy: " + dateReveive + "<br/>");
            sb.AppendLine("Ngày test: " + dateTest + "<br/>");
            sb.AppendLine("Ngày nhận link kiện: " + dateeta + "<br/>");
            sb.AppendLine("Ngày hoàn tất: " + dateComplete + "<br/>");
            sb.AppendLine("Ngày trả máy: " + dateClose + "<br/>");
            return sb.ToString();
        }
    }
}
