using Accent.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Accent.KShortestPaths;
namespace Accent.Web
{
    sealed class AccentService
    {
        AccentPredictor accent = new AccentPredictor();

        private static AccentService accentInstance = null;
        private static readonly object lockObject = new object();
        private AccentService()
        {
            string _path1Gram = HostingEnvironment.MapPath("~/Datasets/news1gram");
            string _path2Gram = HostingEnvironment.MapPath("~/Datasets/news2grams");
            accent.InitNgram(_path1Gram, _path2Gram);
        }
        public static AccentService AccentInstance
        {
            get
            {
                if (accentInstance == null)
                {
                    lock (lockObject)
                    {
                        if (accentInstance == null)
                        {
                            accentInstance = new AccentService();
                        }

                    }
                }
                return accentInstance;
            }
        }
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