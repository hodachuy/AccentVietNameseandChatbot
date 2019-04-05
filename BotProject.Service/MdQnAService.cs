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
    public interface IMdQnAService
    {
        IEnumerable<MdQnAViewModel> GetListMdQnA(string filter, string sort, int pageNumber, int pageSize, long? selectedID = null);
        IEnumerable<MdArea> GetListMdArea(int? id);
        MdQuestion CreateQuestion(MdQuestion ques);
        MdAnswer CreateAnswer(MdAnswer ans);
        void UpdateQuestion(MdQuestion ques);
        void UpdateAnswer(MdAnswer ans);
        void Save();
    }
    public class MdQnAService : IMdQnAService
    {
        private IUnitOfWork _unitOfWork;
        private IModuleRepository _moduleRepository;
        private IMdAreaRepository _mdAreaRepository;
        private IMdQuestionRepository _mdQuesRepository;
        private IMdAnswerRepository _mdAnswerRepository;
        public MdQnAService(IUnitOfWork unitOfWork,
            IModuleRepository moduleRepository,
            IMdAreaRepository mdAreaRepository,
            IMdQuestionRepository mdQuesRepository,
            IMdAnswerRepository mdAnswerRepository)
        {
            _unitOfWork = unitOfWork;
            _moduleRepository = moduleRepository;
            _mdAreaRepository = mdAreaRepository;
            _mdQuesRepository = mdQuesRepository;
            _mdAnswerRepository = mdAnswerRepository;
        }
        public IEnumerable<MdQnAViewModel> GetListMdQnA(string filter, string sort, int pageNumber, int pageSize, long? selectedID = default(long?))
        {
            return _moduleRepository.GetListMdQnA(filter,sort,pageNumber,pageSize,selectedID);
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
    }
}
