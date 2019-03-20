using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BotProject.Service;
using BotProject.Web.Models;
using BotProject.Model.Models;
using BotProject.Common;

namespace BotProject.Web.API
{
    [RoutePrefix("api/card")]
    public class CardController : ApiControllerBase
    {
        private ICardService _cardService;
        private ICommonCardService _commonCardService;
        public CardController(IErrorService errorService,
                            ICardService cardService,
                            ICommonCardService commonCardService) : base(errorService)
        {
            _cardService = cardService;
            _commonCardService = commonCardService;
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, CardViewModel cardVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                if(cardVm.ID == null)
                {
                    Card cardDb = new Card();
                    cardDb.Name = cardVm.Name;
                    _cardService.Create(cardDb);
                    _cardService.Save();
                    cardDb.PatternText = CommonConstants.PostBackCard + cardDb.ID.ToString();//use to card #
                    if(cardVm.CardContents != null && cardVm.CardContents.Count() != 0)
                    {

                    }
                }
                return response;
            });
        }
    }
}
