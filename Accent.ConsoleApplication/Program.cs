﻿using System;
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

namespace Accent.ConsoleApplication
{
    class Program
    {
        private static bool FindComputer(string title)
        {
            if (title == "Computer")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void Main(string[] args)
        {
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
