using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accent.Utils;


namespace Accent.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] x = { 6, 2, 3, 4, 5, 9, 1 };
            printArray(x);

            int left = 0;
            int right = x.Length - 1;

            var a = x[right];

            quickSort(x, left, right);

            printArray(x);
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
