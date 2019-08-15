using BotProject.Common.ViewModels;
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
    public interface IModuleCategoryService
    {
        IEnumerable<ModuleCategory> GetAllModuleCategory();
    }
    public class ModuleCategoryService : IModuleCategoryService
    {
        private IUnitOfWork _unitOfWork;
        private IModuleCategoryRepository _moduleCategoryRepository;
        public ModuleCategoryService(IUnitOfWork unitOfWork,
            IModuleCategoryRepository moduleCategoryRepository)
        {
            _unitOfWork = unitOfWork;
            _moduleCategoryRepository = moduleCategoryRepository;
        }     
        public IEnumerable<ModuleCategory> GetAllModuleCategory()
        {
            return _moduleCategoryRepository.GetAll();
        }
    }
}
