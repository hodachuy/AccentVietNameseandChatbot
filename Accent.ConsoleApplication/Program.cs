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
using System.Threading;
using static System.Console;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

namespace Accent.ConsoleApplication
{
    class Program
    {
        private static double GetRating()
        {
            int star5 = 0;
            int star4 = 2;
            int star3 = 1;
            int star2 = 0;
            int star1 = 3;

            double rating = (double)(5 * star5 + 4 * star4 + 3 * star3 + 2 * star2 + 1 * star1) / (star1 + star2 + star3 + star4 + star5);

            rating = Math.Round(rating, 1);

            return rating;
        }

        public static string HighLightWord(string strString, string strKeyWord, bool bNosign)
        {
            if (string.IsNullOrEmpty(strString) || string.IsNullOrEmpty(strKeyWord))
                return strString;

            string strStringLower = strString.ToLower();
            string strWord = strKeyWord;
            if (!string.IsNullOrEmpty(strWord))
            {
                int iStartAt = 0, iFound = -1;
                ArrayList arrOpenTag = new ArrayList();
                while (iStartAt < strStringLower.Length)
                {
                    if ((iFound = strStringLower.IndexOf(strWord, iStartAt)) < 0)
                        break;
                    arrOpenTag.Add(iFound);
                    iStartAt = iFound + 1;
                }

                for (int i = arrOpenTag.Count - 1; i >= 0; i--)
                {
                    int iOpenTag = (int)arrOpenTag[i];
                    if (iOpenTag > 0)
                    {
                        char chPre = strStringLower[iOpenTag - 1];
                        if (chPre != ' ' && chPre != ',' && chPre != '.' && chPre != ';' && chPre != ':' && chPre != '!' && chPre != '?' && chPre != '\'' && chPre != '"' && chPre != '(' && chPre != ')' && chPre != '|' && chPre != '\r' && chPre != '\n')
                            continue;
                    }
                    int iCloseTag = iOpenTag + strWord.Length;
                    if (iCloseTag < strStringLower.Length)
                    {
                        char chNext = strStringLower[iCloseTag];
                        if (chNext != ' ' && chNext != ',' && chNext != '.' && chNext != ';' && chNext != ':' && chNext != '!' && chNext != '?' && chNext != '\'' && chNext != '"' && chNext != '(' && chNext != ')' && chNext != '|' && chNext != '\r' && chNext != '\n')
                            continue;
                    }

                    strStringLower = strStringLower.Insert(iCloseTag, "</b>");
                    strStringLower = strStringLower.Insert(iOpenTag, "<b>");

                    strString = strString.Insert(iCloseTag, "</b>");
                    strString = strString.Insert(iOpenTag, "<b>");
                }
            }
            return strString;
        }


        public static void Main(string[] args)
        {
            string hl = "Chuyển nhượng, nhận thừa kế, quà tặng là bất thừa động sản tại Việt Nam";
            string hlw = HighLightWord(hl, "thừa", false);

            double rating = GetRating();
            Console.WriteLine("rating: " + rating);
            Console.ReadKey();

            string v = "Chuyển nhượng\n, nhận thừa kế\n, quà tặng là bất động sản tại Việt Nam";

            string v2 = Regex.Replace(v, "\n", " ");

            List<string> lstDoc = new List<string>();
            lstDoc.Add("Chuyển nhượng, nhận thừa kế, quà tặng là bất động sản tại Việt Nam");
            lstDoc.Add("Chuyển nhượng vốn (trừ chuyển nhượng chứng khoán)");
            lstDoc.Add("Đầu tư vốn (trường hợp nhận cổ tức bằng cổ phiếu, lợi tức ghi tăng vốn)");
            lstDoc.Add("Khai thuế TNCN theo tháng hoặc quý đối với tổ chức, cá nhân trả thu nhập chịu thuế thu nhập cá nhân");
            lstDoc.Add("Khai thuế TNCN theo tháng hoặc quý đối với tổ chức, cá nhân trả thu nhập chịu thuế thu nhập cá nhân");

            var lstFind = lstDoc.FindAll(x => x.ToLower().Contains("chuyển nhượng") || x.Contains("a"));

            Console.OutputEncoding = Encoding.UTF8;

            if (lstFind.Count() != 0)
            {
                foreach(var item in lstFind)
                {
                    Console.WriteLine(item);
                }
            }





            //LoadBalancer b1 = LoadBalancer.GetLoadBalancer();
            //LoadBalancer b2 = LoadBalancer.GetLoadBalancer();
            //LoadBalancer b3 = LoadBalancer.GetLoadBalancer();
            //LoadBalancer b4 = LoadBalancer.GetLoadBalancer();

            //// Same instance?

            //if (b1 == b2 && b2 == b3 && b3 == b4)
            //{
            //    Console.WriteLine("Same instance\n");
            //}

            //// Load balance 15 server requests
            //LoadBalancer balancer = LoadBalancer.GetLoadBalancer();

            //for (int i = 0; i < 15; i++)
            //{
            //    string server = balancer.Server;
            //    Console.WriteLine("Dispatch Request to: " + server);
            //}

            //// Wait for user

            //Console.ReadKey();


            //AccentPredictor accent = new AccentPredictor();

            //string path1Gram = System.IO.Path.GetFullPath("news1gram.bin");
            //string path2Gram = System.IO.Path.GetFullPath("news2grams.bin");
            //string path1Statistic = System.IO.Path.GetFullPath("_1Statistic");
            //accent.InitNgram2(path1Gram, path2Gram, path1Statistic);

            //////Console.OutputEncoding = Encoding.UTF8;
            //////----- Test -----//
            //////Console.WriteLine("Accuary: " + accent.getAccuracy(System.IO.Path.GetFullPath("test.txt")) + "%");

            //while (true)
            //{
            //    Console.InputEncoding = Encoding.Unicode;
            //    Console.OutputEncoding = Encoding.UTF8;
            //    Console.WriteLine("Nhap chuoi :");
            //    string text = Console.ReadLine();
            //    if (text == "exit")
            //    {
            //        break;
            //    }
            //    if (text == "1")
            //    {
            //        accent.InitNgram2(path1Gram, path2Gram, path1Statistic);
            //    }
            //    string results = accent.predictAccentsWithMultiMatches(text, 10);
            //    Console.WriteLine("DS Ket qua : {0}", results);

            //    Console.WriteLine("Ket qua : {0}", accent.predictAccents(text));
            //}
        }


        class LoadBalancer
        {
            private static LoadBalancer _instance;
            private List<string> _servers = new List<string>();
            private Random _random = new Random();

            // Lock synchronization object

            private static object syncLock = new object();

            // Constructor (protected)

            protected LoadBalancer()
            {
                // List of available servers
                _servers.Add("ServerI");
                _servers.Add("ServerII");
                _servers.Add("ServerIII");
                _servers.Add("ServerIV");
                _servers.Add("ServerV");
            }

            public static LoadBalancer GetLoadBalancer()
            {
                // Support multithreaded applications through

                // 'Double checked locking' pattern which (once

                // the instance exists) avoids locking each

                // time the method is invoked

                if (_instance == null)
                {
                    lock (syncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new LoadBalancer();
                        }
                    }
                }

                return _instance;
            }

            // Simple, but effective random load balancer
            public string Server
            {
                get
                {
                    int r = _random.Next(_servers.Count);
                    return _servers[r].ToString();
                }
            }
        }
    }
}
