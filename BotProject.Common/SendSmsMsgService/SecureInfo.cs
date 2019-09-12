using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BotProject.Common.SendSmsMsgService
{
    public class SecureInfo
    {
        private Random m_rand;

        public string AccessCode = ConfigHelper.ReadString("AccessCode");
        public string SecretKey = ConfigHelper.ReadString("SecretKey");
        public string SecureExtension = ConfigHelper.ReadString("SecureExtension");
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
            int iRandomNumLen = 5;
            string strAccessCode = ConfigHelper.ReadString("AccessCode");
            string strSecretKey = ConfigHelper.ReadString("SecretKey");
            string strExtension = ConfigHelper.ReadString("SecureExtension");
            string strRandom = RandomStr(iRandomNumLen);
            return GenXmlParam(strFunctionName, arrName, arrValue, strAccessCode, strSecretKey, strRandom, strExtension);
        }
    }

}
