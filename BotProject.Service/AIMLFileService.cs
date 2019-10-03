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
    public interface IAIMLFileService
    {
        AIMLFile GetByCardId(int cardId);
        IEnumerable<AIMLFile> GetByBotId(int botId);
        IEnumerable<AIMLFile> GetByUserId(string userId);
        AIMLFile GetByFormId(int formId);
        AIMLFile GetByID(int id);
        AIMLFile Create(AIMLFile aiml);
        void Update(AIMLFile aiml);
        void Save();
    }
    public class AIMLFileService : IAIMLFileService
    {
        IAIMLFileRepository _aimlRepository;
        IUnitOfWork _unitOfWork;
        public AIMLFileService(IUnitOfWork unitOfWork, IAIMLFileRepository aimlRepository)
        {
            _aimlRepository = aimlRepository;
            _unitOfWork = unitOfWork;
        }
        public AIMLFile Create(AIMLFile aiml)
        {
            return _aimlRepository.Add(aiml);
        }

        public IEnumerable<AIMLFile> GetByBotId(int botId)
        {
            return _aimlRepository.GetMulti(x => x.BotID == botId && x.Status == true);
        }

        public AIMLFile GetByCardId(int cardId)
        {
            return _aimlRepository.GetSingleByCondition(x => x.CardID == cardId);
        }

        public AIMLFile GetByFormId(int formId)
        {
            return _aimlRepository.GetSingleByCondition(x => x.FormQnAnswerID == formId);
        }

        public AIMLFile GetByID(int id)
        {
            return _aimlRepository.GetSingleById(id);
        }

        public IEnumerable<AIMLFile> GetByUserId(string userId)
        {
            return _aimlRepository.GetMulti(x => x.UserID == userId);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(AIMLFile aiml)
        {
            _aimlRepository.Update(aiml);
        }
    }
}
