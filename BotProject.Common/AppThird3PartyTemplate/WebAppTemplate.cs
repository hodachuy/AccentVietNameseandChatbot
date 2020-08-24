using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BotProject.Common.AppThird3PartyTemplate
{
	public class WebAppTemplate
	{
		public static string GetMessageTemplateText(string text)
		{
			text = Regex.Replace(text, "  ", "<br/>");
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
			sb.AppendLine("		<div class=\"_4xko _4xkr _tmpB\" tabindex=\"0\" role=\"button\" style=\"background-color:rgb(241, 240, 240); font-family: Segoe UI Light\">");
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
	}
}
