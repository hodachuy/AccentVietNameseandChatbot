using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BotProject.Service;
using BotProject.Model.Models;
using BotProject.Web.Models;
using BotProject.Common;

namespace BotProject.Web.API
{
    [RoutePrefix("api/medical/symptoms")]
    public class MedicalSymptomsController : ApiControllerBase
    {
        private IMedicalSymptomService _medSymptomsService;
        private ApiQnaNLRService _apiNLR;
        public MedicalSymptomsController(IErrorService errorService,
                                        IMedicalSymptomService medSymptomsService) : base(errorService)
        {
            _medSymptomsService = medSymptomsService;
            _apiNLR = new ApiQnaNLRService();
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetListMedicalSymptoms(HttpRequestMessage request, int page, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                int totalRow = 0;
                var lstSymptoms = _medSymptomsService.GetAll();
                if(lstSymptoms.Count() != 0)
                {
                    totalRow = lstSymptoms.Count();
                }
                var query = lstSymptoms.OrderByDescending(x=>x.ID).Skip((page - 1) * pageSize).Take(pageSize);
                var paginationSet = new PaginationSet<MedicalSymptom>()
                {
                    Items = query,
                    Page = page,
                    TotalCount = totalRow,
                    MaxPage = pageSize,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("getbyid")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int Id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                MedicalSymptom med = new MedicalSymptom();
                med = _medSymptomsService.GetById(Id);            
                response = request.CreateResponse(HttpStatusCode.OK, med);
                return response;
            });
        }


        [Route("createUpdateSymptoms")]
        [HttpPost]
        public HttpResponseMessage CreateUpdateSymptoms(HttpRequestMessage request, MedicalSymptomViewModel medSymptomsVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadGateway, ModelState);
                    return response;
                }
                string tagHtmlExceptRemove = "br";
                MedicalSymptom medSymptomsDb = new MedicalSymptom();
                if(medSymptomsVm.ID != 0)
                {
                    medSymptomsDb = _medSymptomsService.GetById(medSymptomsVm.ID);
                    medSymptomsDb.ID = medSymptomsVm.ID;
                    medSymptomsDb.Name = medSymptomsVm.Name;
                    medSymptomsDb.Description = CommonSer.RemoveAllTagHTML(medSymptomsVm.Description, tagHtmlExceptRemove);
                    medSymptomsDb.Cause = CommonSer.RemoveAllTagHTML(medSymptomsVm.Cause, tagHtmlExceptRemove);
                    medSymptomsDb.Treament = CommonSer.RemoveAllTagHTML(medSymptomsVm.Treament, tagHtmlExceptRemove);
                    medSymptomsDb.Advice = CommonSer.RemoveAllTagHTML(medSymptomsVm.Advice, tagHtmlExceptRemove);
                    medSymptomsDb.Predict = CommonSer.RemoveAllTagHTML(medSymptomsVm.Predict, tagHtmlExceptRemove);
                    medSymptomsDb.Protect = CommonSer.RemoveAllTagHTML(medSymptomsVm.Protect, tagHtmlExceptRemove);
                    medSymptomsDb.Symptoms = CommonSer.RemoveAllTagHTML(medSymptomsVm.Symptoms, tagHtmlExceptRemove);
                    medSymptomsDb.DoctorCanDo = CommonSer.RemoveAllTagHTML(medSymptomsVm.DoctorCanDo, tagHtmlExceptRemove);
                    medSymptomsDb.ContentHTML = medSymptomsVm.ContentHTML;

                    medSymptomsDb.BotID = 3019;

                    _medSymptomsService.Update(medSymptomsDb);
                    _medSymptomsService.Save();
                    string rs = _apiNLR.UpdateSymptoms(medSymptomsDb.ID.ToString(), medSymptomsDb.Name, medSymptomsDb.Description, medSymptomsDb.Cause, medSymptomsDb.Treament,
                    medSymptomsDb.Advice, medSymptomsDb.Symptoms, medSymptomsDb.Predict, medSymptomsDb.Protect, medSymptomsDb.DoctorCanDo);

                }
                else
                {
                    medSymptomsDb.ID = medSymptomsVm.ID;
                    medSymptomsDb.Name = medSymptomsVm.Name;
                    medSymptomsDb.Description = CommonSer.RemoveAllTagHTML(medSymptomsVm.Description, tagHtmlExceptRemove);
                    medSymptomsDb.Cause = CommonSer.RemoveAllTagHTML(medSymptomsVm.Cause, tagHtmlExceptRemove);
                    medSymptomsDb.Treament = CommonSer.RemoveAllTagHTML(medSymptomsVm.Treament, tagHtmlExceptRemove);
                    medSymptomsDb.Advice = CommonSer.RemoveAllTagHTML(medSymptomsVm.Advice, tagHtmlExceptRemove);
                    medSymptomsDb.Predict = CommonSer.RemoveAllTagHTML(medSymptomsVm.Predict, tagHtmlExceptRemove);
                    medSymptomsDb.Protect = CommonSer.RemoveAllTagHTML(medSymptomsVm.Protect, tagHtmlExceptRemove);
                    medSymptomsDb.Symptoms = CommonSer.RemoveAllTagHTML(medSymptomsVm.Symptoms, tagHtmlExceptRemove);
                    medSymptomsDb.DoctorCanDo = CommonSer.RemoveAllTagHTML(medSymptomsVm.DoctorCanDo, tagHtmlExceptRemove);
                    medSymptomsDb.ContentHTML = medSymptomsVm.ContentHTML;

                    medSymptomsDb.BotID = 3019;

                    _medSymptomsService.Create(medSymptomsDb);
                    _medSymptomsService.Save();

                    string rs = _apiNLR.AddSymptoms(medSymptomsDb.ID.ToString(), medSymptomsDb.Name, medSymptomsDb.Description, medSymptomsDb.Cause, medSymptomsDb.Treament,
                    medSymptomsDb.Advice, medSymptomsDb.Symptoms, medSymptomsDb.Predict, medSymptomsDb.Protect, medSymptomsDb.DoctorCanDo);

                }

                response = request.CreateResponse(HttpStatusCode.OK, medSymptomsDb);
                return response;
            });
        }

    }
}
