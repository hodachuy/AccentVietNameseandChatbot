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
    public interface IMdSearchCategoryService
    {
        MdSearchCategory GetById(int id);
        IEnumerable<MdSearchCategory> GetListMdSearchCategory();
    }
    public class MdSearchCategoryService : IMdSearchCategoryService
    {
        private IUnitOfWork _unitOfWork;
        private IMdSearchCategoryRepository _mdSearchCategoryRepository;

        public MdSearchCategoryService(IUnitOfWork unitOfWork, IMdSearchCategoryRepository mdSearchCategoryRepository)
        {
            _unitOfWork = unitOfWork;
            _mdSearchCategoryRepository = mdSearchCategoryRepository;
        }

        public MdSearchCategory GetById(int id)
        {
            return _mdSearchCategoryRepository.GetSingleById(id);
        }

        public IEnumerable<MdSearchCategory> GetListMdSearchCategory()
        {
            return _mdSearchCategoryRepository.GetAll();
        }
    }
}
