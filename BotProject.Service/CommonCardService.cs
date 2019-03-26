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
    public interface ICommonCardService
    {
        //add
        ButtonLink AddButtonLink(ButtonLink btnLink);
        ButtonPostback AddButtonPostback(ButtonPostback btnPostback);
        TemplateGenericGroup AddTempGnrGroup(TemplateGenericGroup tempGnrGroup);
        TemplateGenericItem AddTempGnrItem(TemplateGenericItem tempGnrItem);
        TemplateText AddTempText(TemplateText tempText);
        Image AddImage(Image image);
        QuickReply AddQuickReply(QuickReply qReply);


        //ButtonLink DeleteButtonLink(int id);
        //ButtonPostback DeleteButtonPostback(int id);
        //TemplateGenericItem DeleteTempGnrItem(int id);
        //TemplateGenericGroup DeleteTempGnrGroup(int id);
        //QuickReply DeleteQuickReply(int id);
        //Image DeleteImage(int id);


        Card GetFullDetailCard(int cardId);

        Card DeleteCard(int cardId);
        void DeleteFullContentCard(int cardId);

        void Save();
    }

    public class CommonCardService : ICommonCardService
    {
        IButtonLinkRepository _buttonLinkRepository;
        IButtonPostbackRepository _buttonPostbackRepository;
        IImageRepository _imageRepository;
        ITemplateGenericGroupRepository _templateGenericGroupRepository;
        ITemplateGenericItemRepository _templateGenericItemRepository;
        ITemplateTextRepository _templateTextRepository;
        IQuickReplyRepository _quickReplyRepository;
        ICardRepository _cardRepository;
        IUnitOfWork _unitOfWork;
        public CommonCardService(IUnitOfWork unitOfWork,
                                ICardRepository cardRepository,
                                IButtonLinkRepository buttonLinkRepository,
                                IButtonPostbackRepository buttonPostbackRepository,
                                IImageRepository imageRepository,
                                ITemplateGenericGroupRepository templateGenericGroupRepository,
                                ITemplateGenericItemRepository templateGenericItemRepository,
                                ITemplateTextRepository templateTextRepository,
                                IQuickReplyRepository quickReplyRepository)
        {
            _unitOfWork = unitOfWork;
            _cardRepository = cardRepository;
            _buttonLinkRepository = buttonLinkRepository;
            _buttonPostbackRepository = buttonPostbackRepository;
            _imageRepository = imageRepository;
            _templateGenericGroupRepository = templateGenericGroupRepository;
            _templateGenericItemRepository = templateGenericItemRepository;
            _templateTextRepository = templateTextRepository;
            _quickReplyRepository = quickReplyRepository;
        }
        public ButtonLink AddButtonLink(ButtonLink btnLink)
        {
            return _buttonLinkRepository.Add(btnLink);
        }

        public ButtonPostback AddButtonPostback(ButtonPostback btnPostback)
        {
            return _buttonPostbackRepository.Add(btnPostback);
        }

        public Image AddImage(Image image)
        {
            return _imageRepository.Add(image);
        }

        public QuickReply AddQuickReply(QuickReply qReply)
        {
            return _quickReplyRepository.Add(qReply);
        }

        public TemplateGenericGroup AddTempGnrGroup(TemplateGenericGroup tempGnrGroup)
        {
            return _templateGenericGroupRepository.Add(tempGnrGroup);
        }

        public TemplateGenericItem AddTempGnrItem(TemplateGenericItem tempGnrItem)
        {
            return _templateGenericItemRepository.Add(tempGnrItem);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public TemplateText AddTempText(TemplateText tempText)
        {
            return _templateTextRepository.Add(tempText);
        }

        public Card GetFullDetailCard(int cardId)
        {
            Card card = new Card();
            card = _cardRepository.GetSingleById(cardId);
            card.Images = _imageRepository.GetMulti(x => x.CardID == cardId).ToList();
            card.TemplateTexts = _templateTextRepository.GetMulti(x => x.CardID == cardId).ToList();
            card.TemplateGenericGroups = _templateGenericGroupRepository.GetMulti(x => x.CardID == cardId).ToList();
            card.QuickReplys = _quickReplyRepository.GetMulti(x => x.CardID == cardId).ToList();
            if(card.TemplateTexts != null && card.TemplateTexts.Count() != 0)
            {
                foreach(var item in card.TemplateTexts)
                {
                    item.ButtonLinks = _buttonLinkRepository.GetMulti(x => x.TempTxtID == item.ID).ToList();
                    item.ButtonPostbacks = _buttonPostbackRepository.GetMulti(x => x.TempTxtID == item.ID).ToList();
                }
            }
            if(card.TemplateGenericGroups != null && card.TemplateGenericGroups.Count() != 0)
            {
                foreach(var tempGroup in card.TemplateGenericGroups)
                {
                    tempGroup.TemplateGenericItems = _templateGenericItemRepository.GetMulti(x => x.TempGnrGroupID == tempGroup.ID).ToList();
                    if(tempGroup.TemplateGenericItems != null && tempGroup.TemplateGenericItems.Count() != 0)
                    {
                        foreach(var tempItem in tempGroup.TemplateGenericItems)
                        {
                            tempItem.ButtonLinks = _buttonLinkRepository.GetMulti(x => x.TempGnrItemID == tempItem.ID).ToList();
                            tempItem.ButtonPostbacks = _buttonPostbackRepository.GetMulti(x => x.TempGnrItemID == tempItem.ID).ToList();
                        }
                    }
                }
            }
            
            return card;
        }

        // xóa lun card
        public Card DeleteCard(int cardId)
        {
            throw new NotImplementedException();
        }

        // xóa nội dung trong card
        public void DeleteFullContentCard(int cardId)
        {
            _buttonLinkRepository.DeleteMulti(x => x.CardID == cardId);
            _buttonPostbackRepository.DeleteMulti(x => x.CardID == cardId);
            _templateTextRepository.DeleteMulti(x => x.CardID == cardId);
            _templateGenericItemRepository.DeleteMulti(x => x.CardID == cardId);
            _templateGenericGroupRepository.DeleteMulti(x => x.CardID == cardId);
            _quickReplyRepository.DeleteMulti(x => x.CardID == cardId);
        }
    }
}
