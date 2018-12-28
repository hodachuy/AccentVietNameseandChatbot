using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accent.Utils
{
    public class FileProcessor
    {

        /// <summary>
        ///================================================================
        /// </summary>
        /// <param name="content"> of file </param>
        /// <param name="fileName"> output file
        /// ================================================================ </param>
        public virtual void writeFile(string content, string fileName)
        {
            try
            {

                FileStream fos = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                var bytes = new byte[] { unchecked((byte)0xEF), unchecked((byte)0xBB), unchecked((byte)0xBF) };
                fos.Write(bytes, 0, bytes.Length);
                StreamWriter @out = new StreamWriter(fos, Encoding.UTF8);
                @out.Write(content);
                @out.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
        }

        /// <summary>
        ///================================================================
        /// </summary>
        /// <param name="content"> of file </param>
        /// <param name="fileName"> output file
        /// ================================================================ </param>
        public virtual void writeFileNew(string content, string fileName)
        {
            try
            {

                FileStream fos = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                StreamWriter @out = new StreamWriter(fos, Encoding.UTF8);
                @out.Write(content);
                @out.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
        }
        /// <summary>
        ///========================================================================
        /// //Read data from  file
        /// /*========================================================================
        /// </summary>
        public virtual List<string> readFile(string filePath)
        {
            try
            {
                List<string> result = new List<string>();
                using (StreamReader reader = File.OpenText(filePath))
                {
                    string line = "";

                    while ((line = reader.ReadLine()) != null)
                    {
                        result.Add(line);
                    }
                }
                return result;
            }
            catch (IOException)
            {

            }
            return new List<string>();
        }

        public virtual string readFileNew(string @string)
        {
            // TODO Auto-generated method stub
            StringBuilder sb = new StringBuilder("");
            try
            {
                using (StreamReader reader = File.OpenText(@string))
                {
                    string line = "";

                    while ((line = reader.ReadLine()) != null)
                    {
                        sb.Append(reader.ReadLine() + "\n");
                    }
                }

                return sb.ToString();
            }
            catch (IOException)
            {

            }
            return null;
        }
    }

}
