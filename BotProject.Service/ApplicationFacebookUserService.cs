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
    public interface IApplicationFacebookUserService
    {
        ApplicationFacebookUser GetByUserId(string userId);
        ApplicationFacebookUser Add(ApplicationFacebookUser user);
        void Update(ApplicationFacebookUser user);
        bool CheckExistUser(string userId);
        void Save();
        int CheckDuplicateRequestWithTimeStamp(string timeStamp, string userId);
    }
    public class ApplicationFacebookUserService: IApplicationFacebookUserService
    {
        IApplicationFacebookUserRepository _appFacebookRepository;
        IUnitOfWork _unitOfWork;
        public ApplicationFacebookUserService(IUnitOfWork unitOfWork, IApplicationFacebookUserRepository appFacebookRepository)
        {
            _unitOfWork = unitOfWork;
            _appFacebookRepository = appFacebookRepository;
        }

        public ApplicationFacebookUser Add(ApplicationFacebookUser user)
        {
            var userDb = _appFacebookRepository.GetSingleByCondition(x => x.UserId == user.UserId);
            if (userDb == null)
                return _appFacebookRepository.Add(user);
            return userDb;
        }

        public int CheckDuplicateRequestWithTimeStamp(string timeStamp, string userId)
        {
            return _appFacebookRepository.CheckDuplicateRequestWithTimeStamp(timeStamp, userId);
        }

        public bool CheckExistUser(string userId)
        {
            var userDb = _appFacebookRepository.GetSingleByCondition(x => x.UserId == userId);
            if (userDb == null)
                return false;
            return true;
        }

        public ApplicationFacebookUser GetByUserId(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                return null;
            }
            var lstUserDuplicate = _appFacebookRepository.GetMulti(x => x.UserId == userId).ToList();
            if(lstUserDuplicate.Count() == 0)
            {
                return null;
            }
            if (lstUserDuplicate.Count() > 1)
            {
                _appFacebookRepository.Delete(lstUserDuplicate[0]);
                _unitOfWork.Commit();
                return lstUserDuplicate[1];
            }
            return lstUserDuplicate[0];
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ApplicationFacebookUser user)
        {
            _appFacebookRepository.Update(user);
        }
    }
}
