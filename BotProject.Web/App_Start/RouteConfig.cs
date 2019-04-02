using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BotProject.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "QnA",
                url: "bot/qna/{alias}/{id}",
                defaults: new { controller = "Bot", action = "QnA", id = UrlParameter.Optional },
                  namespaces: new string[] { "BotProject.Web.Controllers" }
            );

            routes.MapRoute(
               name: "Bot Setting",
               url: "bot/setting/{alias}/{id}",
               defaults: new { controller = "Bot", action = "Setting", id = UrlParameter.Optional },
                 namespaces: new string[] { "BotProject.Web.Controllers" }
           );

            routes.MapRoute(
             name: "TagCategory",
             url: "bot/{alias}/{id}/cardcategory",
             defaults: new { controller = "Bot", action = "CardCategory", id = UrlParameter.Optional },
               namespaces: new string[] { "BotProject.Web.Controllers" }
             );

            routes.MapRoute(
             name: "AIML",
             url: "bot/{alias}/{id}/aiml",
             defaults: new { controller = "Bot", action = "AIML", id = UrlParameter.Optional },
               namespaces: new string[] { "BotProject.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
