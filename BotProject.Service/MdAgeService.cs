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
    public interface IMdAgeService
    {
        MdAge GetByBotID(int botId);
        MdAge Create(MdAge module);
        void Update(MdAge module);
        void Save();
    }
    public class MdAgeService : IMdAgeService
    {
        IMdAgeRepository _mdAgeRepository;
        IUnitOfWork _unitOfWork;
        public MdAgeService(IUnitOfWork unitOfWork, IMdAgeRepository mdAgeRepository)
        {
            _unitOfWork = unitOfWork;
            _mdAgeRepository = mdAgeRepository;
        }
        public MdAge Create(MdAge module)
        {
            return _mdAgeRepository.Add(module);
        }

        public MdAge GetByBotID(int botId)
        {
            return _mdAgeRepository.GetSingleByCondition(x => x.BotID == botId);
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(MdAge module)
        {
            _mdAgeRepository.Update(module);
        }
    }
}
