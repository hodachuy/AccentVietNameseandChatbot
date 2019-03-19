using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BotProject.Service;

namespace BotProject.Web.API
{
    public class CardController : ApiControllerBase
    {
        private ICardService _cardService;
        public CardController(IErrorService errorService, ICardService cardService) : base(errorService)
        {
            _cardService = cardService;
        }
    }
}
