using AIMLbot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace AIML.BOT.Utils
{
    public class Render
    {
        public static TagHtml RenderTagToHtml(string tagName, string outerTagContent, string innerTagContent)
        {
            TagHtml rs = new TagHtml();
            StringBuilder sb = new StringBuilder();
            outerTagContent = Regex.Replace(outerTagContent, "<text>", "");
            outerTagContent = Regex.Replace(outerTagContent, "</text>", "");
            switch (tagName)
            {
                case "button":
                    if (outerTagContent.Contains("<postback>"))
                    {
                        string dataPostback = new Regex("<postback>(.*)</postback>", RegexOptions.IgnoreCase).Match(outerTagContent).Groups[1].Value;
                        outerTagContent = outerTagContent.Replace("<button>", "<button class=\"{{lvbot_postback_button}}\" data-postback =\"" + dataPostback + "\">");
                        outerTagContent = Regex.Replace(outerTagContent, @"<postback>(.*?)</postback>", String.Empty);

                        rs.ButtonPostback = outerTagContent;
                    }
                    else if (outerTagContent.Contains("<url>"))
                    {
                        string dataUrl = new Regex("<url>(.*)</url>", RegexOptions.IgnoreCase).Match(outerTagContent).Groups[1].Value;
                        outerTagContent = outerTagContent.Replace("<button>", "<button class=\"{{lvbot_url_button}}\" data-url =\"" + dataUrl + "\">");
                        outerTagContent = Regex.Replace(outerTagContent, @"<url>(.*?)</url>", String.Empty);
                        rs.Body = outerTagContent;
                    }
                    else if (outerTagContent.Contains("<menu>"))
                    {
                        string dataMenu = new Regex("<menu>(.*)</menu>", RegexOptions.IgnoreCase).Match(outerTagContent).Groups[1].Value;
                        outerTagContent = outerTagContent.Replace("<button>", "<button class=\"{{lvbot_menu_button}}\" data-url =\"" + dataMenu + "\">");
                        outerTagContent = Regex.Replace(outerTagContent, @"<menu>(.*?)</menu>", String.Empty);
                        rs.Body = outerTagContent;
                    }
                    break;
                case "link":
                    string dataLink = new Regex("<url>(.*)</url>", RegexOptions.IgnoreCase).Match(outerTagContent).Groups[1].Value;
                    outerTagContent = outerTagContent.Replace("<link>", "<a class=\"{{lvbot_link}}\" target=\"_blank\" href=\"" + dataLink + "\">").Replace("</link>", "</a>");
                    outerTagContent = Regex.Replace(outerTagContent, @"<url>(.*?)</url>", String.Empty);
                    rs.Body = outerTagContent;
                    break;
                case "image":
                    string dataImage = new Regex("<image>(.*)</image>", RegexOptions.IgnoreCase).Match(outerTagContent).Groups[1].Value;
                    outerTagContent = Regex.Replace(outerTagContent, @"<image>(.*?)</image>", String.Empty);
                    outerTagContent = "<img class=\"{{lvbot_image}}\" src=\"" + dataImage + "\"/>";
                    rs.Body = outerTagContent;
                    break;
                case "title":
                    outerTagContent = outerTagContent.Replace("<title>", "<div class=\"{{lvbot_card_title}}\">")
                                                    .Replace("</title>", "</div>");
                    rs.Body = outerTagContent;
                    break;
                case "subtitle":
                    outerTagContent = outerTagContent.Replace("<subtitle>", "<div class=\"{{lvbot_card_subtitle}}\">")
                                .Replace("</subtitle>", "</div>");
                    rs.Body = outerTagContent;
                    break;
                default:
                    break;
            }

            //result = outerTagContent;

            if (tagName == "card")
            {
                XmlNode resultNode = AIMLTagHandler.getNode("<node>" + innerTagContent + "</node>");
                if (resultNode.HasChildNodes)
                {
                    string htmlCard = "";
                    foreach (XmlNode cNode in resultNode.ChildNodes)
                    {
                        htmlCard += RenderTagToHtml(cNode.Name, cNode.OuterXml, "");
                    }

                    rs.Body = htmlCard;
                }
            }
            if (tagName == "carousel")
            {
                XmlNode resultNode = AIMLTagHandler.getNode("<node>" + innerTagContent + "</node>");
                if (resultNode.HasChildNodes)
                {
                    string htmlCarousel = "";
                    foreach (XmlNode cNode in resultNode.ChildNodes)
                    {
                        htmlCarousel += RenderTagToHtml(cNode.Name, cNode.OuterXml, cNode.InnerXml);
                    }
                    rs.Body = htmlCarousel;
                }
            }
            return rs;
        }
    }
}
