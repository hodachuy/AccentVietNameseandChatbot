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
    public interface IMdPhoneService
    {
        MdPhone GetByBotID(int botId);
        MdPhone Create(MdPhone module);
        void Update(MdPhone module);
        void Save();
    }
    public class MdPhoneService : IMdPhoneService
    {
        IMdPhoneRepository _mdPhoneRepository;
        IUnitOfWork _unitOfWork;
        public MdPhoneService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public MdPhone Create(MdPhone module)
        {
            return _mdPhoneRepository.Add(module);
        }

        public MdPhone GetByBotID(int botId)
        {
            return _mdPhoneRepository.GetSingleByCondition(x => x.BotID == botId);
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(MdPhone module)
        {
            _mdPhoneRepository.Update(module);
        }
    }
}
