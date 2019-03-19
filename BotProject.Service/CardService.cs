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
    public interface ICardService
    {
        Card Create(ref Card Card);
        IEnumerable<Card> GetListCardByBotID(int botId);
        Card GetByID(int CardId);
        void Save();
    }
    public class CardService : ICardService
    {
        ICardRepository _CardRepository;
        IUnitOfWork _unitOfWork;
        public CardService(ICardRepository CardRepository, IUnitOfWork unitOfWork)
        {
            _CardRepository = CardRepository;
            _unitOfWork = unitOfWork;
        }
        public Card Create(ref Card Card)
        {
            try
            {
                _CardRepository.Add(Card);
                _unitOfWork.Commit();
                return Card;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Card> GetListCardByBotID(int botId)
        {
            return _CardRepository.GetMulti(x => x.BotID == botId);
        }

        public Card GetByID(int CardId)
        {
            return _CardRepository.GetSingleById(CardId);
        }
    }
}
