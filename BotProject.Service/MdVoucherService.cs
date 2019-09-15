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
        MdVoucher GetByID(int id);
        IEnumerable<MdVoucher> GetByBotID(int botId);
        MdVoucher Create(MdVoucher module);
        void Update(MdVoucher module);
        void Save();
        void Delete(int id);
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

        public void Delete(int id)
        {
            _mdVoucherRepository.Delete(id);
        }

        public IEnumerable<MdVoucher> GetByBotID(int botId)
        {
            return _mdVoucherRepository.GetMulti(x => x.BotID == botId);
        }

        public MdVoucher GetByID(int id)
        {
            return _mdVoucherRepository.GetSingleById(id);
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
