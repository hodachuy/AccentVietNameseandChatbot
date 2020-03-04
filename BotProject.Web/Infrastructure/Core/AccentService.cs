using Accent.Utils;
using BotProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace BotProject.Web
{
    public class AccentService
    {

        AccentPredictor accent;
        //private static AccentService accentInstance = null;
        //private static readonly object lockObject = new object();
        public AccentService()
        {
            //string _path1Gram = HostingEnvironment.MapPath("~/File/Datasets_Training_Accent/news1gram");
            //string _path2Gram = HostingEnvironment.MapPath("~/File/Datasets_Training_Accent/news2grams");
            //accent = new AccentPredictor();
            //string _path1Gram = PathServer.PathAccent + "news1gram";
            //string _path2Gram = PathServer.PathAccent + "news2grams";
            //string _path1Statistic = PathServer.PathAccent + "_1Statistic";
            //accent.InitNgram(_path1Gram, _path2Gram, _path1Statistic);
        }
        //public static AccentService AccentInstance
        //{
        //    get
        //    {
        //        if (accentInstance == null)
        //        {
        //            lock (lockObject)
        //            {
        //                if (accentInstance == null)
        //                {
        //                    accentInstance = new AccentService();
        //                }

        //            }
        //        }
        //        return accentInstance;
        //    }
        //}
        public string GetAccentVN(string text)
        {
            string result = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://bot.digipro.vn/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = new HttpResponseMessage();
                try
                {
                    string requestUri = "apiv1/GetAccentVN?text="+text;
                    response = client.GetAsync(requestUri).Result;
                }
                catch (Exception ex)
                {
                    return result;
                }
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    var resultAccent = new JavaScriptSerializer
                    {
                        MaxJsonLength = Int32.MaxValue,
                        RecursionLimit = 100
                    }.Deserialize<ReponseAccent>(result);
                    return resultAccent.Item;
                }
            }
            return result;


            //return accent.predictAccents(text);
        }
        public string GetMultiMatchesAccentVN(string text, int nResults)
        {
            string result = "";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://bot.digipro.vn/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = new HttpResponseMessage();
                try
                {
                    string requestUri = "apiv1/GetAccentVN?text=" + text;
                    response = client.GetAsync(requestUri).Result;
                }
                catch (Exception ex)
                {
                    return result;
                }
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    var resultAccent = new JavaScriptSerializer
                    {
                        MaxJsonLength = Int32.MaxValue,
                        RecursionLimit = 100
                    }.Deserialize<ReponseAccent>(result);
                    return string.Join(",", resultAccent.ArrItems);
                }
            }
            return result;
            //return accent.predictAccentsWithMultiMatches(text, nResults, false);
        }

        //Tăng performance tạm thời gọi bên web digipro
        public class ReponseAccent
        {
            public string Item { set; get; }
            public List<string> ArrItems { set; get; }
        }
    }
}