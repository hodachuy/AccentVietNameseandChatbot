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

namespace Accent.ConsoleApplication
{
    class Program
    {
        public class SearchNlpQnAViewModel
        {
            public string _id { set; get; }
            public string question { set; get; }
            public string answer { set; get; }
            public string html { set; get; }
            public string field { set; get; }
            public int id { set; get; }
        }
        public class ProactiveMessage
        {
            public string id { set; get; }
            public string started { set; get; }
            public string timeout { set; get; }
        }
        public static List<ProactiveMessage> _lstProac = new List<ProactiveMessage>();
        public class HelloJob : IJob
        {
            public void Execute(IJobExecutionContext context)
            {
                JobKey key = context.JobDetail.Key;
                //JobDataMap dataMapDefault = context.JobDetail.JobDataMap;
                //string userId = dataMapDefault.GetString("userId");
                //Console.WriteLine("HelloJob is executing. " + userId);
                //if(userId == "0")
                //{

                //}
                ProactiveMessage pr = new ProactiveMessage();

                JobDataMap dataMap = context.MergedJobDataMap;
                string userId2 = dataMap.GetString("userId");
                string TimeStarted = dataMap.GetString("TimeStarted");
                string TimeOut = dataMap.GetString("timeout");

                pr.id = userId2;
                pr.started = TimeStarted;
                pr.timeout = TimeOut;

                _lstProac.Add(pr);

                var lstActive = _lstProac.OrderBy(x => x.timeout).GroupBy(x => x.id);

                string abc = JObject.FromObject(
                          new
                          {
                              recipient = new { id = "2666433486706739" },
                              message = new { text = "Welcome to Chatbot Lacviet!" },
                          }).ToString();

                SendMessage(abc, "2666433486706739");
                Console.WriteLine("HelloJob is executing. " + userId2 +" Started:" + TimeStarted + " TimeOut:" + TimeOut + " Count:" + _lstProac.Count() + " ItemCount:" + lstActive.Count());
            }
        }

        public static void Main(string[] args)
        {

            while (true)
            {
                Console.InputEncoding = Encoding.Unicode;
                Console.WriteLine("Nhap giờ :");
                string text = Console.ReadLine();
                if (text == "exit")
                {
                    break;
                }
                DateTime timeCurrent = DateTime.Now.AddHours(Convert.ToDouble(text));

                if((timeCurrent.DayOfWeek == DayOfWeek.Saturday) || (timeCurrent.DayOfWeek == DayOfWeek.Sunday))
                {

                }

                if ((timeCurrent.Hour >= 8 && timeCurrent.Hour < 12))
                {
                    Console.WriteLine("ok");
                }else if (timeCurrent.Hour >= 13 && (timeCurrent.TimeOfDay < System.TimeSpan.Parse("17:30:00")))
                {
                    Console.WriteLine("ok");
                }else
                {
                    Console.WriteLine("fail :");
                }
            }


            

            // define the job and tie it to our HelloJob class
            //IJobDetail job = JobBuilder.Create<HelloJob>()
            //    .WithIdentity("s1", "group1") // name "myJob", group "group1"
            //    .UsingJobData("userId", "0")
            //    .Build();

            //while (true)
            //{
            //    Console.InputEncoding = Encoding.Unicode;
            //    Console.WriteLine("Nhap chuoi :");
            //    string text = Console.ReadLine();
            //    if (text == "exit")
            //    {
            //        break;
            //    }

            //    //Schedule("groupUser1", text, "trigger1");
            //    Console.WriteLine("abc :");
            //}

            //Schedule("groupUser1","1235","trigger1");
            //System.Threading.Thread.Sleep(5000);
            //Schedule("groupUser1", "123875", "trigger1");

            //System.Threading.Thread.Sleep(2000);
            //Schedule("group2","6789");
        }
        public static void Schedule(string name, string UserId, string triggerName)
        {
            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();


            //define the job and tie it to our HelloJob class
           IJobDetail job = JobBuilder.Create<HelloJob>()
                //.WithIdentity("ProactiveMsgJob", "ProactiveMsgJob") 
                //.UsingJobData("userId", "0")
                .Build();



            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
                    //.WithIdentity(triggerName, "ProactiveMsgJob")
                    .UsingJobData("userId", UserId)
                    .UsingJobData("TimeStarted", DateTime.Now.ToLocalTime().ToString())
                    .UsingJobData("timeout", DateTime.Now.AddSeconds(5).ToLocalTime().ToString())
                    .StartAt(DateTime.Now.AddSeconds(5).ToLocalTime())
                    //.WithSimpleSchedule(x => x
                    //    .WithIntervalInSeconds(5)
                    //    .WithRepeatCount(0))
                    //.ForJob("ProactiveMsgJob", group)
                    .Build();

            sched.ScheduleJob(job, trigger);

        }



