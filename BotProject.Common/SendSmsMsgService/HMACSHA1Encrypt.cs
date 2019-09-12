using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Common.SendSmsMsgService
{
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
}
