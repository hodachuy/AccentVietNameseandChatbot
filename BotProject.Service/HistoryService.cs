using BotProject.Data.Infrastructure;
using BotProject.Data.Repositories;
using BotProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Service
{
    public interface IHistoryService
    {
        IEnumerable<History> GetHistoryByBotId(int botId);
        History Create(History history);
        History GetById(int id);
        void Update(History history);
        void Save();
    }
    public class HistoryService: IHistoryService
    {
        IHistoryRepository _historyRepository;
        IUnitOfWork _unitOfWork;
        public HistoryService(IHistoryRepository historyRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _historyRepository = historyRepository;
        }

        public History Create(History history)
        {
            return _historyRepository.Add(history);
        }

        public History GetById(int id)
        {
            return _historyRepository.GetSingleById(id);
        }

        public IEnumerable<History> GetHistoryByBotId(int botId)
        {
            return _historyRepository.GetMulti(x => x.BotID == botId);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(History history)
        {
            _historyRepository.Update(history);
        }
    }
}
