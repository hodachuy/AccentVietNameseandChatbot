using BotProject.Common.Exceptions;
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
    public interface IAttributeSystemService
    {
        IEnumerable<AttributeSystem> GetListAttributeSystemByBotId(int botId);
        AttributeSystem GetById(int Id);
        AttributeSystem Create(AttributeSystem attr);
        void Update(AttributeSystem attr);
        void Delete(int Id);

        // Attribute Facebook
        IEnumerable<AttributeFacebookUser> GetListAttributeFacebook(string userId, int botId);
        AttributeFacebookUser CreateUpdateAttributeFacebook(AttributeFacebookUser attr);
        bool CheckExistAttributeFacebook(string userId, int botId, string key);

        // Attribute Zalo
        AttributeZaloUser CreateUpdateAttributeZalo(AttributeZaloUser attr);
        IEnumerable<AttributeZaloUser> GetListAttributeZalo(string userId, int botId);
        bool CheckExistAttributeZalo(string userId, int botId, string key);

        // Attribute Platform User
        AttributePlatformUser CreateUpdateAttributePlatform(AttributePlatformUser attr);
        IEnumerable<AttributePlatformUser> GetListAttributePlatform(string userId, int botId);
        bool CheckExistAttributePlatform(string userId, int botId, string key);

        void Save();
    }
    public class AttributeSystemService : IAttributeSystemService
    {
        IAttributeSystemRepository _attributeRepository;
        IAttributeFacebookUserRepository _attFacebookRepository;
        IAttributeZaloUserRepository _attZaloRepository;
        IAttributePlatformUserRepository _attPlatformRepository;
        IUnitOfWork _unitOfWork;
        public AttributeSystemService(IAttributeSystemRepository attributeRepository,
                                      IAttributeFacebookUserRepository attFacebookRepository,
                                      IAttributePlatformUserRepository attPlatformRepository,
                                      IAttributeZaloUserRepository attZaloRepository,
                                      IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _attributeRepository = attributeRepository;
            _attZaloRepository = attZaloRepository;
            _attFacebookRepository = attFacebookRepository;
            _attPlatformRepository = attPlatformRepository;
        }

        public AttributeSystem Create(AttributeSystem attr)
        {
            if (_attributeRepository.CheckContains(x => x.Name == attr.Name && x.BotID == attr.BotID))
                throw new NameDuplicatedException("Tên không được trùng");
            return _attributeRepository.Add(attr);
        }

        public void Delete(int Id)
        {
            _attributeRepository.Delete(Id);
        }

        public AttributeSystem GetById(int Id)
        {
            return _attributeRepository.GetSingleById(Id);
        }

        public IEnumerable<AttributeSystem> GetListAttributeSystemByBotId(int botId)
        {
            return _attributeRepository.GetMulti(x => x.BotID == botId);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(AttributeSystem attr)
        {
            _attributeRepository.Update(attr);
        }

        public IEnumerable<AttributeFacebookUser> GetListAttributeFacebook(string userId, int botId)
        {
            return _attFacebookRepository.GetMulti(x => x.UserID == userId && x.BotID == botId);
        }

        public AttributeFacebookUser CreateUpdateAttributeFacebook(AttributeFacebookUser attr)
        {
            AttributeFacebookUser attDb = new AttributeFacebookUser();
            attDb = _attFacebookRepository.GetSingleByCondition(x => x.AttributeKey == attr.AttributeKey && x.BotID == attr.BotID && x.UserID == attr.UserID);
            if(attDb != null)
            {
                attDb.AttributeValue = attr.AttributeValue;
                _attFacebookRepository.Update(attDb);
                _unitOfWork.Commit();
                return attDb;
            }
            else {
                _attFacebookRepository.Add(attr);
                _unitOfWork.Commit();
            } 
            return attr;
        }

        public AttributeZaloUser CreateUpdateAttributeZalo(AttributeZaloUser attr)
        {
            AttributeZaloUser attDb = new AttributeZaloUser();
            attDb = _attZaloRepository.GetSingleByCondition(x => x.AttributeKey == attr.AttributeKey
                                                            && x.BotID == attr.BotID
                                                            && x.UserID == attr.UserID);
            if(attDb != null)
            {
                attDb.AttributeValue = attr.AttributeValue;
                _attZaloRepository.Update(attDb);
                _unitOfWork.Commit();
                return attDb;
            }
            else
            {
                _attZaloRepository.Add(attr);
                _unitOfWork.Commit();
            }
            return attr;
        }

        public IEnumerable<AttributeZaloUser> GetListAttributeZalo(string userId, int botId)
        {
            return _attZaloRepository.GetMulti(x => x.UserID == userId && x.BotID == botId);
        }

        public bool CheckExistAttributeFacebook(string userId, int botId, string key)
        {
            var attDb = _attFacebookRepository.GetSingleByCondition(x => x.AttributeKey == key && x.BotID == botId && x.UserID == userId);
            if (attDb == null) return false;
            return true;
        }

        public bool CheckExistAttributeZalo(string userId, int botId, string key)
        {
            var attDb = _attZaloRepository.GetSingleByCondition(x => x.AttributeKey == key && x.BotID == botId && x.UserID == userId);
            if (attDb == null) return false;
            return true;
        }

        public AttributePlatformUser CreateUpdateAttributePlatform(AttributePlatformUser attr)
        {
            AttributePlatformUser attDb = new AttributePlatformUser();
            attDb = _attPlatformRepository.GetSingleByCondition(x => x.AttributeKey == attr.AttributeKey
                                                            && x.BotID == attr.BotID
                                                            && x.UserID == attr.UserID);
            if (attDb != null)
            {
                attDb.AttributeValue = attr.AttributeValue;
                _attPlatformRepository.Update(attDb);
                _unitOfWork.Commit();
                return attDb;
            }
            else
            {
                _attPlatformRepository.Add(attr);
                _unitOfWork.Commit();
            }
            return attr;
        }

        public IEnumerable<AttributePlatformUser> GetListAttributePlatform(string userId, int botId)
        {
            return _attPlatformRepository.GetMulti(x => x.UserID == userId && x.BotID == botId);
        }

        public bool CheckExistAttributePlatform(string userId, int botId, string key)
        {
            var attDb = _attPlatformRepository.GetSingleByCondition(x => x.AttributeKey == key && x.BotID == botId && x.UserID == userId);
            if (attDb == null) return false;
            return true;
        }
    }
}
