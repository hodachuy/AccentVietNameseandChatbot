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
    public interface IModuleKnowledegeService
    {
        ModuleKnowledgeMedInfoPatient Add(ModuleKnowledgeMedInfoPatient mdKnowledgeMedInfoPatient);
        ModuleKnowledgeMedInfoPatient GetByMdMedInfoPatientID(int id);
        void Save();
    }
    public class ModuleKnowledegeService : IModuleKnowledegeService
    {
        IModuleKnowledgeMedInfoPatientRepository _mdKnowledgeMedInfoPatientRepository;
        IUnitOfWork _unitOfWork;
        public ModuleKnowledegeService(IUnitOfWork unitOfWork,
            IModuleKnowledgeMedInfoPatientRepository mdKnowledgeMedInfoPatientRepository)
        {
            _unitOfWork = unitOfWork;
            _mdKnowledgeMedInfoPatientRepository = mdKnowledgeMedInfoPatientRepository;
        }

        public ModuleKnowledgeMedInfoPatient Add(ModuleKnowledgeMedInfoPatient mdKnowledgeMedInfoPatient)
        {
            return _mdKnowledgeMedInfoPatientRepository.Add(mdKnowledgeMedInfoPatient);
        }

        public ModuleKnowledgeMedInfoPatient GetByMdMedInfoPatientID(int id)
        {
            return _mdKnowledgeMedInfoPatientRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
