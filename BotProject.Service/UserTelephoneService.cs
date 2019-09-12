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
    public interface IUserTelephoneService
    {
        UserTelePhone GetByID(int id);
        void Update(UserTelePhone userTelephone);
        UserTelePhone Create(UserTelePhone userTelephone);
        bool CheckIsReceivedByVoucher(int mdVoucherId);
        bool CheckIsPhoneNumberExistByVourcher(int mdVoucherId);
        void Save();
    }
    public class UserTelephoneService : IUserTelephoneService
    {
        IUserTelephoneRepository _userTelephoneRepository;
        IUnitOfWork _unitOfWork;

        public UserTelephoneService(IUnitOfWork unitOfWork, IUserTelephoneRepository userTelephoneRepository)
        {
            _unitOfWork = unitOfWork;
            _userTelephoneRepository = userTelephoneRepository;
        }

        public UserTelePhone GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(UserTelePhone userTelephone)
        {
            throw new NotImplementedException();
        }

        public UserTelePhone Create(UserTelePhone userTelephone)
        {
            throw new NotImplementedException();
        }

        public bool CheckIsReceivedByVoucher(int mdVoucherId)
        {
            throw new NotImplementedException();
        }

        public bool CheckIsPhoneNumberExistByVourcher(int mdVoucherId)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
