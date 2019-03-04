using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AIMLbot;
using System.Web.Hosting;
using System.Threading.Tasks;

namespace Accent.Web
{
    sealed class BotService
    {
        Bot _bot;
        private User user;
        private static BotService botInstance = null;
        private static readonly object lockObject = new object();
        private string pathSetting = HostingEnvironment.MapPath("~/Datasets_BOT/config");// HostingEnvironment.MapPath("~/Datasets_BOT/config/Settings.xml");

        private BotService()
        {
             _bot = new Bot();
            string userName = "user" + Guid.NewGuid();
            user = new User(userName, _bot);

            _bot.loadSettings(pathSetting);           
            _bot.isAcceptingUserInput = false;
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
            AIMLbot.Request r = new Request(text, user, _bot);
            AIMLbot.Result res = _bot.Chat(r);

            //AIMLbot.Result rs = _bot.Chat(text, user.UserID);
            return res;
        }
        public void loadAIMLFromFiles(string path)
        {
            _bot.loadAIMLFromFiles(path);
        }
    }
}