using BotProject.Common.DigiproService.Digipro.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace BotProject.Common.DigiproService.Digipro
{
    public static class DigiproService
    {
        private const string NumberPattern = @"^\d+$";

        private static string urlGetByServiceTag = "GetttingServicesByServicetag";
        private static string urlGetttingServicesByRofNumber = "GetttingServicesByRofNumber";

        private static string UrlDigipro = ConfigHelper.ReadString("UrlDigipro");
        public static string GetDetailServiceDigiproByRofOrSvtag(string nameFunction, string rofNumberOrSvTag)
        {
            string jsonResult = null;
            DigiproServiceModel rsDgpModel = new DigiproServiceModel();
            bool isRof = Regex.Match(rofNumberOrSvTag, NumberPattern).Success;
            if (isRof)
            {                
                String urlGetByRof4 = String.Format("{0}?id={1}&idsla=4", urlGetttingServicesByRofNumber, rofNumberOrSvTag);
                jsonResult = GetAsync(urlGetByRof4).Result;
                if(jsonResult == "null")
                {
                    String urlGetByRof1 = String.Format("{0}?id={1}&idsla=1", urlGetttingServicesByRofNumber, rofNumberOrSvTag);
                    jsonResult = GetAsync(urlGetByRof1).Result;
                }
                if (jsonResult == "null")
                {
                    String urlGetByRof5 = String.Format("{0}?id={1}&idsla=5", urlGetttingServicesByRofNumber, rofNumberOrSvTag);
                    jsonResult = GetAsync(urlGetByRof5).Result;
                }

                if (jsonResult == "null")
                {
                    return null;
                }

                rsDgpModel = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 }.
                                                        Deserialize<DigiproServiceModel>(jsonResult);
            }
            else
            {
                List<DigiproServiceModel> lstDgp = new List<DigiproServiceModel>();

                // ưu tiên lấy theo 4
                String urlGetSTag4 = String.Format("{0}?id={1}&idsla=4", urlGetByServiceTag, rofNumberOrSvTag);
                string jsonResult4 = GetAsync(urlGetSTag4).Result;
                if(jsonResult4 != "null")
                {
                    rsDgpModel = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 }.
                                    Deserialize<DigiproServiceModel>(jsonResult4);
                    lstDgp.Add(rsDgpModel);
                }

                String urlGetSTag1 = String.Format("{0}?id={1}&idsla=1", urlGetByServiceTag, rofNumberOrSvTag);
                string jsonResult1 = GetAsync(urlGetSTag1).Result;
                if (jsonResult1 != "null")
                {
                    rsDgpModel = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 }.
                                    Deserialize<DigiproServiceModel>(jsonResult1);
                    lstDgp.Add(rsDgpModel);
                }

                String urlGetSTag5 = String.Format("{0}?id={1}&idsla=5", urlGetByServiceTag, rofNumberOrSvTag);
                string jsonResult5 = GetAsync(urlGetSTag5).Result;
                if (jsonResult5 != "null")
                {
                    rsDgpModel = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 }.
                                    Deserialize<DigiproServiceModel>(jsonResult5);
                    lstDgp.Add(rsDgpModel);
                }

                if(lstDgp.Count == 0)
                {
                    return null;
                }
                else
                {
                    var lstDgpModel = lstDgp.OrderByDescending(x => x.datereceive).ToList();
                    rsDgpModel = lstDgpModel[0];
                }
            }

            string type = "";
            if(rsDgpModel.id_sla == 1)
            {
                type = "Bảo hành tại Trung tâm (CIS):";
            }
            else if (rsDgpModel.id_sla == 4)
            {
                type = "Sửa chữa (OOW):";
            }
            else if (rsDgpModel.id_sla == 5)
            {
                type = "Bảo hành lưu động (SOW):";
            }
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
                                                    dateClose,
                                                    type,
                                                    isRof);
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
                                                            string dateClose,
                                                            string type,
                                                            bool isRof)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Máy có Service tag "+ serviceTag + " được nhận " + type + "<br/><br/>");
            sb.AppendLine("- Tình trạng: Máy đã trả vào ngày " + dateClose + "<br/>");
            sb.AppendLine("- Tên khách hàng: " + customerName.ToUpper() + "<br/>");
            sb.AppendLine("- Điện thoại: " + ReplaceAt(phoneNumber,6, 4, "****") + "<br/>");
            sb.AppendLine("- Email: " + ReplaceEmail(email) + "<br/><br/>");
            sb.AppendLine("Thông tin chi tiết:<br/>");
            if (isRof)
            {
                sb.AppendLine("+ Service tag: " + serviceTag + "<br/>");
            }else
            {
                sb.AppendLine("+ Số phiếu: " + rofNumber + "<br/>");
            }

            sb.AppendLine("+ Ngày nhận máy: " + dateReveive + "<br/>");
            sb.AppendLine("+ Ngày kiểm tra: " + dateTest + "<br/>");

            if (!String.IsNullOrEmpty(dateeta) && dateeta != "01/01/0001")
            {
                sb.AppendLine("+ Ngày nhận linh kiện: " + dateeta + "<br/>");
            }

            sb.AppendLine("+ Ngày hoàn tất: " + dateComplete + "<br/>");
            sb.AppendLine("+ Ngày trả máy: " + dateClose + "<br/>");

            return sb.ToString();
        }
        private static string ReplaceAt(this string str, int index, int length, string replace)
        {
            return str.Remove(index, Math.Min(length, str.Length - index))
                    .Insert(index, replace);
        }

        private static string ReplaceEmail(this string str)
        {
            string mailName = "";
            string mailDomain = "";
            Regex rName = new Regex("([^@]+)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            Match _partName = rName.Match(str);
            if (_partName.Success)
            {
                mailName = _partName.Groups[0].Value;
                string star = "*";
                if((mailName.Length - 5) > 0)
                {
                    for (int i = 0; i < (mailName.Length - 5); i++)
                    {
                        star = star + "*";
                    }
                }
                mailName = mailName.Substring(0, 4) + star;
            }
            Regex rDomain = new Regex("@(.*)$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            Match _partDomain = rDomain.Match(str);
            if (_partDomain.Success)
            {
                mailDomain = _partDomain.Groups[0].Value;
            }

            return mailName + mailDomain;


        }
    }
}
