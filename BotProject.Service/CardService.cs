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
    public interface ICardService
    {
        Card Create(Card Card);
        void Update(Card card);
        IEnumerable<Card> GetListCardByBotID(int botId);
        IEnumerable<Card> GetListCardByGroupCardID(int grCardId);
        Card GetByID(int CardId);
        Card GetSingleCondition(string pattern);
        StoreProcCardViewModel GetCardByPattern(string pattern);
        Card GetByCardCloneParentID(int cardCloneParentId);
        void Save();
    }
    public class CardService : ICardService
    {
        ICardRepository _cardRepository;      
        IUnitOfWork _unitOfWork;
        public CardService(ICardRepository cardRepository, IUnitOfWork unitOfWork)
        {
            _cardRepository = cardRepository;
            _unitOfWork = unitOfWork;
        }
        public Card Create(Card Card)
        {
            return _cardRepository.Add(Card);
        }
        public IEnumerable<Card> GetListCardByBotID(int botId)
        {
            return _cardRepository.GetMulti(x => x.BotID == botId && x.IsDelete == false);
        }
        public Card GetByID(int CardId)
        {
            var card = _cardRepository.GetSingleById(CardId);
            return card; 
        }

        public void Update(Card card)
        {
            _cardRepository.Update(card);
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }
        public IEnumerable<Card> GetListCardByGroupCardID(int grCardId)
        {
            return _cardRepository.GetMulti(x => x.GroupCardID == grCardId && x.IsDelete == false);
        }

        public Card GetSingleCondition(string pattern)
        {
            return _cardRepository.GetSingleByCondition(x => x.PatternText.Contains(pattern));
        }

        public StoreProcCardViewModel GetCardByPattern(string pattern)
        {
            return _cardRepository.GetCardByPattern(pattern);
        }

        public Card GetByCardCloneParentID(int cardCloneParentId)
        {
            return _cardRepository.GetSingleByCondition(x => x.CardCloneParentID == cardCloneParentId);
        }
    }
}
