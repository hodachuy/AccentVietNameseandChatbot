using Accent.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Accent.WebService
{
    sealed class AccentService
    {
        AccentPredictor accent = new AccentPredictor();

        private static AccentService accentInstance = null;
        private static readonly object lockObject = new object();
        private AccentService()
        {
            string path1Gram = HostingEnvironment.MapPath("~/Datasets/news1gram");
            string path2Gram = HostingEnvironment.MapPath("~/Datasets/news2grams");
            //accent.InitNgram(path1Gram, path2Gram);

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