using BotProject.Data.Infrastructure;
using BotProject.Data.Repositories;
using BotProject.Model.Models;
using BotProject.Model.Models.LiveChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Service.Livechat
{
    public interface IChatWidgetService
    {
        // customization
        ChatWidgetCustomization AddChatWidgetCustomization(ChatWidgetCustomization customization);
        ChatWidgetCustomization GetChatWidgetCustomizationByChannelGroupId(int channelGroupId);
        // language
        ChatWidgetLanguage AddChatWidgetLanguage(ChatWidgetLanguage cWidgetLanguage);
        ChatWidgetLanguage GetChatWidgetLanguageByChannelGroupId(int channelGroupId);
    }
    public class ChatWidgetService : IChatWidgetService
    {
        IChatWidgetCustomizationRepository _cWidgetCustomizationRepository;
        IChatWidgetLanguageRepository _cWidgetLanguageRepository;
        IUnitOfWork _unitOfWork;
        public ChatWidgetService(IChatWidgetCustomizationRepository cWidgetCustomizationRepository,
                                IChatWidgetLanguageRepository cWidgetLanguageRepository,
                                IUnitOfWork unitOfWork)
        {
            _cWidgetCustomizationRepository = cWidgetCustomizationRepository;
            _cWidgetLanguageRepository = cWidgetLanguageRepository;
            _unitOfWork = unitOfWork;
        }

        public ChatWidgetCustomization AddChatWidgetCustomization(ChatWidgetCustomization customization)
        {
            return _cWidgetCustomizationRepository.Add(customization);
        }

        public ChatWidgetLanguage AddChatWidgetLanguage(ChatWidgetLanguage cWidgetLanguage)
        {
            return _cWidgetLanguageRepository.Add(cWidgetLanguage);
        }

        public ChatWidgetCustomization GetChatWidgetCustomizationByChannelGroupId(int channelGroupId)
        {
            return _cWidgetCustomizationRepository.GetSingleByCondition(x => x.ChannelGroupID == channelGroupId);
        }

        public ChatWidgetLanguage GetChatWidgetLanguageByChannelGroupId(int channelGroupId)
        {
            return _cWidgetLanguageRepository.GetSingleByCondition(x => x.ChannelGroupID == channelGroupId);
        }
    }
}
