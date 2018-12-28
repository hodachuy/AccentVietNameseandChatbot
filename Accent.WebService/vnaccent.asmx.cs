using Accent.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;

namespace Accent.WebService
{
    /// <summary>
    /// Summary description for vnaccent
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class vnaccent : System.Web.Services.WebService
    {
        private AccentService _accent;
        public vnaccent()
        {
            _accent = AccentService.AccentInstance;
        }
        [WebMethod]
        public string ConvertVN(string text)
        {
            if (!String.IsNullOrEmpty(text))
                text = Regex.Replace(HttpUtility.HtmlDecode(text), @"<(.|\n)*?>", "");
            string textVN = _accent.GetAccentVN(text);
            return textVN;
        }
        public string GetAccentVN(string text)
        {
            if (!String.IsNullOrEmpty(text))
                text = Regex.Replace(HttpUtility.HtmlDecode(text), @"<(.|\n)*?>", "");

            string textVN = _accent.GetAccentVN(text);
            string arrTextVN = _accent.GetMultiMatchesAccentVN(text, 5);

            return arrTextVN;
        }
    }
}
