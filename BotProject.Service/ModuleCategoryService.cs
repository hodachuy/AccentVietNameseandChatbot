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
        IEnumerable<MdQnAViewModel> GetListMdQnA(string filter, string sort, int pageNumber, int pageSize, long? selectedID);
        IEnumerable<MdArea> GetListMdArea(int? id);
        MdQuestion CreateQuestion(MdQuestion ques);
        MdAnswer CreateAnswer(MdAnswer ans);
        IEnumerable<ModuleCategory> GetAllModuleCategory();
        void UpdateQuestion(MdQuestion ques);
        void UpdateAnswer(MdAnswer ans);
        void Save();
    }
    public class ModuleCategoryService : IModuleCategoryService
    {
        private IUnitOfWork _unitOfWork;
        private IModuleCategoryRepository _moduleCategoryRepository;
        private IMdAreaRepository _mdAreaRepository;
        private IMdQuestionRepository _mdQuesRepository;
        private IMdAnswerRepository _mdAnswerRepository;
        public ModuleCategoryService(IUnitOfWork unitOfWork,
            IModuleCategoryRepository moduleCategoryRepository,
            IMdAreaRepository mdAreaRepository,
            IMdQuestionRepository mdQuesRepository,
            IMdAnswerRepository mdAnswerRepository)
        {
            _unitOfWork = unitOfWork;
            _moduleCategoryRepository = moduleCategoryRepository;
            _mdAreaRepository = mdAreaRepository;
            _mdQuesRepository = mdQuesRepository;
            _mdAnswerRepository = mdAnswerRepository;
        }
        public IEnumerable<MdQnAViewModel> GetListMdQnA(string filter, string sort, int pageNumber, int pageSize, long? selectedID)
        {
            return _moduleCategoryRepository.GetListMdQnA(filter,sort,pageNumber,pageSize,selectedID);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<MdArea> GetListMdArea(int? id)
        {
            if (id == null)
                return _mdAreaRepository.GetAll();
            return _mdAreaRepository.GetMulti(x => x.ID == id);
        }

        public MdQuestion CreateQuestion(MdQuestion ques)
        {
            return _mdQuesRepository.Add(ques);
        }

        public MdAnswer CreateAnswer(MdAnswer ans)
        {
            return _mdAnswerRepository.Add(ans);
        }

        public void UpdateQuestion(MdQuestion ques)
        {
            _mdQuesRepository.Update(ques);
        }

        public void UpdateAnswer(MdAnswer ans)
        {
            _mdAnswerRepository.Update(ans);
        }

        public IEnumerable<ModuleCategory> GetAllModuleCategory()
        {
            return _moduleCategoryRepository.GetAll();
        }
    }
}
