using BotProject.Data.Infrastructure;
using BotProject.Data.Repositories;
using BotProject.Model.Models;
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
        ChatWidgetCustomization GetChatWidgetCustomizationByBotId(int botId);
        // language
        ChatWidgetLanguage AddChatWidgetLanguage(ChatWidgetLanguage cWidgetLanguage);
        ChatWidgetLanguage GetChatWidgetLanguageByBotId(int botId);
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

        public ChatWidgetCustomization GetChatWidgetCustomizationByBotId(int botId)
        {
            return _cWidgetCustomizationRepository.GetSingleByCondition(x => x.BotID == botId);
        }

        public ChatWidgetLanguage GetChatWidgetLanguageByBotId(int botId)
        {
            return _cWidgetLanguageRepository.GetSingleByCondition(x => x.BotID == botId);
        }
    }
}
