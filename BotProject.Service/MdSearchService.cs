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
    public interface IMdSearchService
    {
        MdSearch GetByID(int id);
        IEnumerable<MdSearch> GetByBotID(int botID);
        MdSearch Create(MdSearch module);
        void Delete(int id);
        void Update(MdSearch module);
        void Save();
    }

    public class MdSearchService : IMdSearchService
    {
        IMdSearchRepository _mdSearchRepository;
        IUnitOfWork _unitOfWork;
        public MdSearchService(IMdSearchRepository mdSearchRepository,
                               IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mdSearchRepository = mdSearchRepository;
        }
        public MdSearch Create(MdSearch module)
        {
            return _mdSearchRepository.Add(module);
        }

        public void Delete(int id)
        {
            _mdSearchRepository.Delete(id);
        }

        public IEnumerable<MdSearch> GetByBotID(int botID)
        {
            return _mdSearchRepository.GetMulti(x => x.BotID == botID);
        }

        public MdSearch GetByID(int id)
        {
            return _mdSearchRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(MdSearch module)
        {
            _mdSearchRepository.Update(module);
        }
    }
}
