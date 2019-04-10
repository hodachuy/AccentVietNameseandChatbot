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

        void Save();
    }
    public class SettingService : ISettingService
    {
        ISettingRepository _settingRepository;
        IUnitOfWork unitOfWork;
        public SettingService(ISettingRepository settingRepository, IUnitOfWork unitOfWork)
        {
            _settingRepository = settingRepository;
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
    }
}
