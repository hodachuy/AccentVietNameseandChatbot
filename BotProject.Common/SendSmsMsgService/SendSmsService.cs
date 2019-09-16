using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BotProject.Common.SendSmsMsgService
{
    public class SendSmsService
    {
        private SendSmsMsgServiceSoapClient _sm;
        public SendSmsService()
        {
            _sm = new SendSmsMsgServiceSoapClient();
        }
        public string SendSmsMsg(string strPhoneNumber, string strMessage)
        {
            string xmlParam = GenXmlParam("84", strPhoneNumber, strMessage);//"Your Digipro verification code is: 58134"
            string rsMsg = _sm.ExecuteFunc("SendSmsMsg", xmlParam);
            return ConvertXMLToJson(rsMsg);
        }

        public static string GenXmlParam(string strCountryCode, string strPhoneNumber, string strMessage)
        {
            try
            {
                string strAccessCode = "SureHis";
                string strSecretKey = "Lv@HospitalMngt";
                string strSecureExtension = "xJBrDX";

                string strRandom = "316566353"; // Gia tri nay neu thay doi moi lan goi function thi tot, con khong thi gan co dinh cung duoc
                string strFunctionName = "SendSmsMsg"; // Ten ham

                string strXmlParam = (new SecureInfo()).GenXmlParam(strFunctionName,
                        new string[] { "CountryCode", "PhoneNumber", "Message" },
                        new string[] { strCountryCode, strPhoneNumber, strMessage },
                        strAccessCode,
                        strSecretKey,
                        strRandom,
                        strSecureExtension
                       );
                return strXmlParam;
            }
            catch (Exception ex) { }
            return "";
        }
        public static string ConvertXMLToJson(string xml)
        {
            if (xml != "" && xml != null)
            {

                XmlDocument _doc = new XmlDocument();
                _doc.LoadXml(xml);
                //return JsonConvert.SerializeXmlNode(doc);
                string _returnJson;
                var _rows = _doc.SelectNodes("//Table");
                if (_rows.Count == 1)
                {
                    var contentNode = _doc.SelectSingleNode("//NewDataSet");
                    contentNode.AppendChild(_doc.CreateNode("element", "Table", ""));
                    //contentNode.AppendChild(doc.CreateNode("element", "Total", ""));

                    // Convert to JSON and replace the empty element we created but keep the array declaration
                    _returnJson = JsonConvert.SerializeXmlNode(_doc, Newtonsoft.Json.Formatting.None, true).Replace(",null]", "]");
                }
                else
                {
                    // Convert to JSON
                    _returnJson = JsonConvert.SerializeXmlNode(_doc, Newtonsoft.Json.Formatting.None, true);
                }
                return _returnJson;
                //return JsonConvert.SerializeObject(new { data = returnJson, total = rows[0]["Total"].ToString()});
            }
            {
                object _table = null;
                return JsonConvert.SerializeObject(new { Table = _table });
            }
        }
    }
}
