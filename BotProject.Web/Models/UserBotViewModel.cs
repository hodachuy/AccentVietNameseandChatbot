using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace BotProject.Web.Models
{
    [Serializable]
    public class UserBotViewModel
    {
        public string ID { set; get; }
        public string BotID { set; get; }
        public string StopWord { set; get;}
        public SettingsDictionaryViewModel SettingDicstionary { set; get; }
    }
    [Serializable]
    public class SettingsDictionaryViewModel
    {
        public Dictionary<string, string> settingsHash { set; get; }
        public string[] SettingNames { set; get; }
        public List<string> orderedKeys { set; get; }
        public int Count { set; get; }
    }
}