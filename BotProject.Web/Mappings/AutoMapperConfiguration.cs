using AutoMapper;
using BotProject.Model.Models;
using BotProject.Web.Models;

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
            });
        }
    }
}