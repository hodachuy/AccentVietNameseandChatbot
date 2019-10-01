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

        public const string SessionUserBot = "UserBot";

		public const string ModuleAdminContact = "postback_module_admin_contact";
		public const string ModuleEngineerName = "postback_module_engineer_name";
		public const string ModulePhone = "postback_module_phone";
        public const string ModuleVoucher = "postback_module_voucher";
        public const string ModuleAge = "postback_module_age";
        public const string ModuleEmail = "postback_module_email";
        public const string ModuleName = "postback_module_name";
        public const string ModuleSearchAPI = "postback_module_api_search";

        public const string MethodeHTTP_POST = "POST";
        public const string MethodeHTTP_GET = "GET";

        public const string UserSay_IsStartDefault = "StartDefault";
        public const string UserSay_IsStartFirst = "StartFirst";
        public const string UserSay_IsStartLast = "StartLast";
        public const string UserSay_IsStartDouble = "StartDouble";

        public const string MdSearch_Luat = "luat";
        public const string MdSearch_Dell = "dell";
        public const string MdSearch_Digipro = "digipro";

        //DỊCH VỤ NGOÀI SỬA CHỮA
        public const string MdSearch_Digipro_OOW = "digipro-oow";

        public const string MdSearch_Yte = "y-te";


        public const string POSTBACK_MODULE = "postback_module";


        public const string TYPE_FACEBOOK = "facebook";
        public const string TYPE_ZALO = "zalo";
    }

    public class ZaloConstans
    {
        public const string EVENT_USER_SEND_TEXT = "user_send_text";
    }


    public class PathServer
    {
        /// <summary>
        /// HttpContext.Current.Server.MapPath("~/File/Images/");
        /// System.Web.HttpContext.Current.Server.MapPath
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

        public static string PathNLR = HttpContext.Current.Server.MapPath("~/File/NLR/");


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

    public class MessageBot
    {
        public const string BOT_HISTORY_HANDLE_001 = "Nhấn nút";
        public const string BOT_HISTORY_HANDLE_002 = "Không tìm thấy trong kịch bản";
        public const string BOT_HISTORY_HANDLE_003 = "Bot hiểu trả lời theo kịch bản";
        public const string BOT_HISTORY_HANDLE_004 = "Điền thông tin";
        public const string BOT_HISTORY_HANDLE_005 = "Gọi NLP API từ bên ngoài";
        public const string BOT_HISTORY_HANDLE_006 = "Gọi Search NLP trong cấu hình";
		public const string BOT_HISTORY_HANDLE_007 = "Nhấn Voucher";
        public const string BOT_HISTORY_HANDLE_008 = "Bot không hiểu và Search NLP không tìm thấy";
    }
}
