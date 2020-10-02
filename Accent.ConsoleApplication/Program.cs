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

            string test = "abc <url>https://thuedientu.gdt.gov.vn/etaxnnt/Request?&dse_sessionId=KPFbMT9aY7Yq2MEqwYTA8dh&dse_applicationId=-1&dse_pageId=2&dse_operationName=corpIndexProc&dse_errorPage=error_page.jsp&dse_processorState=initial&dse_nextEventName=start</url> asd sad <url>https://thuedientu.gdt.gov.vn/etaxnnt/Request?&dse_sessionId=KPFbMT9aY7Yq2MEqwYTA8dh&dse_applicationId=-1&dse_pageId=2&dse_operationName=corpIndexProc&dse_errorPage=error_page.jsp&dse_processorState=initial&dse_nextEventName=start</url>";
            test = Regex.Replace(test, "<url>(.*?)</url>", m => String.Format("<url>{0}</url>", HttpUtility.HtmlEncode(m.Groups[1].Value)));

            //var lstAIMLBOT = new List<Tuple<Bot, string>>();

            //lstAIMLBOT.Add(new Tuple<Bot, string>(new Bot(), "1"));
            //lstAIMLBOT.Add(new Tuple<Bot, string>(new Bot(), "2"));
            //lstAIMLBOT.Add(new Tuple<Bot, string>(new Bot(), "3"));

            //string botId = "2";
            //for(int i = 0; i < lstAIMLBOT.Count; i ++)
            //{
            //    if(lstAIMLBOT[i].Item2 == botId)
            //    {
            //        var z = "2";
            //    }
            //}
            //var x1 = lstAIMLBOT[0].Item1;
            //var x2 = lstAIMLBOT[1].Item1;
            //var x3 = lstAIMLBOT[2].Item1;


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

            //Console.OutputEncoding = Encoding.UTF8;
            ////-----Test---- -//
            //Console.WriteLine("Accuary: " + accent.getAccuracy(System.IO.Path.GetFullPath("test.txt")) + "%");

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

        // Validate string containt SQL-Injection
        static string CleanInput(string strIn)
        {
            // Replace invalid characters with empty strings.
            try
            {
                return Regex.Replace(strIn, @"[^\w\.@-]", "",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters,
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
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


        private static int[] Map_VNOrigin = {194,226,258,259,202,234,212,244,431,432,416,417,272,
                                273,7840,7841,7842,7843,7844,7845,7846,7847,7848,7849,7850,7851,
                                7852,7853,7854,7855,7856,7857,7858,7859,7860,7861,7862,7863,7864,
                                7865,7866,7867,7868,7869,7870,7871,7872,7873,7874,7875,7876,7877,
                                7878,7879,7880,7881,7882,7883,7884,7885,7886,7887,7888,7889,7890,
                                7891,7892,7893,7894,7895,7896,7897,7898,7899,7900,7901,7902,7903,
                                7904,7905,7906,7907,7908,7909,7910,7911,7912,7913,7914,7915,7916,
                                7917,7918,7919,7920,7921,7922,7923,7924,7925,7926,7927,7928,7929,
                                192,193,195,200,201,204,205,210,211,213,217,218,221,224,225,227,
                                232,233,236,237,242,243,245,249,250,253,360,361,296,297};

        private static int[] Map_VN1258 = {194,226,258,259,202,234,212,244,431,432,416,417,272,273,
                                65,803,97,803,65,777,97,777,194,769,226,769,194,768,226,768,194,777,
                                226,777,194,771,226,771,194,803,226,803,258,769,259,769,258,768,259,
                                768,258,777,259,777,258,771,259,771,258,803,259,803,69,803,101,803,
                                69,777,101,777,69,771,101,771,202,769,234,769,202,768,234,768,202,
                                777,234,777,202,771,234,771,202,803,234,803,73,777,105,777,73,803,
                                105,803,79,803,111,803,79,777,111,777,212,769,244,769,212,768,244,
                                768,212,777,244,777,212,771,244,771,212,803,244,803,416,769,417,769,
                                416,768,417,768,416,777,417,777,416,771,417,771,416,803,417,803,85,
                                803,117,803,85,777,117,777,431,769,432,769,431,768,432,768,431,777,
                                432,777,431,771,432,771,431,803,432,803,89,768,121,768,89,803,121,
                                803,89,777,121,777,89,771,121,771,65,768,65,769,65,771,69,768,69,
                                769,73,768,73,769,79,768,79,769,79,771,85,768,85,769,89,769,97,768,
                                97,769,97,771,101,768,101,769,105,768,105,769,111,768,111,769,111,
                                771,117,768,117,769,121,769,85,771,117,771,73,771,105,771};
        /// <summary>
        /// chuyen doi chuoi Unicode to hop sang chuoi Unicode dung san
        /// </summary>
        /// <param name="strUnicode">chuoi Unicode to hop</param>
        /// <returns>chuoi Unicode dung san</returns>
        //public static string UnicodeVN1258ToUnicodeOrigin(string strUnicode)
        public static string UnicodeVN1258ToUnicodeOrigin(object stringUnicode)
        {
            //if (strUnicode == null)
            if (stringUnicode == null)
                return null;
            StringBuilder strOriginDest = new StringBuilder();
            int i = 0;
            //int iLenOrigin = 134;
            int iLen1258 = 254;

            //string stTest0_14 = tu 0 den 14 cua Map_VN1258;
            //string stMapVN1258 = tu 14 den het cua Map_VN1258;
            string stMapVN1258 = "";
            for (i = 0; i < iLen1258; i++)
                stMapVN1258 += (char)Map_VN1258[i];
            string stMapVN1258_a = stMapVN1258.Substring(0, 14);

            string strUnicode = (string)stringUnicode;
            i = 0;
            while (i < strUnicode.Length)
            {
                if (strUnicode[i] == 9)
                {
                    strOriginDest.Append("\t");
                    i++;
                    continue;
                }
                if (strUnicode[i] < 'A')
                {
                    strOriginDest.Append(strUnicode[i]);
                    i++;
                    continue;
                }
                if (strUnicode[i] > 'Z' && strUnicode[i] < 'a')
                {
                    strOriginDest.Append(strUnicode[i]);
                    i++;
                    continue;
                }
                if (strUnicode[i] >= 'A' && strUnicode[i] <= 'Z' && strUnicode[i] != 'A' && strUnicode[i] != 'E' && strUnicode[i] != 'I' && strUnicode[i] != 'O' && strUnicode[i] != 'U' && strUnicode[i] != 'Y')
                {
                    strOriginDest.Append(strUnicode[i]);
                    i++;
                    continue;
                }
                if (strUnicode[i] >= 'a' && strUnicode[i] <= 'z' && strUnicode[i] != 'a' && strUnicode[i] != 'e' && strUnicode[i] != 'i' && strUnicode[i] != 'o' && strUnicode[i] != 'u' && strUnicode[i] != 'y')
                {
                    strOriginDest.Append(strUnicode[i]);
                    i++;
                    continue;
                }
                if (i + 1 < strUnicode.Length)
                {
                    string stFind = strUnicode[i].ToString() + strUnicode[i + 1];
                    int k = stMapVN1258.IndexOf(stFind, 14);
                    if (k != -1)
                    {
                        strOriginDest.Append((char)Map_VNOrigin[14 + (k - 14) / 2]);
                        i += 2;
                    }
                    else
                    {
                        stFind = strUnicode[i].ToString();
                        k = stMapVN1258_a.IndexOf(stFind);
                        if (k != -1)
                        {
                            strOriginDest.Append((char)Map_VNOrigin[k]);
                        }
                        else strOriginDest.Append(strUnicode[i]);
                        i++;
                    }
                }
                else
                {
                    string stFind = strUnicode[i].ToString();
                    int k = stMapVN1258_a.IndexOf(stFind);
                    if (k != -1)
                    {
                        strOriginDest.Append((char)Map_VNOrigin[k]);
                    }
                    else strOriginDest.Append(strUnicode[i]);
                    i++;
                }
            }
            return strOriginDest.ToString();
        }
    }
}
