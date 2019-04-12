using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BotProject.Common
{
    public class CommonConstants
    {
        public const string SessionUser = "User";
        public const string Administrator = "Administrator";
        public const string Visitor = "Visitor";
        public const string PathImage = "Card";

        public const string PostBackCard = "postback_card_";

        public const string CreateQnA = "Create";
        public const string UpdateQnA = "Update";
    }

    public class PathServer
    {
        /// <summary>
        /// HttpContext.Current.Server.MapPath("~/File/Images/");
        /// </summary>
        public static string PathImage = HttpContext.Current.Server.MapPath("~/File/Images/");
        /// <summary>
        /// HttpContext.Current.Server.MapPath("~/File/AIML/");
        /// </summary>
        public static string PathAIML = HttpContext.Current.Server.MapPath("~/File/AIML/");

        /// <summary>
        /// HttpContext.Current.Server.MapPath("~/File/Datasets_Training_Accent/")
        /// </summary>
        public static string PathAccent = HttpContext.Current.Server.MapPath("~/File/Datasets_Training_Accent/");

        public static string PathLogoSetting = HttpContext.Current.Server.MapPath("~/assets/images/logo/");
    }

    public class PathConfig
    {
        /// <summary>
        /// HttpContext.Current.Server.MapPath("~/Web.config")
        /// </summary>
        public static string PathWebConfig = HttpContext.Current.Server.MapPath("~/Web.config");

        /// <summary>
        /// HttpContext.Current.Server.MapPath("~/AppSettings.config")
        /// </summary>
        public static string PathAppConfig = HttpContext.Current.Server.MapPath("~/AppSettings.config");
    }
}
