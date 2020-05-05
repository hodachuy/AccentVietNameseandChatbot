using BotProject.Common;
using BotProject.Common.DigiproService.Digipro;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using static BotProject.Common.DigiproService.Digipro.DigiproService;

namespace BotProject.Web.API
{
    [RoutePrefix("api/dell")]
    public class DellController : ApiController
    {
        public DellController()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
        }
        // GET: Dell
        [HttpGet]
        public IHttpActionResult Index(string id)
        {
            return Ok();
        }

        [Route("test2")]
        [HttpGet]
        public IHttpActionResult TestSenderFbUser(string senderId, string phoneNumber)
        {
            ReturnCode rt = new ReturnCode();
            string sender = senderId;
            string first_name = "Huy";
            string last_name = "Ho";
            string avatar = "https://platform-lookaside.fbsbx.com/platform/profilepic/?psid=2666433486706739&width=1024&ext=1591152161&hash=AeR0YIW52Gw55qxH";
            string phone = phoneNumber;
            rt = DigiproService.SendFbUserToService(sender,first_name,last_name,avatar, phone);
            return Ok();
        }


        [HttpPost]
        [Route("random")]
        public IHttpActionResult RandomT(string id)
        {
            var newValue = new Random().Next(1, 7);
            object context;
            if (Request.Properties.TryGetValue("MS_HttpContext", out context))
            {
                var httpContext = context as HttpContextBase;
                if (httpContext != null && httpContext.Session != null)
                {
                    var lastValue = httpContext.Session["LastValue"] as int?;
                    httpContext.Session["LastValue"] = newValue;

                    //var user = httpContext.Session["User"] as ApplicationUser;
                    NumberResult num = new NumberResult
                    {
                        NewValue = newValue,
                        LastValue = lastValue ?? 0
                    };
                    return Ok(num );
                }
            }
            return Ok();
        }

        public class NumberResult
        {
            public int NewValue { set; get; }
            public int LastValue { set; get; }
        }


        //[Route("getdgpservice")]
        //[HttpGet]
        //public IHttpActionResult GetDgpService(string id)
        //{
        //    var Result = DigiproService.GetDetailServiceWarrantyDigipro(id);
        //    return Ok(Result);
        //}
        [Route("test")]
        [HttpGet]
        public async Task<IHttpActionResult> Test()
        {
            string result = null;
            string flacName = @"D:\banmai.0.eb32b236630ec00f67fd0f53ace90353.mp3";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.openfpt.vn/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("api_key", "74ea791c21504fc5b6fd4978cce6d6e0");
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Taco2) Gecko/20100101");


                byte[] byteArray = File.ReadAllBytes(flacName);

                ByteArrayContent bytesContent = new ByteArrayContent(byteArray);


                var response = await client.PostAsync("fsr", bytesContent);
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    dynamic stuff = JsonConvert.DeserializeObject(result);
                    string status = stuff.status;
                    if(status == "0")
                    {
                        string utterance = stuff.hypotheses[1].utterance;
                    }
                }
                return Ok(result);
            }
        }


        [Route("getwarranty")]
        [HttpGet]
        public IHttpActionResult Warranty(string id)
        {
            var Result = DellServices.GetAssetHeader(id);
            return Ok(Result);
        }

        [HttpPost]
        public IHttpActionResult Warrantys(string id)
        {
            var Result = DellServices.GetAssetHeader(id);
            //return Request.CreateResponse(HttpStatusCode.OK, Result);
            return Ok(Result);
        }
    }
}
