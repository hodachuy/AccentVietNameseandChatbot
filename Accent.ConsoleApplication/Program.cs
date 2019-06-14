using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accent.Utils;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace Accent.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //int[] x = { 6, 2, 3, 4, 5, 9, 1 };
            //printArray(x);

            //int left = 0;
            //int right = x.Length - 1;

            //var a = x[right];

            //quickSort(x, left, right);
            bool isNumber = Regex.Match("0375348328", @"/(09|01[2|6|8|9])+([0-9]{8})\b/g").Success;

            string text = "postback_module_med_get_info_patient_13";
            if (text.Contains("postback_module_med_get_info_patient"))
            {
                string x1 = "1";
            }


            //printArray(x);
            Console.OutputEncoding = Encoding.UTF8;
            string x = GetMessageTemplate("abc", "1").ToString();
            Console.WriteLine(x);

            //AccentPredictor accent = new AccentPredictor();

            //string path1Gram = System.IO.Path.GetFullPath("news1gram");
            //string path2Gram = System.IO.Path.GetFullPath("news2grams");
            //accent.InitNgram(path1Gram, path2Gram);

            //Console.OutputEncoding = Encoding.UTF8;

            ////----- Test -----//
            ////Console.WriteLine("Accuary: " + accent.getAccuracy(System.IO.Path.GetFullPath("test.txt")) + "%");

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
        private static JObject GetMessageTemplate(string text, string sender)
        {
            return JObject.FromObject(new
            {
                recipient = new { id = sender },
                message = new {
                    text = "Welcome to Chatbot2!",
                    template = new
                    {
                        text = "abc"
                    }
                }
            });
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

    }
}
