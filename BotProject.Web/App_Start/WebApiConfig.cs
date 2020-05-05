using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;

namespace BotProject.Web
{
    public static class WebApiConfig
    {		
		public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            // config show value defaul.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
            new DefaultContractResolver { IgnoreSerializableAttribute = true };

            // Config admin web api with token remove default cookie.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            //handle session

            var httpControllerRouteHandler = typeof(HttpControllerRouteHandler).GetField("_instance",
                                            System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

            if (httpControllerRouteHandler != null)
            {
                httpControllerRouteHandler.SetValue(null,
                                                    new Lazy<HttpControllerRouteHandler>(() => new SessionHttpControllerRouteHandler(), true));
            }


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //RouteTable.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //).RouteHandler = new SessionStateRouteHandler();
        }

        //public class SessionableControllerHandler : HttpControllerHandler, IRequiresSessionState
        //{
        //    public SessionableControllerHandler(RouteData routeData)
        //        : base(routeData)
        //    { }
        //}

        //public class SessionStateRouteHandler : IRouteHandler
        //{
        //    IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        //    {
        //        return new SessionableControllerHandler(requestContext.RouteData);
        //    }
        //}
        public class SessionControllerHandler : HttpControllerHandler, IRequiresSessionState
        {
            public SessionControllerHandler(RouteData routeData)
                : base(routeData)
            { }
        }

        public class SessionHttpControllerRouteHandler : HttpControllerRouteHandler
        {
            protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
            {
                return new SessionControllerHandler(requestContext.RouteData);
            }
        }
    }
}
