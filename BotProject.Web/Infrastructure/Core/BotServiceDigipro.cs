using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AIMLbot;
using System.Web.Hosting;
using System.Threading.Tasks;
using BotProject.Common;
using BotProject.Web.Models;
using BotProject.Service;
using System.Xml;
using BotProject.Web.Infrastructure.Log4Net;

namespace BotProject.Web
{
    sealed class BotServiceDigipro
    {
        Bot _bot;
        User _user;
        private static BotServiceDigipro botInstance = null;
        private static readonly object lockObject = new object();
        private string pathSetting = PathServer.PathAIML + "config";
        private BotServiceDigipro()
        {
            _bot = new Bot();
            _bot.isAcceptingUserInput = true;
            _bot.loadSettings(pathSetting);

        }
        public static BotServiceDigipro BotInstance
        {
            get
            {
                if (botInstance == null)
                {
                    lock (lockObject)
                    {
                        if (botInstance == null)
                        {
                            botInstance = new BotServiceDigipro();
                        }

                    }
                }
                return botInstance;
            }
        }


        public AIMLbot.Result Chat(string text, User userBot)//, AIMLbot.Utils.SettingsDictionary Predicates
        {
            AIMLbot.Request r = new Request(text, userBot, _bot);
            AIMLbot.Result result = _bot.Chat(r);
            return result;
        }

        public AIMLbot.User loadUserBot(string userId)
        {
            _user = new User(userId, _bot);
            return _user;
        }

        public AIMLbot.Utils.SettingsDictionary loadPredicates()
        {
            return _user.Predicates;
        }

        public void loadSetting()
        {
            _bot.loadSettings(pathSetting);
        }

        public void loadAIMLFromFiles(string path)
        {
            _bot.loadAIMLFromFiles(path);
        }

        public void loadAIMLFromDatabase(IEnumerable<AIMLViewModel> lstAIML)
        {
            if (lstAIML.Count() != 0)
            {
                foreach (var item in lstAIML)
                {
                    try
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(item.Content);
                        _bot.loadAIMLFromXML(doc, item.Src);

                    }
                    catch (Exception ex)
                    {
                        string msg = item.Content + ex.Message;
                        BotLog.Info(msg);
                    }
                }
            }
        }
    }
}