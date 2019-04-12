using BotProject.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AIML.BOT.Utils
{
    public class Common
    {
        public static string ReadString(string key)
        {
            try
            {
                string path = PathConfig.PathAppConfig;
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode node = doc.SelectSingleNode("AppSettings");
                XmlNodeList prop = node.SelectNodes("add");

                foreach (XmlNode item in prop)
                {
                    var objKey = item.Attributes["key"];
                    var objVal = item.Attributes["value"];
                    if (objKey.Value == key)
                    {
                        return objVal.Value;
                    }
                }
                return "";
            }
            catch(Exception ex)
            {
                return "";
            }
        }
    }
}
