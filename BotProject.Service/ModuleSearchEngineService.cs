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
    public interface IModuleSearchEngineService
    {
        IEnumerable<MdQnAViewModel> GetListMdQnA(string filter, string sort, int pageNumber, int pageSize, long? selectedID);
        IEnumerable<MdArea> GetListMdArea(int? id);
        MdQuestion CreateQuestion(MdQuestion ques);
        MdAnswer CreateAnswer(MdAnswer ans);
        MdArea CreateArea(MdArea area);
        MdArea GetByAreaId(int id);
        MdArea GetByAreaName(string areaName);
        void UpdateQuestion(MdQuestion ques);
        void UpdateAnswer(MdAnswer ans);
        void UpdateArea(MdArea area);
        void DeleteArea(int areaId);
        void Save();
    }
    public class ModuleSearchEngineService : IModuleSearchEngineService
    {
        private IUnitOfWork _unitOfWork;
        private IMdAreaRepository _mdAreaRepository;
        private IMdQuestionRepository _mdQuesRepository;
        private IMdAnswerRepository _mdAnswerRepository;
        public ModuleSearchEngineService(IUnitOfWork unitOfWork,
            IMdAreaRepository mdAreaRepository,
            IMdQuestionRepository mdQuesRepository,
            IMdAnswerRepository mdAnswerRepository)
        {
            _unitOfWork = unitOfWork;
            _mdAreaRepository = mdAreaRepository;
            _mdQuesRepository = mdQuesRepository;
            _mdAnswerRepository = mdAnswerRepository;
        }
        public IEnumerable<MdQnAViewModel> GetListMdQnA(string filter, string sort, int pageNumber, int pageSize, long? selectedID)
        {
            return _mdQuesRepository.GetListMdQnA(filter, sort, pageNumber, pageSize, selectedID);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<MdArea> GetListMdArea(int? id)
        {
            if (id == null)
                return _mdAreaRepository.GetAll();
            return _mdAreaRepository.GetMulti(x => x.BotID == id);
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

        public MdArea CreateArea(MdArea area)
        {
            return _mdAreaRepository.Add(area);
        }

        public void UpdateArea(MdArea area)
        {
            _mdAreaRepository.Update(area);
        }

        public void DeleteArea(int areaId)
        {
            _mdAreaRepository.Delete(areaId);
        }

        public MdArea GetByAreaId(int id)
        {
            return _mdAreaRepository.GetSingleById(id);
        }

        public MdArea GetByAreaName(string areaName)
        {
            return _mdAreaRepository.GetSingleByCondition(x => x.Name.Contains(areaName));
        }
    }
}
