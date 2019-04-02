using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AIMLbot;
using System.Web.Hosting;
using System.Threading.Tasks;
using BotProject.Common;

namespace BotProject.Web
{
    sealed class BotService
    {
        Bot _bot;
        private User _user;
        private static BotService botInstance = null;
        private static readonly object lockObject = new object();
        private string pathSetting = PathServer.PathAIML + "config";
        private BotService()
        {
             _bot = new Bot();
            _user = new User(Guid.NewGuid().ToString(), _bot);
            _bot.loadSettings(pathSetting);           
            _bot.isAcceptingUserInput = true;
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
        public AIMLbot.Result Chat(string text)
        {
            //if (String.IsNullOrEmpty(logoBot))
            //{
            //    logoBot = System.Configuration.ConfigurationManager.AppSettings["Domain"] + "assets/images/user_bot.jpg";
            //}
            //if (String.IsNullOrEmpty(colorFormBot))
            //{
            //    colorFormBot = "rgb(234, 82, 105);";
            //}
            //AIMLbot.Request r = new Request(text, _user, _bot);

            //AIMLbot.Result result = _bot.Chat(r, colorFormBot, logoBot);

            AIMLbot.Result result = _bot.Chat(text, _user.UserID);
            return result;
        }
        public void loadAIMLFromFiles(string path)
        {
            _bot.loadAIMLFromFiles(path);
        }
    }
}