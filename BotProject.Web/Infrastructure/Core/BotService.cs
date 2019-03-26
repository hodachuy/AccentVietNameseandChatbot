using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AIMLbot;
using System.Web.Hosting;
using System.Threading.Tasks;

namespace BotProject.Web
{
    sealed class BotService
    {
        Bot _bot;
        private User _user;
        private static BotService botInstance = null;
        private static readonly object lockObject = new object();
        private string pathSetting = HostingEnvironment.MapPath("~/File/AIML/config");

        private BotService()
        {
             _bot = new Bot();
            _user = new User(Guid.NewGuid().ToString(), _bot);
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
        public AIMLbot.Result Chat(string text, string colorFormBot, string logoBot)
        {
            if (String.IsNullOrEmpty(logoBot))
            {
                logoBot = System.Configuration.ConfigurationManager.AppSettings["Domain"] + "assets/images/user_bot.jpg";
            }
            if (String.IsNullOrEmpty(colorFormBot))
            {
                colorFormBot = "rgb(234, 82, 105);";
            }
            AIMLbot.Request r = new Request(text, _user, _bot);

            AIMLbot.Result result = _bot.Chat(r, colorFormBot, logoBot);

            //AIMLbot.Result rs = _bot.Chat(text, user.UserID);
            return result;
        }
        public void loadAIMLFromFiles(string path)
        {
            _bot.loadAIMLFromFiles(path);
        }
    }
}