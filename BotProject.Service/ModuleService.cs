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
    public interface IModuleService
    {
        IEnumerable<Module> GetAll();
        Module GetByID(int id);
        Module Create(Module module);
        void Update(Module module);
        IEnumerable<Module> GetAllModuleByBotID(int botId);
        void Save();
    }
    public class ModuleService : IModuleService
    {
        IModuleRepository _moduleRepository;
        IUnitOfWork _unitOfWork;
        public ModuleService(IUnitOfWork unitOfWork, IModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
            _unitOfWork = unitOfWork;
        }
        public Module Create(Module module)
        {
            return _moduleRepository.Add(module);
        }

        public IEnumerable<Module> GetAll()
        {
            return _moduleRepository.GetAll();
        }

        public IEnumerable<Module> GetAllModuleByBotID(int botId)
        {
            return _moduleRepository.GetMulti(x => x.BotID == botId);
        }

        public Module GetByID(int id)
        {
            return _moduleRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Module module)
        {
            _moduleRepository.Update(module);
        }
    }
}
