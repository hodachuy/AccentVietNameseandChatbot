using BotProject.Model.Models;
using BotProject.Service;
using BotProject.Web.Infrastructure.Extensions;
using BotProject.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BotProject.Web.Controllers
{
    public class FacebookBotController : Controller
    {
        private IErrorService _errorService;
        public FacebookBotController(IErrorService errorService)
        {
            _errorService = errorService;
        }
        // GET: FacebookBot
        public ActionResult Receive()
        {
            var query = Request.QueryString;

            //_logWriter.WriteLine(Request.RawUrl);
            string verifytoken = Helper.ReadString("VerifyTokenWebHook");

            if (query["hub.mode"] == "subscribe" &&
                query["hub.verify_token"] == verifytoken)
            {
                //string type = Request.QueryString["type"];
                var retVal = query["hub.challenge"];
                return Json(int.Parse(retVal), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [ActionName("Receive")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ReceivePost(FacebookBotRequest data)
        {
            Task.Factory.StartNew(() =>
            {
                string accesstoken = Helper.ReadString("AccessToken");
                foreach (var entry in data.entry)
                {
                    foreach (var message in entry.messaging)
                    {
                        if (string.IsNullOrWhiteSpace(message?.message?.text))
                            continue;

                        string mesg = message.sender.id + " ";
                        LogError(mesg);
                        var msg = "You said: " + message.message.text;
                        var json = $@" {{recipient: {{  id: {message.sender.id}}},message: {{text: ""{msg}"" }}}}";
                        try
                        {
                            PostRaw("https://graph.facebook.com/v3.2/me/messages?access_token=" + accesstoken + "", json);

                        }catch(Exception ex)
                        {
                            LogError(ex.Message);
                        }

                    }
                }
            });

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        private string PostRaw(string url, string data)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "POST";
            using (var requestWriter = new StreamWriter(request.GetRequestStream()))
            {
                requestWriter.Write(data);
            }

            var response = (HttpWebResponse)request.GetResponse();
            if (response == null)
                throw new InvalidOperationException("GetResponse returns null");

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }

        private void LogError(string message)
        {
            try
            {
                Error error = new Error();
                error.CreatedDate = DateTime.Now;
                error.Message = message;
                _errorService.Create(error);
                _errorService.Save();
            }
            catch
            {
            }
        }
    }
}