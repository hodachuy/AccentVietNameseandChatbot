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
using AutoMapper;

namespace BotProject.Web.API
{
    [RoutePrefix("api/attribute")]
    public class AttributeSystemController : ApiControllerBase
    {
        private IAttributeSystemService _attributeService;
        public AttributeSystemController(IErrorService errorService,
                                        IAttributeSystemService attributeService) : base(errorService)
        {
            _attributeService = attributeService;
        }

        [Route("getListByBotId")]
        [HttpGet]
        public HttpResponseMessage GetListAttributeSystemByBotId(HttpRequestMessage request, int botId, int page, int pageSize = 10)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var lstAttribute = _attributeService.GetListAttributeSystemByBotId(botId);
                var lstAttributeVm = Mapper.Map<IEnumerable<AttributeSystem>, IEnumerable<AttributeSystemViewModel>>(lstAttribute);
                totalRow = lstAttributeVm.Count();
                var query = lstAttributeVm.Skip((page - 1) * pageSize).Take(pageSize);
                var paginationSet = new PaginationSet<AttributeSystemViewModel>()
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
        [Route("createUpdate")]
        [HttpPost]
        public HttpResponseMessage CreateUpdateAttributeSystem(HttpRequestMessage request, AttributeSystemViewModel attVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                try
                {
                    AttributeSystem attrDb = new AttributeSystem();
                    if (attVm.ID == 0)
                    {
                        attrDb.BotID = attVm.BotID;
                        attrDb.Name = attVm.Name;
                        attrDb.Type = attVm.Type;
                        _attributeService.Create(attrDb);
                        _attributeService.Save();
                        response = request.CreateResponse(HttpStatusCode.OK, new { status = true, attribute = attrDb });
                    }else
                    {
                        attrDb = _attributeService.GetById(attVm.ID);
                        attrDb.Name = attVm.Name;
                        attrDb.Type = attVm.Type;
                        _attributeService.Update(attrDb);
                        _attributeService.Save();
                        response = request.CreateResponse(HttpStatusCode.OK, new { status = true, attribute = attrDb });
                    }
                }
                catch(Exception ex)
                {
                    response = request.CreateResponse(HttpStatusCode.OK, new { status = false });
                }
                return response;
            });
        }      
    }
}
