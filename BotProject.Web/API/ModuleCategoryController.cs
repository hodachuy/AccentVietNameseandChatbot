using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BotProject.Service;
using BotProject.Model.Models;

namespace BotProject.Web.API
{
    [RoutePrefix("api/modulecategory")]
    public class ModuleCategoryController : ApiControllerBase
    {
        private IModuleCategoryService _moduleCategoryService;
        public ModuleCategoryController(IErrorService errorService, IModuleCategoryService moduleCategoryService) : base(errorService)
        {
            _moduleCategoryService = moduleCategoryService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var lstModuleCategory = _moduleCategoryService.GetAllModuleCategory().ToList();
                if (lstModuleCategory.Count() != 0)
                {
                    totalRow = lstModuleCategory.Count();
                }
                var paginationSet = new PaginationSet<ModuleCategory>()
                {
                    Items = lstModuleCategory,
                    Page = page,
                    TotalCount = totalRow,
                    MaxPage = pageSize,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }
    }
}
