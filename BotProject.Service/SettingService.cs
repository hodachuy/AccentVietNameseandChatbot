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
        SystemConfig CreateKeyConfig(SystemConfig system);
        void UpdateKeyConfig(SystemConfig system);
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

        public void UpdateKeyConfig(SystemConfig system)
        {
            _systemConfigRepository.Update(system);
        }
    }
}
