using BotProject.Data.Infrastructure;
using BotProject.Data.Repositories;
using BotProject.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Service
{
    public interface IApplicationThirdPartyService
    {
        ApplicationThirdParty GetByPageId(string pageId);
        ApplicationThirdParty GetByZaloPageId(string pageId);
    }
    public class ApplicationThirdPartyService : IApplicationThirdPartyService
    {
        IApplicationThirdPartyRepository _app3rd;
        IUnitOfWork _unitOfWork;
        public ApplicationThirdPartyService(IApplicationThirdPartyRepository app3rd, IUnitOfWork unitOfWork)
        {
            _app3rd = app3rd;
            _unitOfWork = unitOfWork;
        }
        public ApplicationThirdParty GetByPageId(string pageId)
        {
            return _app3rd.GetSingleByCondition(x => x.PageID == pageId);
        }

        public ApplicationThirdParty GetByZaloPageId(string pageId)
        {
            return _app3rd.GetSingleByCondition(x => x.PageID == pageId && x.Type == "zalo");
        }
    }
}
