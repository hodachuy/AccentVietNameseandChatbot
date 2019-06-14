using BotProject.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BotProject.Service
{
    public interface IHandleModuleServiceService
    {
        HandleResultBotViewModel HandleIsPhoneNumber(string number, int botID);
        HandleResultBotViewModel HandledIsEmail(string email, int botID);
        HandleResultBotViewModel HandledIsAge(string age, int botID);
        HandleResultBotViewModel HandleIsName(string name, int botID);
        HandleResultBotViewModel HandleIsModuleKnowledgeInfoPatient(string mdName, int botID);      
    }
    public class HandleModuleService : IHandleModuleServiceService
    {
        private const string CharacterPattern = @"^[A-Za-z]+";
        private const string NumberPattern = @"^\d+$";
        private const string PhonePattern = @"^(\+[0-9]{9})$";
        private const string EmailPattern =
        @"^\s*[\w\-\+_']+(\.[\w\-\+_']+)*\@[A-Za-z0-9]([\w\.-]*[A-Za-z0-9])?\.[A-Za-z][A-Za-z\.]*[A-Za-z]$";

        private IMdPhoneService _mdPhoneService;
        private IMdEmailService _mdEmailService;
        private IMdAgeService _mdAgeService;
        private IModuleKnowledegeService _mdKnowledegeService;

        public HandleModuleService(IMdPhoneService mdPhoneService,
                                    IMdEmailService mdEmailService,
                                    IMdAgeService mdAgeService,
                                    IModuleKnowledegeService mdKnowledegeService)
        {
            _mdPhoneService = mdPhoneService;
            _mdEmailService = mdEmailService;
            _mdAgeService = mdAgeService;
            _mdKnowledegeService = mdKnowledegeService;
        }
        public HandleResultBotViewModel HandleIsPhoneNumber(string number, int botID)
        {
            HandleResultBotViewModel rsHandle = new HandleResultBotViewModel();
            var mdPhoneDb = _mdPhoneService.GetByBotID(botID);
            rsHandle.Postback = mdPhoneDb.Payload;
            if (number.Contains(Common.CommonConstants.ModulePhone))
            {
                rsHandle.Status = false;
                rsHandle.Message = tempText(mdPhoneDb.MessageStart);// sau này phát triển thêm random nhiều message, tạo aiml random li(thẻ error phone)
                return rsHandle;
            }
            bool isNumber = ValidatePhoneNumber(number, true);
            if (!isNumber)
            {
                rsHandle.Status = false;
                rsHandle.Message = tempText(mdPhoneDb.MessageError);
                return rsHandle;
            }
            rsHandle.Status = true;
            rsHandle.Message = tempText(mdPhoneDb.MessageEnd);
            return rsHandle;
        }

        public HandleResultBotViewModel HandledIsEmail(string email, int botID)
        {
            HandleResultBotViewModel rsHandle = new HandleResultBotViewModel();
            var mdEmailDb = _mdEmailService.GetByBotID(botID);
            rsHandle.Postback = mdEmailDb.Payload;

            rsHandle.Status = true;
            if (email.Contains(Common.CommonConstants.ModuleEmail))
            {
                rsHandle.Status = false;
                rsHandle.Message = tempText(mdEmailDb.MessageStart);
                return rsHandle;
            }
            bool isEmail = Regex.Match(email, EmailPattern).Success;
            if (!isEmail)
            {
                rsHandle.Status = false;
                rsHandle.Message = tempText(mdEmailDb.MessageError);
                return rsHandle;
            }
            rsHandle.Status = true;
            rsHandle.Message = tempText(mdEmailDb.MessageEnd);// nếu call tới follow thẻ khác trả về postback id card
            return rsHandle;
        }

        public HandleResultBotViewModel HandledIsAge(string age, int botID)
        {
            HandleResultBotViewModel rsHandle = new HandleResultBotViewModel();
            var mdAgeDb = _mdAgeService.GetByBotID(botID);

            rsHandle.Postback = mdAgeDb.Payload;
            rsHandle.Status = true;
            if (age.Contains(Common.CommonConstants.ModuleAge))
            {
                rsHandle.Status = false;
                rsHandle.Message = tempText(mdAgeDb.MessageStart);
                return rsHandle;
            }
            bool isAge = Regex.Match(age, NumberPattern).Success;
            if (!isAge)
            {
                rsHandle.Status = false;
                rsHandle.Message = tempText(mdAgeDb.MessageError);
                return rsHandle;
            }
            else
            {
                if (Int32.Parse(age) < 5)
                {
                    rsHandle.Status = false;
                    rsHandle.Message = tempText("Bạn còn quá trẻ để chúng tôi đưa ra tư vấn.");
                    return rsHandle;
                }
                if (Int32.Parse(age) > 110)
                {
                    rsHandle.Status = false;
                    rsHandle.Message = tempText("Xin lỗi chúng tôi không thể đưa ra tư vấn hợp lý lúc này khi bạn đã lớn tuổi.");
                    return rsHandle;
                }
            }
            rsHandle.Status = true;
            rsHandle.Message = tempText(mdAgeDb.MessageEnd);// nếu call tới follow thẻ khác trả về postback id card
            return rsHandle;
        }

        public HandleResultBotViewModel HandleIsName(string name, int botID)
        {
            HandleResultBotViewModel rsHandle = new HandleResultBotViewModel();
            //rsHandle.Postback = postbackCard;
            if (name.Contains(Common.CommonConstants.ModuleName))
            {
                rsHandle.Status = false;
                rsHandle.Message = tempText("Bạn tên là gì?");// sau này phát triển thêm random nhiều message, tạo aiml random li(thẻ error phone)
                return rsHandle;
            }
            bool isName = Regex.Match(name, CharacterPattern).Success;
            if (!isName)
            {
                rsHandle.Status = false;
                rsHandle.Message = tempText("Hình như không giống tên cho lắm?");
                return rsHandle;
            }
            rsHandle.Status = true;
            rsHandle.Message = tempText("Cảm ơn bạn đã cho biết tên!");
            return rsHandle;
        }

        public HandleResultBotViewModel HandleIsModuleKnowledgeInfoPatient(string mdName, int botID)
        {
            HandleResultBotViewModel rsHandle = new HandleResultBotViewModel();
            string mdInfoPatientID = mdName.Replace("postback_module_med_get_info_patient_", "");
            var mdGetInfoPatientDb = _mdKnowledegeService.GetByMdMedInfoPatientID(Int32.Parse(mdInfoPatientID));
            if(mdGetInfoPatientDb != null)
            {
                if (!String.IsNullOrEmpty(mdGetInfoPatientDb.OptionText))
                {
                    var arrOpt = mdGetInfoPatientDb.OptionText.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                    rsHandle.Message = TemplateOptionBot(arrOpt, mdGetInfoPatientDb.Title, mdGetInfoPatientDb.Payload);
                }
            }
          
            return rsHandle;
        }

        private static string TemplateOptionBot(string[] arrOpt, string title, string postback)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div class=\"_4xkn clearfix\">");
            sb.AppendLine (     "<div class=\"profilePictureColumn\" style=\"bottom: 0px;\">");
            sb.AppendLine("<div class=\"_4cqr\">");
            sb.AppendLine("<img class=\"profilePicture img\" src=\"~/assets/images/user_bot.jpg\"/>");
            sb.AppendLine("<div class=\"clearfix\"></div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"messages\">");
            sb.AppendLine("<div class=\"_21c3\">");
            sb.AppendLine("<div class=\"clearfix _2a0-\">");
            sb.AppendLine("<div class=\"_4xko _4xkr\" tabindex=\"0\" role=\"button\" style=\"background-color: rgb(241, 240, 240);\">");
            sb.AppendLine("<span>");
            sb.AppendLine("<span>"+title+"</span>");
            sb.AppendLine("</span>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"_4xko _4xkr\" tabindex=\"0\" role=\"button\" style=\"background-color: rgb(241, 240, 240);width:100%\">");
            sb.AppendLine("<ul>");
            foreach(var item in arrOpt)
            {
                sb.AppendLine("<li><input type = \"checkbox\"/> "+ item + "</li>");
            }
            sb.AppendLine("</ul>");
            sb.AppendLine("</div>");
            if (!String.IsNullOrEmpty(postback))
            {
               sb.AppendLine("<div class=\"_6ir5\">");
               sb.AppendLine("<div class=\"_4bqf _6ir3\">");
               sb.AppendLine("<a class=\"_6ir4 _6ir4_menu\" data-postback =\"" + postback + "\" href=\"#\" style=\"color: rgb(234, 82, 105);\">Tiếp tục</a>");
               sb.AppendLine("</div>");
               sb.AppendLine("</div>");
            }

            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"_4xkn clearfix\">");

            return sb.ToString();
        }


        /// <summary>
        /// Template text UI BOT
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Validate Phone
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="IsRequired"></param>
        /// <returns></returns>
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
    }
}
