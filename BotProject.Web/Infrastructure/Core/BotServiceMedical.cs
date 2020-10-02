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

namespace BotProject.Web.Infrastructure.Core
{
    sealed class BotServiceMedical
    {
        Bot _bot;
        User _user;
        private IAIMLFileService _aimlFileService;
        private static BotServiceMedical botInstance = null;
        private static readonly object lockObject = new object();
        private string pathSetting = PathServer.PathAIML + "config";
        private BotServiceMedical()
        {
            _bot = new Bot();
            _bot.isAcceptingUserInput = true;
            _bot.loadSettings(pathSetting);

            //string pathFile = PathServer.PathAIML2Graphmaster + "BotID_3019.bin";
            //_bot.loadFromBinaryFile(pathFile);

            //_aimlFileService = ServiceFactory.Get<IAIMLFileService>();
            //var lstAimlFile = _aimlFileService.GetByBotId(3019);
            //loadAIMLFromDatabase(lstAimlFile);
            _user = loadUserBot(Guid.NewGuid().ToString());
        }
        public static BotServiceMedical BotInstance
        {
            get
            {
                if (botInstance == null)
                {
                    lock (lockObject)
                    {
                        if (botInstance == null)
                        {
                            botInstance = new BotServiceMedical();
                        }

                    }
                }
                return botInstance;
            }
        }


        public AIMLbot.Result Chat(string text)//, AIMLbot.Utils.SettingsDictionary Predicates
        {
            AIMLbot.Request r = new Request(text, _user, _bot);
            AIMLbot.Result result = _bot.Chat(r);
            return result;
        }

        public AIMLbot.User loadUserBot(string userId)
        {
            _user = new User(userId, _bot);
            return _user;
        }      

        public void loadAIMLFromDatabase(IEnumerable<BotProject.Model.Models.AIMLFile> lstAIML)
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

        public void loadAIMLFromFileBinary(string pathFile)
        {
            _bot.loadFromBinaryFile(pathFile);
        }
    }
}