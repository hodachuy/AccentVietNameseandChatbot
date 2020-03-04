﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using static BotProject.Web.WebApiConfig;

namespace BotProject.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "DefaultApi2",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            ).RouteHandler = new SessionHttpControllerRouteHandler();

            routes.MapRoute(
              name: "Faq",
              url: "home/faq/{id}",
              defaults: new { controller = "Home", action = "FAQ", id = UrlParameter.Optional },
                namespaces: new string[] { "BotProject.Web.Controllers" }
            );

            routes.MapRoute(
                name: "QnA",
                url: "bot/qna/{alias}/{formQnAId}/{botId}",
                defaults: new { controller = "Bot", action = "QnA", formQnAId = UrlParameter.Optional, botId = UrlParameter.Optional },
                  namespaces: new string[] { "BotProject.Web.Controllers" }
            );

            routes.MapRoute(
             name: "ModuleBot",
             url: "bot/{alias}/{id}/module",
             defaults: new { controller = "Bot", action = "Module", id = UrlParameter.Optional },
               namespaces: new string[] { "BotProject.Web.Controllers" }
             );

            routes.MapRoute(
               name: "Bot Setting",
               url: "bot/setting/{alias}/{id}",
               defaults: new { controller = "Bot", action = "Setting", id = UrlParameter.Optional },
                 namespaces: new string[] { "BotProject.Web.Controllers" }
           );

            routes.MapRoute(
            name: "Bot Search Engine",
            url: "bot/searchengine/{alias}/{botId}",
            defaults: new { controller = "Bot", action = "BotSearchEngine", botId = UrlParameter.Optional },
              namespaces: new string[] { "BotProject.Web.Controllers" }
            );

            routes.MapRoute(
            name: "Bot Medical Symptoms",
            url: "bot/medicalsymptoms/{alias}/{botId}",
            defaults: new { controller = "Bot", action = "BotMedicalSymptoms", botId = UrlParameter.Optional },
              namespaces: new string[] { "BotProject.Web.Controllers" }
            );

            routes.MapRoute(
            name: "Bot History",
            url: "bot/history/{alias}/{botId}",
            defaults: new { controller = "Bot", action = "BotHistory", botId = UrlParameter.Optional },
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
