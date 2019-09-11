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
    public interface IGroupCardService
    {
        GroupCard Create(GroupCard GroupCard);
        void Update(GroupCard GroupCard);
        IEnumerable<GroupCard> GetListGroupCardByBotID(int botId);
        GroupCard Delete(int id);
        GroupCard GetById(int id);
        void Save();
    }
    public class GroupCardService : IGroupCardService
    {
        IGroupCardRepository _GroupCardRepository;
        IUnitOfWork unitOfWork;
        public GroupCardService(IGroupCardRepository GroupCardRepository, IUnitOfWork unitOfWork)
        {
            _GroupCardRepository = GroupCardRepository;
            this.unitOfWork = unitOfWork;
        }
        public GroupCard Create(GroupCard GroupCard)
        {
            return _GroupCardRepository.Add(GroupCard);
        }

        public IEnumerable<GroupCard> GetListGroupCardByBotID(int botId)
        {
            return _GroupCardRepository.GetMulti(x => x.BotID == botId && x.IsDelete == false);
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public void Update(GroupCard GroupCard)
        {
            _GroupCardRepository.Update(GroupCard);
        }

        public GroupCard Delete(int id)
        {
            return _GroupCardRepository.Delete(id);
        }

        public GroupCard GetById(int id)
        {
            return _GroupCardRepository.GetSingleById(id);
        }
    }
}