        private static async Task<HttpResponseMessage> SendMessage(string templateJson, string sender)
        {
            string pageToken = "EAAhdfR3uJyYBAC3Af74l0eB92vGj4rom6PZAmIJg32cwwnmPoCmCnr6Hf1Y0ZAxyvbVuPjazIwWi1h4lppZAPbDSLBakjFdojM02Ipk2rOnjvfCTyzBTCwmUv4CZATIxLNflL7cmbkYeZAfuZBf9CgElNyFmKHLdb9gtb5ZAnI1owZDZD";
            HttpResponseMessage res;
            if (!String.IsNullOrEmpty(templateJson))
            {
                string Domain = "https://bot.surelrn.vn/";
                templateJson = templateJson.Replace("{{senderId}}", sender);
                templateJson = Regex.Replace(templateJson, "File/", Domain + "File/");
                templateJson = Regex.Replace(templateJson, "<br />", "\\n");
                templateJson = Regex.Replace(templateJson, "<br/>", "\\n");
                templateJson = Regex.Replace(templateJson, @"\\n\\n", "\\n");
                templateJson = Regex.Replace(templateJson, @"\\n\\r\\n", "\\n");
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    res = await client.PostAsync($"https://graph.facebook.com/v3.2/me/messages?access_token=" + pageToken + "", new StringContent(templateJson, Encoding.UTF8, "application/json"));
                }
            }
            return new HttpResponseMessage(HttpStatusCode.OK);

        }

        private static JObject GetMessageTemplate(string text, string sender)
        {
            return JObject.FromObject(
                new
                {
                    recipient = new { id = sender },
                    message = new { text = "Welcome to Chatbot Lacviet!" },
                });
        }
        //private static JObject GetMessageTemplate(string text, string sender)
        //{
        //    return JObject.FromObject(new
        //    {
        //        recipient = new { id = sender },
        //        message = new {
        //            text = "Welcome to Chatbot2!",
        //            template = new
        //            {
        //                text = "abc"
        //            }
        //        }
        //    });
        //}

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

        public static void quickSort(int[] arr, int left, int right)
        {
            if (arr == null || arr.Length == 0)
                return;

            if (left >= right)
                return;

            int middle = left + (right - left) / 2;
            int pivot = arr[middle];
            int i = left, j = right;

            while (i <= j)
            {
                while (arr[i] < pivot)
                {
                    i++;
                }

                while (arr[j] > pivot)
                {
                    j--;
                }

                if (i <= j)
                {
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                    i++;
                    j--;
                }
            }

            if (left < j)
                quickSort(arr, left, j);

            if (right > i)
                quickSort(arr, i, right);
        }
        public static void printArray(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine("DS Ket qua : {0}", arr[i] + " ");
            }

