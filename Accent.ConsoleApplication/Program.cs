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

namespace Accent.ConsoleApplication
{
    class Program
    {

        public static void Main(string[] args)
        {

            AccentPredictor accent = new AccentPredictor();

            string path1Gram = System.IO.Path.GetFullPath("news1gram");
            string path2Gram = System.IO.Path.GetFullPath("news2grams");
            string path1Statistic = System.IO.Path.GetFullPath("_1Statistic");
            accent.InitNgram(path1Gram, path2Gram, path1Statistic);

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
    }
}
