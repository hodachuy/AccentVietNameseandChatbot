using BotProject.Common.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotProject.Common.AppThird3PartyTemplate
{
    public class ZaloTemplate
    {
        public static JObject GetMessageTemplateText(string text, string sender)
        {
            return JObject.FromObject(
                 new
                 {
                     recipient = new { user_id = sender },
                     message = new { text = text },
                 });
        }
        public static JObject GetMessageTemplateImage(string urlImage, string sender)
        {
            return JObject.FromObject(
                 new
                 {
                     recipient = new { id = sender },
                     message = new
                     {
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
            if (!String.IsNullOrEmpty(patternQuickReply) && !String.IsNullOrEmpty(titleQuickReply))
            {
                return JObject.FromObject(
                     new
                     {
                         recipient = new { user_id = sender },
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
                         recipient = new { user_id = sender },
                         message = new
                         {
                             text = text
                         },
                     });

        }

        public static Object GetMessageTemplateGenericByList(string sender, List<SearchNlpQnAViewModel> lstSearchNLP)
        {
            JObject jb = new JObject();
            jb.Add("recipient", sender);


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
                              elements = new[]
                              {

                                    new
                                    {
                                        title = "Trung tâm chăm sóc khách hàng Digipro.vn",
                                        item_url = "http://digipro.vn/",
                                        image_url = "https://bot.surelrn.vn/File/Images/Card/134a16f1-7c56-4eca-a61b-1bbe5a23a42b-Logo_DGP_EN_1600-800_5.png",
                                        subtitle = "Tư vấn bảo hành, sửa chữa máy tính",
                                        buttons = new []
                                        {
                                            new
                                            {
                                                  type = "postback",
                                                  title = "💻 Bảo hành dòng máy Dell",
                                                  payload = "postback_card_6070"
                                            },
                                            new
                                            {
                                                  type = "postback",
                                                  title = "🔍 Tra cứu máy bảo hành",
                                                  payload = "postback_card_6071"
                                            },
                                            new
                                            {
                                                  type = "postback",
                                                  title =  "📞 Thông tin hỗ trợ",
                                                  payload = "postback_card_6072"
                                            },
                                        }
                                    }
                              }
                          }
                      }
                  },
              });
        }
    }
}