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
    public interface IHistoryService
    {
        IEnumerable<StoreProcHistoryViewModel> GetHistoryByBotId(string filter, string sort, int pageNumber, int pageSize, long? selectedID);
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

        public IEnumerable<StoreProcHistoryViewModel> GetHistoryByBotId(string filter, string sort, int pageNumber, int pageSize, long? selectedID)
        {
            return _historyRepository.GetListHistory(filter, sort, pageNumber, pageSize, selectedID);
        }

        public History Create(History history)
        {
            return _historyRepository.Add(history);
        }

        public History GetById(int id)
        {
            return _historyRepository.GetSingleById(id);
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
