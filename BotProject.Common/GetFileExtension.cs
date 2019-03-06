using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Common
{
    public class GetFileExtension
    {
        /// <summary>
        /// To demonstrate extraction of file extension from base64 string.
        /// </summary>
        /// <param name="base64String">base64 string.</param>
        /// <returns>Henceforth file extension from string.</returns>
        public static string GetExtension(string base64String)
        {
            var data = base64String.Substring(0, 20);
            if (data.Contains("jpeg"))
            {
                return "jpg".ToUpper();
            }
            if (data.Contains("png"))
            {
                return "png".ToUpper();
            }
            return "jpg".ToUpper();
        }
    }
}
