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
    public interface IMedicalSymptomService
    {
        IEnumerable<MedicalSymptom> GetAll();
        MedicalSymptom GetById(int Id);
        MedicalSymptom Create(MedicalSymptom medSymptom);
        MedicalSymptom CreateUpdate(MedicalSymptom medSymptom);
        void Update(MedicalSymptom medSymptom);
        void Save();
    }
    public class MedicalSymptomService : IMedicalSymptomService
    {
        IMedicalSymptomRepository _medSymptomRepository;
        IUnitOfWork _unitOfWork;
        public MedicalSymptomService(IMedicalSymptomRepository medSymptomRepository, IUnitOfWork unitOfWork)
        {
            _medSymptomRepository = medSymptomRepository;
            _unitOfWork = unitOfWork;
        }
        public MedicalSymptom Create(MedicalSymptom medSymptom)
        {
            return _medSymptomRepository.Add(medSymptom);
        }

        public void Update(MedicalSymptom medSymptom)
        {
            _medSymptomRepository.Update(medSymptom);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<MedicalSymptom> GetAll()
        {
            return _medSymptomRepository.GetAll();
        }

        public MedicalSymptom GetById(int Id)
        {
            return _medSymptomRepository.GetSingleById(Id);
        }

        public MedicalSymptom CreateUpdate(MedicalSymptom medSymptom)
        {
            if(medSymptom.ID != 0)
            {
                Update(medSymptom);
                return medSymptom;
            }
            return _medSymptomRepository.Add(medSymptom);
        }
    }
}
