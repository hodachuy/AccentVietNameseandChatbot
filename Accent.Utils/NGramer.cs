using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Accent.Utils
{
    /// <summary>
    /// Tạo danh sách n-grams
    /// </summary>
    public class NGramer
    {
         string folderPath;
        public NGramer(string folderPath)
        {
            this.folderPath = folderPath;
        }
        public virtual void statisticNGrams(int nFileToProcess, bool lowerCase, string _1GramFile, string _2GramsFile)
        {
            Dictionary<string, int> _1GramMap = new Dictionary<string, int>();
            Dictionary<string, int> _2GramsMap = new Dictionary<string, int>();
            Console.WriteLine(this.folderPath);
            string[] fileList = Directory.GetFiles(folderPath);
            FileProcessor fileProcessor = new FileProcessor();
            int count = 0;
            if (nFileToProcess < 0)
            {
                nFileToProcess = fileList.Length + 5;
            }
            foreach (string fileName in fileList)
            {
                count++;
                if (count > nFileToProcess)
                {
                    break;
                }
                Console.WriteLine(fileName);
                List<string> lines = fileProcessor.readFile(this.folderPath + "/" + fileName);
                string line = "";
                foreach ( string item in lines)
                {

                    if (lowerCase)
                    {
                        line = item.ToLower();
                    }
                    string[] syllables = Regex.Split(line.Replace("_", " "),"\\s+");
                    for (int i = 0; i < syllables.Length; i++)
                    {
                        string _1Gram = syllables[i];

                        if (_1GramMap.ContainsKey(_1Gram))
                        {
                            _1GramMap[_1Gram] = _1GramMap[_1Gram] + 1;
                        }
                        else
                        {
                            _1GramMap[_1Gram] = 1;
                        }

                        if (i < syllables.Length - 1)
                        {
                            string _2Grams = syllables[i] + " " + syllables[i + 1];
                            if (_2GramsMap.ContainsKey(_2Grams))
                            {
                                _2GramsMap[_2Grams] = _2GramsMap[_2Grams] + 1;
                            }
                            else
                            {
                                _2GramsMap[_2Grams] = 1;
                            }
                        }
                    }
                }
            }
            writeToFile(_1GramMap, _1GramFile);
            writeToFile(_2GramsMap, _2GramsFile);
        }

        private void writeToFile(Dictionary<string, int> map, string fileOut)
        {
            try
            {

                FileStream fos = new FileStream(fileOut, FileMode.Create, FileAccess.Write);
                StreamWriter @out = new StreamWriter(fos, Encoding.UTF8);
                foreach (string ngrams in map.Keys)
                {
                    @out.Write(ngrams + "\t" + map[ngrams] + "\n");
                }
                @out.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
        }
    }
}
