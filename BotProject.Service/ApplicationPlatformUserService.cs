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
    public interface IApplicationPlatformUserService
    {
        ApplicationPlatformUser GetByUserId(string userId);
        ApplicationPlatformUser Add(ApplicationPlatformUser user);
        void Update(ApplicationPlatformUser user);
        bool CheckExistUser(string userId);
        void Save();
    }

    public class ApplicationPlatformUserService : IApplicationPlatformUserService
    {
        IApplicationPlatformUserRepository _appPlatformUserRepository;
        IUnitOfWork _unitOfWork;
        public ApplicationPlatformUserService(IUnitOfWork unitOfWork, IApplicationPlatformUserRepository appPlatformUserRepository)
        {
            _unitOfWork = unitOfWork;
            _appPlatformUserRepository = appPlatformUserRepository;
        }

        public ApplicationPlatformUser Add(ApplicationPlatformUser user)
        {
            return _appPlatformUserRepository.Add(user);
        }

        public bool CheckExistUser(string userId)
        {
            var userDb = _appPlatformUserRepository.GetSingleByCondition(x => x.UserId == userId);
            if (userDb == null)
                return false;
            return true;
        }

        public ApplicationPlatformUser GetByUserId(string userId)
        {
            return _appPlatformUserRepository.GetSingleByCondition(x => x.UserId == userId);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ApplicationPlatformUser user)
        {
            _appPlatformUserRepository.Update(user);
        }
    }
}
