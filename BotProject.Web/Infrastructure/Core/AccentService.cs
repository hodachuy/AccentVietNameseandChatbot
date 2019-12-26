using Accent.Utils;
using BotProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace BotProject.Web
{
    public class AccentService
    {

        AccentPredictor accent;
        //private static AccentService accentInstance = null;
        //private static readonly object lockObject = new object();
        public AccentService()
        {
            //string _path1Gram = HostingEnvironment.MapPath("~/File/Datasets_Training_Accent/news1gram");
            //string _path2Gram = HostingEnvironment.MapPath("~/File/Datasets_Training_Accent/news2grams");
            accent = new AccentPredictor();
            string _path1Gram = PathServer.PathAccent + "news1gram";
            string _path2Gram = PathServer.PathAccent + "news2grams";
            string _path1Statistic = PathServer.PathAccent + "_1Statistic";
            accent.InitNgram(_path1Gram, _path2Gram, _path1Statistic);
        }
        //public static AccentService AccentInstance
        //{
        //    get
        //    {
        //        if (accentInstance == null)
        //        {
        //            lock (lockObject)
        //            {
        //                if (accentInstance == null)
        //                {
        //                    accentInstance = new AccentService();
        //                }

        //            }
        //        }
        //        return accentInstance;
        //    }
        //}
        public string GetAccentVN(string text)
        {
            return accent.predictAccents(text);
        }
        public string GetMultiMatchesAccentVN(string text, int nResults)
        {
            return accent.predictAccentsWithMultiMatches(text, nResults, false);
        }
    }
}