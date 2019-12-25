using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accent.Utils;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Specialized;
using System.Dynamic;
using System.ComponentModel;
using System.Globalization;
using Newtonsoft.Json;
using System.Web;
using System.Security.Cryptography;
using Accent.ConsoleApplication.SmsService;
using System.Xml;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using Quartz;
using Quartz.Impl;
using System.IO;
using System.Web.Script.Serialization;

namespace Accent.ConsoleApplication
{
    class Program
    {

        public static void Main(string[] args)
        {

            var x = GetProfileUser("2723257564384999");

            AccentPredictor accent = new AccentPredictor();

            string path1Gram = System.IO.Path.GetFullPath("news1gram");
            string path2Gram = System.IO.Path.GetFullPath("news2grams");
            accent.InitNgram(path1Gram, path2Gram);

            Console.OutputEncoding = Encoding.UTF8;

            //----- Test -----//
            //Console.WriteLine("Accuary: " + accent.getAccuracy(System.IO.Path.GetFullPath("test.txt")) + "%");

            while (true)
            {
                Console.InputEncoding = Encoding.Unicode;
                Console.WriteLine("Nhap chuoi :");
                string text = Console.ReadLine();
                if (text == "exit")
                {
                    break;
                }
                string results = accent.predictAccentsWithMultiMatches(text, 10);
                Console.WriteLine("DS Ket qua : {0}", results);

                Console.WriteLine("Ket qua : {0}", accent.predictAccents(text));
            }
        }
        public static string GetProfileUser(string senderId)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage res = new HttpResponseMessage();
                string pageToken = "EAAH5LTILzD8BAG0rz64t8pb9kbYckP11Mc1pLigLPzjMf07u5065zjt2tfLThBXTrWvCNTDZAeRdtz3hW3jzqgZBVqTrYeuB6aVcqy4wQSxnO0G8AadfBj7QYpHJTmSoNMONYrSlfVPGou0kONnSDJaKcwrDDMYSThEq84EwZDZD";
                res = client.GetAsync($"https://graph.facebook.com/" + senderId + "?fields=first_name,last_name,profile_pic&access_token=" + pageToken).Result;
                if (res.IsSuccessStatusCode)
                {
                    User user = new User();
                    var serializer = new JavaScriptSerializer();
                    serializer.MaxJsonLength = Int32.MaxValue;
                    var x = serializer.Deserialize<User>(res.Content.ReadAsStringAsync().Result);
                    string gen = x.first_name;
                }
                return "";
            }
        }

        public class User
        {
            public string first_name { set; get; }
            public string last_name { set; get; }
            public string profile_pic { set; get; }
            public string id { set; get; }
            public string gender { set; get; }
        }
    }
}