            Console.WriteLine();
        }

        public object Parse(string valueToConvert, Type dataType)
        {
            TypeConverter obj = TypeDescriptor.GetConverter(dataType);
            object value = obj.ConvertFromString(null, CultureInfo.InvariantCulture, valueToConvert);
            return value;
        }
        public class NameDuplicatedException : Exception
        {
            public NameDuplicatedException()
            {
            }

            public NameDuplicatedException(string message)
            : base(message)
            {
            }

            public NameDuplicatedException(string message, Exception inner)
            : base(message, inner)
            {
            }
        }
    }


    public class MyClassBuilder
    {
        AssemblyName asemblyName;
        public MyClassBuilder(string ClassName)
        {
            this.asemblyName = new AssemblyName(ClassName);
        }
        public object CreateObject(string[] PropertyNames, Type[] Types)
        {
            if (PropertyNames.Length != Types.Length)
            {
                Console.WriteLine("The number of property names should match their corresopnding types number");
            }

            TypeBuilder DynamicClass = this.CreateClass();
            this.CreateConstructor(DynamicClass);
            for (int ind = 0; ind < PropertyNames.Count(); ind++)
                CreateProperty(DynamicClass, PropertyNames[ind], Types[ind]);
            Type type = DynamicClass.CreateType();

            return Activator.CreateInstance(type);
        }
        private TypeBuilder CreateClass()
        {
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(this.asemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
            TypeBuilder typeBuilder = moduleBuilder.DefineType(this.asemblyName.FullName
                                , TypeAttributes.Public |
                                TypeAttributes.Class |
                                TypeAttributes.AutoClass |
                                TypeAttributes.AnsiClass |
                                TypeAttributes.BeforeFieldInit |
                                TypeAttributes.AutoLayout
                                , null);
            return typeBuilder;
        }
        private void CreateConstructor(TypeBuilder typeBuilder)
        {
            typeBuilder.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
        }
        private void CreateProperty(TypeBuilder typeBuilder, string propertyName, Type propertyType)
        {
            FieldBuilder fieldBuilder = typeBuilder.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

            PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
            MethodBuilder getPropMthdBldr = typeBuilder.DefineMethod("get_" + propertyName, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, propertyType, Type.EmptyTypes);
            ILGenerator getIl = getPropMthdBldr.GetILGenerator();

            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);

            MethodBuilder setPropMthdBldr = typeBuilder.DefineMethod("set_" + propertyName,
                  MethodAttributes.Public |
                  MethodAttributes.SpecialName |
                  MethodAttributes.HideBySig,
                  null, new[] { propertyType });

            ILGenerator setIl = setPropMthdBldr.GetILGenerator();
            Label modifyProperty = setIl.DefineLabel();
            Label exitSet = setIl.DefineLabel();

            setIl.MarkLabel(modifyProperty);
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fieldBuilder);

            setIl.Emit(OpCodes.Nop);
            setIl.MarkLabel(exitSet);
            setIl.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getPropMthdBldr);
            propertyBuilder.SetSetMethod(setPropMthdBldr);
        }
    }

    public class HMACSHA1Encrypt
    {
        public HMACSHA1Encrypt()
        {
        }

        public static string Encrypt(string strKey, string strSrc)
        {
            if (string.IsNullOrEmpty(strKey) || string.IsNullOrEmpty(strSrc)) return "";

            ASCIIEncoding ascEnc = new ASCIIEncoding();
            HMACSHA1 hmacsha1 = new HMACSHA1(ascEnc.GetBytes(strKey));
            byte[] hashValue = hmacsha1.ComputeHash(ascEnc.GetBytes(strSrc));
            hmacsha1.Clear();
            return Convert.ToBase64String(hashValue);
        }
    }



    public class SecureInfo
    {
        private Random m_rand;

        public string AccessCode = "SureHis";
        public string SecretKey = "Lv@HospitalMngt";
        public string SecureExtension = "xJBrDX";
        public SecureInfo()
        {
            m_rand = new Random(time());
        }

        private int time()
        {
            DateTime dtStart = new DateTime(2010, 9, 1, 0, 0, 0);
            return (int)(((DateTime.Now - dtStart).Ticks) % int.MaxValue);
        }

        public int RandomNum()
        {
            if (m_rand == null) m_rand = new Random(time());
            //string strFileName = (m_rand.Next(int.MaxValue) % 16777216).ToString("X");
            return m_rand.Next();
        }

        public int RandomNum(int iMinValue, int iMaxValue)
        {
            if (m_rand == null) m_rand = new Random(time());
            //string strFileName = (m_rand.Next(int.MaxValue) % 16777216).ToString("X");
            return m_rand.Next(iMinValue, iMaxValue);
        }

        public string RandomStr(int iStrLen)
        {
            int iRandom = RandomNum(0, 999999999);
            string strRandom = iRandom.ToString().PadLeft(iStrLen, '0');
            strRandom = ((strRandom.Length == iStrLen) ? strRandom : (strRandom.Substring(strRandom.Length - iStrLen, iStrLen)));
            System.Diagnostics.Debug.Assert(strRandom.Length == iStrLen);
            return strRandom;
        }

        //public string GenAccessCode(string strRandom)
        //{
        //    string strAccessCode = ConfigReader.readString("AccessCode");
        //    return strAccessCode + strRandom;
        //}

        public string GenSignature(string strSecretKey, string strFunctionName, string strRandom, string strExtension)
        {
            return HMACSHA1Encrypt.Encrypt(strSecretKey, strFunctionName + strRandom + strExtension);
        }

        public string GenSignature(string strFunctionName, string strRandom)
        {
            string strSecretKey = SecretKey;// ConfigReader.readString("SecretKey");
            string strExtension = SecureExtension;// ConfigReader.readString("SecureExtension");
            //return HMACSHA1Encrypt.Encrypt(strSecretKey, strFunctionName + strRandom + strExtension);
            return GenSignature(strSecretKey, strFunctionName, strRandom, strExtension);
        }

        public string GenXmlParam(string strFunctionName, string[] arrName, string[] arrValue, string strAccessCode, string strSecretKey, string strRandom, string strExtension)
        {
            if (arrName == null || arrName.Length == 0 || arrValue == null || arrValue.Length == 0 || arrName.Length != arrValue.Length) return "";

            StringBuilder strXml = new StringBuilder();
            strXml.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<params>\r\n");
            for (int i = 0; i < arrName.Length; i++)
                strXml.Append("<" + arrName[i] + ">" + arrValue[i] + "</" + arrName[i] + ">\r\n");

            // signature
            strXml.Append("<OTV>" + strRandom + "</OTV>\r\n");
            strXml.Append("<AccessCode>" + strAccessCode + "</AccessCode>\r\n");
            strXml.Append("<Signature>" + (new SecureInfo()).GenSignature(strSecretKey, strFunctionName, strRandom, strExtension) + "</Signature>\r\n");

            strXml.Append("</params>");
            return strXml.ToString();
        }

        public string GenXmlParam(string strFunctionName, string[] arrName, string[] arrValue)
        {
            int iRandomNumLen = 6;
            string strAccessCode = "SureHis";// ConfigReader.readString("AccessCode");
            string strSecretKey = "SureHis_SecretKey";//ConfigReader.readString(strAccessCode + "_SecretKey");
            string strExtension = "xJBrDX";// ConfigReader.readString("SecureExtension");
            string strRandom = RandomStr(iRandomNumLen);
            return GenXmlParam(strFunctionName, arrName, arrValue, strAccessCode, strSecretKey, strRandom, strExtension);
        }
    }
}
