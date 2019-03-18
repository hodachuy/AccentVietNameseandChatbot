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

namespace BotProject.Web.API
{
    [RoutePrefix("api/image")]
    public class ImageController : ApiControllerBase
    {
        private IImageService _imageService;
        public ImageController(IErrorService errorService, IImageService imageService) : base(errorService)
        {
            _imageService = imageService;
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

                    var image = new Image()
                    {
                        Name = fileName,
                        Url = "File/Images/" + CommonConstants.PathImage + "/" + fileName,
                        BotID = botId,
                    };
                    var imageReturn = _imageService.Add(ref image);
                    response = request.CreateResponse(HttpStatusCode.OK, imageReturn);
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
        public HttpResponseMessage Delete(HttpRequestMessage request, ImageDelete img)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var fileImage = _imageService.Delete(img.ImageID);
                _imageService.Save();
                string domain = ConfigurationManager.AppSettings["Domain"];
                string fileName = Regex.Replace(img.ImagePath, domain + "File/Images/Card/", "");
                string pathImgServer = ConfigurationManager.AppSettings["ImagePath"];
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

        public class ImageDelete {
            public int ImageID { set; get; }
            public string ImagePath { set; get; }
        }
    }
}
