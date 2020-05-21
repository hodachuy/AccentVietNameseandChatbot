using AutoMapper;
using BotProject.Common.ViewModels;
using BotProject.Model.Models;
using BotProject.Model.Models.LiveChat;
using BotProject.Web.Models;
using BotProject.Web.Models.Livechat;

namespace BotProject.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ApplicationGroup, ApplicationGroupViewModel>();
                cfg.CreateMap<ApplicationRole, ApplicationRoleViewModel>();
                cfg.CreateMap<ApplicationUser, ApplicationUserViewModel>();
                cfg.CreateMap<Bot, BotViewModel>();
				cfg.CreateMap<FormQuestionAnswer, FormQuestionAnswerViewModel>();
                cfg.CreateMap<Setting, BotSettingViewModel>();
                cfg.CreateMap<GroupCard, GroupCardViewModel>();
                cfg.CreateMap<Card, CardViewModel>();
                cfg.CreateMap<Module, ModuleViewModel>();
                cfg.CreateMap<AIMLFile, AIMLViewModel>();
                cfg.CreateMap<AttributeSystem, AttributeSystemViewModel>();
                cfg.CreateMap<SystemConfig, SystemConfigViewModel>();
                cfg.CreateMap<StoreProcQuesGroupViewModel, QuestionGroup>();
                cfg.CreateMap<MedicalSymptom, MedicalSymptomViewModel>();
                cfg.CreateMap<StoreProcBotViewModel, BotViewModel>();
                cfg.CreateMap<Customer, CustomerViewModel>();
                cfg.CreateMap<Device, DeviceViewModel>();
            });
        }
    }
}