using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotProject.Data.Infrastructure;
using BotProject.Data.Repositories;
using BotProject.Model.Models;

namespace BotProject.Service
{
    public interface ISettingService
    {
        Setting Create(Setting Setting);
        void Update(Setting Setting);
        Setting GetSettingByBotID(int botId);
        Setting GetSettingByFbPageID(string pageFbId);
        Setting GetSettingByZaloPageID(string pageZaloId);
        SystemConfig CreateKeyConfig(SystemConfig system);
        void DeleteConfigByBotID(int botId);
        IEnumerable<SystemConfig> GetListSystemConfigByBotId(int botId);
        void Save();
    }
    public class SettingService : ISettingService
    {
        ISettingRepository _settingRepository;
        ISystemConfigRepository _systemConfigRepository;
        IUnitOfWork unitOfWork;
        public SettingService(ISettingRepository settingRepository,
                              IUnitOfWork unitOfWork,
                              ISystemConfigRepository systemConfigRepository)
        {
            _settingRepository = settingRepository;
            _systemConfigRepository = systemConfigRepository;
            this.unitOfWork = unitOfWork;
        }
        public Setting Create(Setting Setting)
        {
            return _settingRepository.Add(Setting);
        }

        public Setting GetSettingByBotID(int botId)
        {
            return _settingRepository.GetSingleByCondition(x=>x.BotID == botId);
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public void Update(Setting Setting)
        {
            _settingRepository.Update(Setting);
        }

        public SystemConfig CreateKeyConfig(SystemConfig system)
        {
            return _systemConfigRepository.Add(system);
        }

        public void DeleteConfigByBotID(int botId)
        {
            _systemConfigRepository.DeleteMulti(x=>x.BotID == botId);
        }

        public IEnumerable<SystemConfig> GetListSystemConfigByBotId(int botId)
        {
            return _systemConfigRepository.GetMulti(x => x.BotID == botId);
        }

        public Setting GetSettingByFbPageID(string pageFbId)
        {
            throw new NotImplementedException();
        }

        public Setting GetSettingByZaloPageID(string pageZaloId)
        {
            throw new NotImplementedException();
        }
    }
}
