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

namespace Accent.ConsoleApplication
{
    class Program
    {
        public static void Main(string[] args)
        {

            Singleton fromManager = Singleton.SingleInstance;
            fromManager.LogMessage("Request Message from Manager");

            Singleton fromEmployee = Singleton.SingleInstance;
            fromEmployee.LogMessage("Request Message from Employee");

            ReadLine();
            while (true)
            {
                Console.InputEncoding = Encoding.Unicode;
                Console.WriteLine("Nhap chuoi :");
                string text = Console.ReadLine();
                if (text == "exit")
                    break;
                if (text == "T")
                {
                    Singleton fromManager1 = Singleton.SingleInstance;
                    fromManager1.LogMessage("Request Message from Manager");

                    Singleton fromEmployee1 = Singleton.SingleInstance;
                    fromEmployee1.LogMessage("Request Message from Employee");
                    ReadLine();
                }
                else
                {
                    Singleton.DerivedClass derivedClass = new Singleton.DerivedClass();
                    derivedClass.LogMessage("new Instances v2");
                }
                ReadLine();
            }

            //AccentPredictor accent = new AccentPredictor();

            //string path1Gram = System.IO.Path.GetFullPath("news1gram");
            //string path2Gram = System.IO.Path.GetFullPath("news2grams");
            //string path1Statistic = System.IO.Path.GetFullPath("_1Statistic");
            //accent.InitNgram(path1Gram, path2Gram, path1Statistic);

            //Console.OutputEncoding = Encoding.UTF8;
            //----- Test -----//
            //Console.WriteLine("Accuary: " + accent.getAccuracy(System.IO.Path.GetFullPath("test.txt")) + "%");

            //while (true)
            //{
            //    Console.InputEncoding = Encoding.Unicode;
            //    Console.WriteLine("Nhap chuoi :");
            //    string text = Console.ReadLine();
            //    if (text == "exit")
            //    {
            //        break;
            //    }
            //    string results = accent.predictAccentsWithMultiMatches(text, 10);
            //    Console.WriteLine("DS Ket qua : {0}", results);

            //    Console.WriteLine("Ket qua : {0}", accent.predictAccents(text));
            //}
        }

        public class Singleton
        {
            private static int _lockFlag = 0; // 0 - free
            static int instanceCounter = 0;
            private static readonly Lazy<Singleton> singleInstance = new Lazy<Singleton>(() => new Singleton()); //private static Singleton singleInstance = null;  
            private Singleton()
            {

                if (Interlocked.CompareExchange(ref _lockFlag, 1, 0) == 0)
                {
                    // only 1 thread will enter here without locking the object/put the
                    // other threads to sleep.

                    instanceCounter++;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    AccentPredictor accent = new AccentPredictor();
                    string path2Gram = System.IO.Path.GetFullPath("ngram2.bin");
                    accent.InitNgram2(path2Gram);
                    Monitor.Enter(accent);
                    // free the lock.
                    Interlocked.Decrement(ref _lockFlag);
                    WriteLine("Instances created " + instanceCounter);
                }
            }
            public static Singleton SingleInstance
            {
                get
                {
                    return singleInstance.Value;
                }
            }
            public void LogMessage(string message)
            {
                WriteLine(" created " + instanceCounter);
                WriteLine("Message " + message);
            }
            public class DerivedClass : Singleton
            {

            }
        }
        public class Disposable : IDisposable
        {
            private bool isDisposed;

            ~Disposable()
            {
                Dispose(false);
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            private void Dispose(bool disposing)
            {
                if (!isDisposed && disposing)
                {
                    DisposeCore();
                }

                isDisposed = true;
            }

            // Ovveride this to dispose custom objects
            protected virtual void DisposeCore()
            {

            }
        }

        #region T
        public static ProfileUser GetProfileUser(string senderId)
        {
            ProfileUser user = new ProfileUser();
            using (HttpClient client = new HttpClient())
            {
                string userId = JObject.FromObject(
                 new
                 {
                     user_id = senderId
                 }).ToString();
                HttpResponseMessage res = new HttpResponseMessage();
                res = client.GetAsync($"https://openapi.zalo.me/v2.0/oa/getprofile?access_token=uSutO3kxTH3lm3zWDxrz4kh7Fs8-f0zhW9ugOnkPA1R7YmyuJh145iMyPJnGb6ulyEX2D2dVVYdofq0PIALSRScm6NPnkZnqzO06L2oI4bUofGjgA8mw6AoIVWjovsa1rUPG5NRyQJItraueBVTRMPBxKNSFs6HfkjDT62NJInkThdKE9RfhUPYYLbWhtqSlzAfbBNMoO3_GdK8z2OPO2gIvKGnzZ7e3pgf11Kg07tVvjo1dVx8yODgB04C7lnyqtCCVCdJF2Z3SjoizNwys8NdiJND4Fgrr5m&data=" + userId).Result;//gender y/c khi sử dụng
                if (res.IsSuccessStatusCode)
                {
                    var serializer = new JavaScriptSerializer();
                    serializer.MaxJsonLength = Int32.MaxValue;
                    user = serializer.Deserialize<ProfileUser>(res.Content.ReadAsStringAsync().Result);
                }
                return user;
            }
        }
        public class ProfileUser
        {
            public ProfileInfo data { set; get; }
            public int error { set; get; }
            public string message { set; get; }
        }
        public class ProfileInfo
        {
            public long user_id { set; get; }
            public long user_id_by_app { set; get; }
            public string display_name { set; get; }
            public int user_gender { set; get; }
            public string avatar { set; get; }
            public int birthday { set; get; }
        }
        #endregion
    }
}
