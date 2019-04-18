using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Web.Infrastructure.Log4Net
{
    public class BotLog
    {
        public static void Error(Object message)
        {
            Log4NetDAL log = new Log4NetDAL();
            log.Error(message);
        }

        public static void Error(Object message, Exception ex)
        {
            Log4NetDAL log = new Log4NetDAL();
            log.Error(message, ex);
        }

        public static void Debug(Object message)
        {
            Log4NetDAL log = new Log4NetDAL();
            log.Debug(message);
        }

        public static void Debug(Object message, Exception ex)
        {
            Log4NetDAL log = new Log4NetDAL();
            log.Debug(message, ex);
        }

        public static void Info(Object message)
        {
            Log4NetDAL log = new Log4NetDAL();
            log.Info(message);
        }

        public static void Info(Object message, Exception ex)
        {
            Log4NetDAL log = new Log4NetDAL();
            log.Info(message, ex);
        }
    }
}