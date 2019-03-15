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
        Bot Create(Bot bot);
        IEnumerable<Bot> GetListBotByUserID(string userId);
        Bot GetByID(int botId);
        void Save();
    }
    public class BotService : IBotService
    {
        IBotRepository _botRepository;
        IUnitOfWork unitOfWork;
        public BotService(IBotRepository botRepository, IUnitOfWork unitOfWork)
        {
            _botRepository = botRepository;
            this.unitOfWork = unitOfWork;
        }
        public Bot Create(Bot bot)
        {
            return _botRepository.Add(bot);
        }


        public void Save()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<Bot> GetListBotByUserID(string userId)
        {
            return _botRepository.GetMulti(x => x.UserID == userId).OrderBy(x => x.CreatedDate);
        }

        public Bot GetByID(int botId)
        {
            return _botRepository.GetSingleById(botId);
        }
    }
}
