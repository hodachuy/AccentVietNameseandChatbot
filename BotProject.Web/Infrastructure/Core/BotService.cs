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
using BotProject.Model.Models;
using System.Configuration;
using System.Text.RegularExpressions;

namespace BotProject.Web
{
    sealed class BotService
    {
        AIMLbot.Bot _bot;
        User _user;
        private static BotService botInstance = null;
        private static readonly object lockObject = new object();
        private string pathSetting = PathServer.PathAIML + "config";
        private BotService()
        {
             _bot = new AIMLbot.Bot();
            _bot.isAcceptingUserInput = true;
            _bot.loadSettings(pathSetting);
            
        }
        public static BotService BotInstance
        {
            get
            {
                if (botInstance == null)
                {
                    lock (lockObject)
                    {
                        if (botInstance == null)
                        {
                            botInstance = new BotService();
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

        /// <summary>
        /// Load tất cả file AIML sẽ kéo vào this.Graphmaster "brain" đầu não của bot,
        /// tránh load AIML mỗi lần khi start ta nên lưu nó vào file binary với param input this.Graphmaster
        /// function saveToBinaryFile tại AIML.Bot
        /// </summary>
        /// <param name="path"></param>

        public void loadAIMLFromFiles(string path)
        {
            _bot.loadAIMLFromFiles(path);
        }
        public void loadAIMLFromDatabase(IEnumerable<AIMLViewModel> lstAIML)
        {
            if(lstAIML.Count() != 0)
            {
                foreach(var item in lstAIML)
                {
                    try
                    {
                        XmlDocument doc = new XmlDocument();
                        item.Content = Regex.Replace(item.Content, "<url>(.*?)</url>", m => String.Format("<url>{0}</url>", HttpUtility.HtmlEncode(m.Groups[1].Value)));
                        doc.LoadXml(item.Content);
                        _bot.loadAIMLFromXML(doc, item.Src);

                    }catch(Exception ex)
                    {
                        string msg = item.Content + ex.Message;
                        BotLog.Info(msg);
                    }
                }
            }
        }

        public void loadAIMLFile(IEnumerable<AIMLFile> lstAIML, string botId)
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

                string pathFolderAIML2Graphmaster = ConfigurationManager.AppSettings["AIML2GraphmasterPath"] + "BotID_" + botId + ".bin";
                saveGraphmaster2AIMLFile(pathFolderAIML2Graphmaster);

            }
        }
        public void saveGraphmaster2AIMLFile(string pathFolderAIML2Graphmaster)
        {
            _bot.saveToBinaryFile(pathFolderAIML2Graphmaster);
        }

        public void loadGraphmaster2AIMLFile(string pathFolderAIML2Graphmaster)
        {
            _bot.loadFromBinaryFile(pathFolderAIML2Graphmaster);
        }


    }
}