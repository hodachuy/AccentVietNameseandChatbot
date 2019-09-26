using BotProject.Common.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Common.AppThird3PartyTemplate
{
    public class FacebookTemplate
    {
        public static JObject GetMessageTemplateText(string text, string sender)
        {
            return JObject.FromObject(
                 new
                 {
                     recipient = new { id = sender },
                     message = new { text = text },
                 });
        }
        public static JObject GetMessageTemplateImage(string urlImage, string sender)
        {
            return JObject.FromObject(
                 new
                 {
                     recipient = new { id = sender },
                     message = new {
                         attachment = new
                         {
                             type = "image",
                             payload = new
                             {
                                 url = urlImage,
                                 is_reusable = true
                             }
                         }
                     },
                 });
        }

        public static JObject GetMessageTemplateTextAndQuickReply(string text, string sender, string patternQuickReply, string titleQuickReply)
        {
            if(!String.IsNullOrEmpty(patternQuickReply) && !String.IsNullOrEmpty(titleQuickReply))
            {
                return JObject.FromObject(
                     new
                     {
                         recipient = new { id = sender },
                         message = new
                         {
                             text = text,
                             quick_replies = new[]
                             {
                                 new
                                 {
                                      content_type = "text",
                                      title = titleQuickReply,
                                      payload = patternQuickReply
                                 }
                             }
                         },
                     });
            }
            return JObject.FromObject(
                     new
                     {
                         recipient = new { id = sender },
                         message = new
                         {
                             text = text
                         },
                     });

        }

        public static Object GetMessageTemplateGenericByList(string sender, List<SearchNlpQnAViewModel> lstSearchNLP)
        {
            return JObject.FromObject(
              new
              {
                  recipient = new { id = sender },
                  message = new
                  {
                      attachment = new
                      {
                          type = "template",
                          payload = new
                          {
                              template_type = "generic",
                              elements = from q in lstSearchNLP
                                         select new
                                         {
                                             title = q.question.Substring(0,60)+"...",
                                             item_url = "",
                                             image_url = ConfigHelper.ReadString("Domain")+ "assets/images/faq.png",
                                             subtitle = "FAQs",
                                             buttons = new[]
                                             {
                                                             new
                                                                {
                                                                   type = "web_url",
                                                                   url = ConfigHelper.ReadString("Domain")+"home/faq/"+q.id+"",
                                                                   title = "Xem chi tiết"
                                                                },
                                                        }
                                         }
                          }
                      }
                  },
              });
        }
    }
}