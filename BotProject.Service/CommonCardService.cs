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
        ButtonLink AddButtonLink(ButtonLink btnLink);
        ButtonPostback AddButtonPostback(ButtonPostback btnPostback);
        TemplateGenericGroup AddTempGnrGroup(TemplateGenericGroup tempGnrGroup);
        TemplateGenericItem AddTempGnrItem(TemplateGenericItem tempGnrItem);
        Image AddImage(Image image);
        QuickReply AddQuickReply(QuickReply qReply);
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
        IUnitOfWork _unitOfWork;
        public CommonCardService(IUnitOfWork unitOfWork,
                                IButtonLinkRepository buttonLinkRepository,
                                IButtonPostbackRepository buttonPostbackRepository,
                                IImageRepository imageRepository,
                                ITemplateGenericGroupRepository templateGenericGroupRepository,
                                ITemplateGenericItemRepository templateGenericItemRepository,
                                ITemplateTextRepository templateTextRepository,
                                IQuickReplyRepository quickReplyRepository)
        {
            _unitOfWork = unitOfWork;
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
    }
}
