using BotProject.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BotProject.Service;
using AutoMapper;
using BotProject.Web.Models;
using BotProject.Model.Models;
using AIMLbot;
using System.Web;
using BotProject.Common;

namespace BotProject.Web.API_Mobile
{
    [RoutePrefix("api/messages")]
    public class MessageController : ApiControllerBase
    {
        private readonly ISettingService _settingService;
        private readonly IBotService _botDbService;
        private readonly IAIMLFileService _aimlFileService;

        private readonly IHandleModuleServiceService _handleMdService;
        private readonly IModuleService _mdService;
        private readonly IModuleKnowledegeService _mdKnowledgeService;
        private readonly IMdSearchService _mdSearchService;

        private BotService _botService;
        private User _user;
        public MessageController(IErrorService errorService,
                                ISettingService settingService,
                                IBotService botDbService,
                                IAIMLFileService aimlFileService,
                                IHandleModuleServiceService handleMdService,
                                IModuleService mdService,
                                IModuleKnowledegeService mdKnowledgeService,
                                IMdSearchService mdSearchService) : base(errorService)
        {
            _settingService = settingService;
            _botDbService = botDbService;
            _botService = BotService.BotInstance;
            _aimlFileService = aimlFileService;
            _handleMdService = handleMdService;
            _mdService = mdService;
            _mdKnowledgeService = mdKnowledgeService;
            _mdSearchService = mdSearchService;
        }

        [Route("loadForm")]
        [HttpGet]
        public HttpResponseMessage LoadForm(HttpRequestMessage request, string tokenId, int botId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var botDb = _botDbService.GetByID(botId);
                var settingDb = _settingService.GetSettingByBotID(botId);
                var settingVm = Mapper.Map<Setting, BotSettingViewModel>(settingDb);

                var lstAIML = _aimlFileService.GetByBotId(botId);
                var lstAIMLVm = Mapper.Map<IEnumerable<AIMLFile>, IEnumerable<AIMLViewModel>>(lstAIML);
                _botService.loadAIMLFromDatabase(lstAIMLVm);

                UserBotViewModel userBot = new UserBotViewModel();
                userBot.ID = Guid.NewGuid().ToString();
                userBot.BotID = botId.ToString();
                _user = _botService.loadUserBot(userBot.ID);


                _user.Predicates.addSetting("phone", "");
                _user.Predicates.addSetting("phonecheck", "false");

                _user.Predicates.addSetting("email", "");
                _user.Predicates.addSetting("emailcheck", "false");

                _user.Predicates.addSetting("age", "");
                _user.Predicates.addSetting("agecheck", "false");

                _user.Predicates.addSetting("name", "");
                _user.Predicates.addSetting("namecheck", "false");

                // load tất cả module của bot và thêm key vao predicate
                var mdBotDb = _mdService.GetAllModuleByBotID(botId).Where(x => x.Name != "med_get_info_patient").ToList();
                if (mdBotDb.Count() != 0)
                {
                    foreach (var item in mdBotDb)
                    {
                        _user.Predicates.addSetting(item.Name, "");
                        _user.Predicates.addSetting(item.Name + "check", "false");
                    }
                }
                var lstMdGetInfoPatientDb = _mdKnowledgeService.GetAllMdKnowledgeMedInfPatientByBotID(botId).ToList();
                if (lstMdGetInfoPatientDb.Count() != 0)
                {
                    foreach (var item in lstMdGetInfoPatientDb)
                    {
                        if (!String.IsNullOrEmpty(item.OptionText))
                        {
                            int index = 0;
                            var arrOpt = item.OptionText.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var opt in arrOpt)
                            {
                                index++;
                                _user.Predicates.addSetting(item.ID + "_opt_" + index, "");
                            }
                        }
                        if (!String.IsNullOrEmpty(item.Payload))
                        {
                            _user.Predicates.addSetting("med_get_info_patient_" + item.ID, "");
                            _user.Predicates.addSetting("med_get_info_patient_check_" + item.ID, "false");
                        }
                    }
                }
                var lstMdSearchDb = _mdSearchService.GetByBotID(botId).ToList();
                if (lstMdSearchDb.Count() != 0)
                {
                    foreach (var item in lstMdSearchDb)
                    {
                        if (!String.IsNullOrEmpty(item.Payload))
                        {
                            _user.Predicates.addSetting("api_search" + item.ID, "");//check bat mo truong hop nut' payload khi qua the moi
                            _user.Predicates.addSetting("api_search_check_" + item.ID, "false");
                        }
                    }
                }
                _user.Predicates.addSetting("isChkMdGetInfoPatient", "false");
                _user.Predicates.addSetting("ThreadMdGetInfoPatientId", "");
                _user.Predicates.addSetting("isChkMdSearch", "false");
                _user.Predicates.addSetting("ThreadMdSearchID", "");
                SettingsDictionaryViewModel settingDic = new SettingsDictionaryViewModel();
                settingDic.Count = _user.Predicates.Count;
                settingDic.orderedKeys = _user.Predicates.orderedKeys;
                settingDic.settingsHash = _user.Predicates.settingsHash;
                settingDic.SettingNames = _user.Predicates.SettingNames;
                userBot.SettingDicstionary = settingDic;
                HttpContext.Current.Session[CommonConstants.SessionUserBot] = userBot;

                response = request.CreateResponse(HttpStatusCode.OK, settingVm);

                return response;
            });
        }

        [Route("get")]
        [HttpGet]
        public HttpResponseMessage GetMessage(HttpRequestMessage request, string msg, string accessToken, string botId, string isAutoSearch)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                return response;
            });
        }
    }
}
