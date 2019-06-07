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
    public interface IMdEmailService
    {
        MdEmail GetByBotID(int botId);
        MdEmail Create(MdEmail module);
        void Update(MdEmail module);
        void Save();
    }
    public class MdEmailService : IMdEmailService
    {
        IMdEmailRepository _mdEmailRepository;
        IUnitOfWork _unitOfWork;
        public MdEmailService(IUnitOfWork unitOfWork, IMdEmailRepository mdEmailRepository)
        {
            _unitOfWork = unitOfWork;
            _mdEmailRepository = mdEmailRepository;
        }
        public MdEmail Create(MdEmail module)
        {
            return _mdEmailRepository.Add(module);
        }

        public MdEmail GetByBotID(int botId)
        {
            return _mdEmailRepository.GetSingleByCondition(x => x.BotID == botId);
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(MdEmail module)
        {
            _mdEmailRepository.Update(module);
        }
    }
}
