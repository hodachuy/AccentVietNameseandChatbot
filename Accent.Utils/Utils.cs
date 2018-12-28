using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accent.Utils
{
    public class Utils
    {
        public static string normaliseString(string input)
        {
            string output = input.Replace("[\t\"\':\\(\\)]", " ").Replace(
                    "\\s{2,}", " ");

            return output.Trim();
        }
    }
}
