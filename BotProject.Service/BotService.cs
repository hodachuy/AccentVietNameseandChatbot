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
    public interface IBotService
    {
        Bot Create(ref Bot bot);
        IEnumerable<Bot> GetListBotByUserID(string userId);
        IEnumerable<Bot> GetListBotByAllUser();
        IEnumerable<StoreProcBotViewModel> GetListBotDashboard(string userId);
		Bot GetByID(int botId);
        void Update(Bot bot);
        void Save();
    }
    public class BotService : IBotService
    {
        IBotRepository _botRepository;
        IUnitOfWork _unitOfWork;
        public BotService(IBotRepository botRepository, IUnitOfWork unitOfWork)
        {
            _botRepository = botRepository;
			_unitOfWork = unitOfWork;
        }
        public Bot Create(ref Bot bot)
        {
            try
            {
                _botRepository.Add(bot);
                _unitOfWork.Commit();
                return bot;
            }
            catch(Exception ex)
            {
                throw;
            }        
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Bot> GetListBotByUserID(string userId)
        {
            return _botRepository.GetMulti(x => x.UserID == userId && x.Status == true).OrderBy(x => x.CreatedDate);
        }

        public Bot GetByID(int botId)
        {
            return _botRepository.GetSingleById(botId);
        }

        public void Update(Bot bot)
        {
            _botRepository.Update(bot);
        }

        public IEnumerable<StoreProcBotViewModel> GetListBotDashboard(string userId)
        {
            return _botRepository.GetListBotDashboard(userId);
        }

        public IEnumerable<Bot> GetListBotByAllUser()
        {
            return _botRepository.GetMulti(x => x.Status == true);
        }
    }
}
