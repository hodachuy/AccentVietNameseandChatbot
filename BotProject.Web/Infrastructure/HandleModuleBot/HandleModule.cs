using BotProject.Web.Infrastructure.HandleModuleBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace BotProject.Web.Infrastructure.HandleModuleBot
{
    public class HandleModule
    {
        private const string CharacterPattern = @"^[A-Za-z]+";
        private const string NumberPattern = @"^\d$";
        private const string PhonePattern = @"^(\+[0-9]{9})$";
        private const string EmailPattern =
        @"^\s*[\w\-\+_']+(\.[\w\-\+_']+)*\@[A-Za-z0-9]([\w\.-]*[A-Za-z0-9])?\.[A-Za-z][A-Za-z\.]*[A-Za-z]$";

        private static string tempText(string text)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div class=\"_4xkn clearfix\">");
            sb.AppendLine("     <div class=\"profilePictureColumn\" style=\"bottom: 0px;\">");
            sb.AppendLine("         <div class=\"_4cqr\">");
            sb.AppendLine("             <img class=\"profilePicture img\" src=\"{{image_logo}}\" alt=\"\">");
            sb.AppendLine("             <div class=\"clearfix\"></div>");
            sb.AppendLine("         </div>");
            sb.AppendLine("     </div>");
            sb.AppendLine("     <div class=\"messages\">");
            sb.AppendLine("         <div class=\"_21c3\">");
            sb.AppendLine("             <div class=\"clearfix _2a0-\">");

            sb.AppendLine("<div class=\"_4xko _4xkr\" tabindex=\"0\" role=\"button\" style=\"background-color:rgb(241, 240, 240); \">");
            sb.AppendLine("     <span>");
            sb.AppendLine("         <span>" + text + "</span>");
            sb.AppendLine("     </span>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            return sb.ToString();
        }

        private static bool ValidatePhoneNumber(string phone, bool IsRequired)
        {
            if (string.IsNullOrEmpty(phone) & !IsRequired)
                return true;

            if (string.IsNullOrEmpty(phone) & IsRequired)
                return false;

            var cleaned = RemoveNonNumeric(phone);
            if (IsRequired)
            {
                if (cleaned.Length == 10)
                    return true;
                else
                    return false;
            }
            else
            {
                if (cleaned.Length == 0)
                    return true;
                else if (cleaned.Length > 0 & cleaned.Length < 10)
                    return false;
                else if (cleaned.Length == 10)
                    return true;
                else
                    return false; // should never get here
            }
        }

        /// <summary>
        /// Removes all non numeric characters from a string
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        private static string RemoveNonNumeric(string phone)
        {
            return Regex.Replace(phone, @"[^0-9]+", "");
        }

        public static HandleResult HandleIsPhoneNumber(string number, string postbackCard)
        {
            HandleResult rsHandle = new HandleResult();
            rsHandle.Postback = postbackCard;
            if (number.Contains(Common.CommonConstants.ModulePhone))
            {
                rsHandle.Status = false;
                rsHandle.Message = tempText("Vui lòng nhập số điện thoại của bạn.") ;// sau này phát triển thêm random nhiều message, tạo aiml random li(thẻ error phone)
                return rsHandle;
            }
            bool isNumber = ValidatePhoneNumber(number, true);
            if (!isNumber)
            {
                rsHandle.Status = false;
                rsHandle.Message = tempText("Số điện thoại không hợp lệ.");
                return rsHandle;
            }
            rsHandle.Status = true;
            rsHandle.Message = tempText("Cảm ơn bạn, chúng tôi sẽ liên hệ tới bạn!");
            return rsHandle;
        }

        public static HandleResult HandledIsEmail(string email, string postbackCard)
        {
            HandleResult rsHandle = new HandleResult();
            rsHandle.Postback = postbackCard;
            rsHandle.Status = true;
            if (!String.IsNullOrEmpty(email))
            {
                rsHandle.Status = false;
                rsHandle.Message = "Vui lòng nhập địa chỉ email của bạn.";
                return rsHandle;
            }
            bool isEmail = Regex.Match(email, EmailPattern).Success;
            if (!isEmail)
            {
                rsHandle.Status = false;
                rsHandle.Message = "Địa chỉ email không hợp lệ.";
                return rsHandle;
            }
            rsHandle.Status = true;
            rsHandle.Message = "Cảm ơn bạn, chúng đã tiếp nhận địa chỉ thành công!";// nếu call tới follow thẻ khác trả về postback id card
            return rsHandle;
        }

        public static HandleResult HandledIsAge(string age, string postbackCard)
        {
            HandleResult rsHandle = new HandleResult();
            rsHandle.Postback = postbackCard;
            rsHandle.Status = true;
            if (!String.IsNullOrEmpty(age))
            {
                rsHandle.Status = false;
                rsHandle.Message = "Vui lòng nhập độ tuổi của bạn.";
                return rsHandle;
            }
            bool isAge = Regex.Match(age, NumberPattern).Success;
            if (!isAge)
            {
                rsHandle.Status = false;
                rsHandle.Message = "Tôi không nghĩ đó là số tuổi, bạn vui lòng nhập vào chữ số.";
                return rsHandle;
            }
            else
            {
                if(Int32.Parse(age) < 6)
                {
                    rsHandle.Status = false;
                    rsHandle.Message = "Bạn còn quá trẻ để chúng tôi đưa ra tư vấn.";
                    return rsHandle;
                }
                if (Int32.Parse(age) < 120)
                {
                    rsHandle.Status = false;
                    rsHandle.Message = "Xin lỗi chúng tôi không thể đưa ra tư vấn hợp lý lúc này khi bạn đã lớn tuổi.";
                    return rsHandle;
                }
            }
            rsHandle.Status = true;
            rsHandle.Message = "Cảm ơn bạn, chúng đã tiếp nhận địa chỉ thành công!";// nếu call tới follow thẻ khác trả về postback id card
            return rsHandle;
        }

        public static HandleResult HandleIsName(string name, string postbackCard)
        {
            HandleResult rsHandle = new HandleResult();
            rsHandle.Postback = postbackCard;
            if (!String.IsNullOrEmpty(name))
            {
                rsHandle.Status = false;
                rsHandle.Message = "Vui lòng nhập tên của bạn";// sau này phát triển thêm random nhiều message, tạo aiml random li(thẻ error phone)
                return rsHandle;
            }
            bool isName = Regex.Match(name, CharacterPattern).Success;
            if (!isName)
            {
                rsHandle.Status = false;
                rsHandle.Message = "Số điện thoại không hợp lệ.";
                return rsHandle;
            }
            rsHandle.Status = true;
            rsHandle.Message = "Cảm ơn bạn, chúng tôi sẽ liên hệ tới bạn!";
            return rsHandle;
        }
    }

    public class HandleResult
    {
        public string Message { set; get; }
        public bool Status { set; get; }
        public string Postback { set; get; }
    }
}