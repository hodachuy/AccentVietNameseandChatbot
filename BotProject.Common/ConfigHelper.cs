using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BotProject.Common
{
    public class ConfigHelper
    {
        public static string pathApp = CurrentAssemblyDirectory() + "AppSettings.config";
        public static string CurrentAssemblyDirectory()
        {
            return System.AppDomain.CurrentDomain.BaseDirectory;
        }

        public static string GetByKey(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }

        public static string ReadString(string key)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(pathApp);
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
            catch
            {
                return "";
            }
        }
    }
}
