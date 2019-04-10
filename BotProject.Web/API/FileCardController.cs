using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BotProject.Service;
using System.Web;
using System.Web.Script.Serialization;
using System.IO;
using BotProject.Model.Models;
using BotProject.Common;
using System.Configuration;
using System.Text.RegularExpressions;
using BotProject.Web.Models;
using System.Web.Http.Cors;

namespace BotProject.Web.API
{
    [RoutePrefix("api/file")]
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FileCardController : ApiControllerBase
    {
        private IFileCardService _FileCardService;
        public FileCardController(IErrorService errorService, IFileCardService FileCardService) : base(errorService)
        {
            _FileCardService = FileCardService;
        }   

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var form = HttpContext.Current.Request.Unvalidated.Form["botId"];
                var botId = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 }.Deserialize<int>(form);
                var file = HttpContext.Current.Request.Files[0];
                if (file.ContentLength > 0)
                {
                    Guid id = Guid.NewGuid();
                    string extentsion = new FileInfo(file.FileName).Extension.ToLower();
                    string fileName = id + "-" + new FileInfo(file.FileName).Name;

                    file.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath("~/File/Images/")
                    + CommonConstants.PathImage + "/" + fileName));

                    var FileCard = new FileCard()
                    {
                        Name = fileName,
                        Type = "image",
                        Url = "File/Images/" + CommonConstants.PathImage + "/" + fileName,
                        BotID = botId,
                    };
                    var FileCardReturn = _FileCardService.Add(ref FileCard);
                    response = request.CreateResponse(HttpStatusCode.OK, FileCardReturn);
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "No Implement");
                }

                return response;
            });
        }


        [Route("delete")]
        [HttpPost]
        public HttpResponseMessage Delete(HttpRequestMessage request, FileCardImage img)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var fileFileCard = _FileCardService.Delete(img.FileImageID);
                _FileCardService.Save();
                string domain = ConfigurationManager.AppSettings["Domain"];
                string fileName = Regex.Replace(img.FileImagePath, domain + "File/Images/Card/", "");
                //string pathImgServer = ConfigurationManager.AppSettings["ImagePath"];

                string pathImgServer = Path.Combine(PathServer.PathImage + CommonConstants.PathImage);

                string[] Files = Directory.GetFiles(pathImgServer);
                foreach (string file in Files)
                {
                    if (file.ToUpper().Contains(fileName.ToUpper()))
                    {
                        File.Delete(file);
                    }
                }
                string statusCode = "1";
                response = request.CreateResponse(HttpStatusCode.Created, statusCode);
                return response;
            });
        }

        //[Route("test")]
        //[HttpPost]
        //public HttpResponseMessage Test(HttpRequestMessage request, FileCardImage file)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        var form = HttpContext.Current.Request.Form["botId"];
        //        var botId = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 }.Deserialize<int>(form);

        //        response = request.CreateResponse(HttpStatusCode.Created, "OK");
        //        return response;
        //    });
        //}


        public class FileCardImage
        {
            public int FileImageID { set; get; }
            public string FileImagePath { set; get; }
        }
    }
}
