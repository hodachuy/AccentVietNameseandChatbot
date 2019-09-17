using BotProject.Common.ViewModels;
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
        IEnumerable<StoreProcUserTelephoneByVoucherViewModel> GetUserTelephoneByVoucher(string filter, string sort, int pageNumber, int pageSize, long? selectedID);
        UserTelePhone GetByPhoneAndMdVoucherId(string phoneNumber, int mdVoucherId);
        void Update(UserTelePhone userTelephone);
        UserTelePhone Create(UserTelePhone userTelephone);
        bool CheckIsReceivedVoucher(string phoneNumber, int mdVoucherId);
        bool CheckIsPhoneNumberExistByVourcher(string phoneNumber, int mdVoucherId);
        bool CheckContainOTP(string OTP, string phoneNumber, int mdVoucherId);
        UserTelePhone GetById(int id);
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

        public UserTelePhone GetByPhoneAndMdVoucherId(string phoneNumber, int mdVoucherId)
        {
            return _userTelephoneRepository.GetSingleByCondition(x => x.TelephoneNumber.Contains(phoneNumber) && x.MdVoucherID == mdVoucherId);
		}

        public void Update(UserTelePhone userTelephone)
        {
			_userTelephoneRepository.Update(userTelephone);
        }

        public UserTelePhone Create(UserTelePhone userTelephone)
        {
            return _userTelephoneRepository.Add(userTelephone);
		}

        public IEnumerable<StoreProcUserTelephoneByVoucherViewModel> GetUserTelephoneByVoucher(string filter, string sort, int pageNumber, int pageSize, long? selectedID)
        {
            return _userTelephoneRepository.GetUserTelephoneByVoucher(filter, sort, pageNumber, pageSize, selectedID);
        }

        public bool CheckIsReceivedVoucher(string phoneNumber, int mdVoucherId)
        {
			return _userTelephoneRepository.CheckContains(x => x.TelephoneNumber.Contains(phoneNumber) && x.MdVoucherID == mdVoucherId && x.IsReceive == true);
        }

        public bool CheckIsPhoneNumberExistByVourcher(string phoneNumber, int mdVoucherId)
        {
			return _userTelephoneRepository.CheckContains(x => x.TelephoneNumber.Contains(phoneNumber) && x.MdVoucherID == mdVoucherId && x.IsReceive == false);
		}

        public void Save()
        {
			_unitOfWork.Commit();
        }

        public bool CheckContainOTP(string OTP, string phoneNumber, int mdVoucherId)
        {
            return _userTelephoneRepository.CheckContains(x => x.TelephoneNumber.Contains(phoneNumber) && x.MdVoucherID == mdVoucherId && x.Code.Contains(OTP));

        }

        public UserTelePhone GetById(int id)
        {
            return _userTelephoneRepository.GetSingleById(id);
        }
    }
}
