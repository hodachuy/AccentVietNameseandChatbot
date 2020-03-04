using BotProject.Web.Infrastructure.Log4Net;
using BotProject.Web.Mappings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace BotProject.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            AutoMapperConfiguration.Configure();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Main();
        }
        protected void Application_PostAuthorizeRequest()
        {
            HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }

        //System.Timers.Timer webKeepAlive = new System.Timers.Timer();
        //Int64 counter = 0;
        //void Main()
        //{
        //    webKeepAlive.Interval = 5000;
        //    webKeepAlive.Elapsed += WebKeepAlive_Elapsed;
        //    webKeepAlive.Start();
        //}


        //private void WebKeepAlive_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    webKeepAlive.Stop();
        //    try
        //    {
        //        // ONLY the first time it retrieves the content it will print the string
        //        String finalHtml = GetPreHitSiteAndAccentContent();
        //        if (counter < 1)
        //        {
        //            Console.WriteLine(finalHtml);
        //        }
        //        counter++;
        //    }
        //    finally
        //    {
        //        webKeepAlive.Interval = 480000; // every 8 minutes
        //        webKeepAlive.Start();
        //    }
        //}

        //public String GetPreHitSiteAndAccentContent()
        //{
        //    try
        //    {

        //        ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(
        //            delegate (object sender2, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        //            {
        //                return true;
        //            });

        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;

        //        String URL = "http://113.161.108.36:80/tiengviet/apiv1/PreHitAccentVN";
        //        WebRequest request = WebRequest.Create(URL);
        //        WebResponse response = request.GetResponse();
        //        Stream data = response.GetResponseStream();
        //        string html = String.Empty;
        //        using (StreamReader sr = new StreamReader(data))
        //        {
        //            html = sr.ReadToEnd();
        //        }
        //        if (html.Length > 0)
        //        {
        //            BotLog.Info(String.Format("GetPreHitSiteAndAccentContent : success"));
        //        }
        //        return "0";
        //    }
        //    catch (Exception ex)
        //    {
        //        BotLog.Info("GetPreHitSiteAndAccentContent: " + ex.Message);
        //        return "fail";
        //    }
        //}
    }
}
