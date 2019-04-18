using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Web.Infrastructure.Log4Net
{
    public class Log4NetDAL
    {
        ILog log = log4net.LogManager.GetLogger(typeof(Log4NetDAL)); //type of class     

        public Log4NetDAL()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public void Error(Object message)
        {
            log.Error(message);
        }

        public void Error(Object message, Exception ex)
        {
            log.Error(message, ex);
        }

        public void Debug(Object message)
        {
            log.Debug(message);
        }

        public void Debug(Object message, Exception ex)
        {
            log.Debug(message, ex);
        }

        public void Info(Object message)
        {
            log.Info(message);
        }

        public void Info(Object message, Exception ex)
        {
            log.Info(message, ex);
        }
    }
}