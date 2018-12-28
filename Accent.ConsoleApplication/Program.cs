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
    }
}
