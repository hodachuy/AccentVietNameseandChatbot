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
    public interface IApplicationZaloUserService
    {
        ApplicationZaloUser GetByUserId(string userId);
        ApplicationZaloUser Add(ApplicationZaloUser user);
        void Update(ApplicationZaloUser user);
        bool CheckExistUser(string userId);
        void Save();
    }
    public class ApplicationZaloUserService : IApplicationZaloUserService
    {
        IApplicationZaloUserRepository _appFacebookRepository;
        IUnitOfWork _unitOfWork;
        public ApplicationZaloUserService(IUnitOfWork unitOfWork, IApplicationZaloUserRepository appFacebookRepository)
        {
            _unitOfWork = unitOfWork;
            _appFacebookRepository = appFacebookRepository;
        }

        public ApplicationZaloUser Add(ApplicationZaloUser user)
        {
            return _appFacebookRepository.Add(user);
        }

        public bool CheckExistUser(string userId)
        {
            var userDb = _appFacebookRepository.GetSingleByCondition(x => x.UserId == userId);
            if (userDb == null)
                return false;
            return true;
        }

        public ApplicationZaloUser GetByUserId(string userId)
        {
            return _appFacebookRepository.GetSingleByCondition(x => x.UserId == userId);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ApplicationZaloUser user)
        {
            _appFacebookRepository.Update(user);
        }
    }
}
