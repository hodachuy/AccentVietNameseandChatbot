using AutoMapper;
using BotProject.Common.ViewModels;
using BotProject.Model.Models;
using BotProject.Service;
using BotProject.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BotProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private IModuleSearchEngineService _moduleSearchEngineService;
        private IMedicalSymptomService _medSymptomsService;
        public HomeController(IModuleSearchEngineService moduleSearchEngineService,
                              IMedicalSymptomService medSymptomsService)
        {
            _moduleSearchEngineService = moduleSearchEngineService;
            _medSymptomsService = medSymptomsService;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FAQ(int id)
        {
            string filter = "q.ID = " + id;
            MdQnAViewModel qna = new MdQnAViewModel();
            qna = _moduleSearchEngineService.GetListMdQnA(filter, "", 1, 1, null).ToList().FirstOrDefault();
            return View(qna);
        }
        public ActionResult FaqMedSymptoms(int? id)
        {
            id = id ?? 0;
            if(id == 0)
            {
                return View();
            }
            var medSymptoms = _medSymptomsService.GetById(id.GetValueOrDefault());
            var medSymptomsVm = Mapper.Map<MedicalSymptom, MedicalSymptomViewModel>(medSymptoms);
            return View(medSymptomsVm);
        }
        public ActionResult FaqMedSymptomsFile(int? id)
        {
            id = id ?? 0;
            if (id == 0)
            {
                return View();
            }
            var medSymptoms = _medSymptomsService.GetById(id.GetValueOrDefault());
            string fileName = medSymptoms.Advice;
            string pathFileMedSymptoms = Common.PathServer.PathMedicalSymptoms + fileName;
            Response.AddHeader("Content-Disposition", new System.Net.Mime.ContentDisposition { Inline = true, FileName = "index.htm" }.ToString());
            return File(pathFileMedSymptoms, "text/plain");
        }

        /// <summary>
        /// Dịch vụ công
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PublicService()
        {
            return View();
        }
    }
}