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
    public interface IMdVoucherService
    {
        MdVoucher GetByBotID(int botId);
        MdVoucher Create(MdVoucher module);
        void Update(MdVoucher module);
        void Save();
    }
    public class MdVoucherService : IMdVoucherService
    {
        IMdVoucherRepository _mdVoucherRepository;
        IUnitOfWork _unitOfWork;
        public MdVoucherService(IUnitOfWork unitOfWork, IMdVoucherRepository mdVoucherRepository)
        {
            _unitOfWork = unitOfWork;
            _mdVoucherRepository = mdVoucherRepository;
        }
        public MdVoucher Create(MdVoucher module)
        {
            return _mdVoucherRepository.Add(module);
        }

        public MdVoucher GetByBotID(int botId)
        {
            return _mdVoucherRepository.GetSingleByCondition(x => x.BotID == botId);
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(MdVoucher module)
        {
            _mdVoucherRepository.Update(module);
        }
    }
}
