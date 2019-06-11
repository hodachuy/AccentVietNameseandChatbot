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
    public interface IModuleFollowCardService
    {
        ModuleFollowCard Add(ref ModuleFollowCard mdFollowCard);
        ModuleFollowCard Delete(int id);
        void Update(ModuleFollowCard mdFollowCard);
        void DeleteMutiModuleFollowCard(int cardID);
        IEnumerable<ModuleFollowCard> GetByCardID(int cardID);
        void Save();
    }
    public class ModuleFollowCardService : IModuleFollowCardService
    {
        IModuleFollowCardRepository _moduleFollowCardRepository;
        IUnitOfWork _unitOfWork;
        public ModuleFollowCardService(IModuleFollowCardRepository moduleFollowCardRepository, IUnitOfWork unitOfWork)
        {
            _moduleFollowCardRepository = moduleFollowCardRepository;
            _unitOfWork = unitOfWork;

        }
        public ModuleFollowCard Add(ref ModuleFollowCard ModuleFollowCard)
        {
            try
            {
                _moduleFollowCardRepository.Add(ModuleFollowCard);
                _unitOfWork.Commit();
                return ModuleFollowCard;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ModuleFollowCard Delete(int id)
        {
            return _moduleFollowCardRepository.Delete(id);
        }

        public void DeleteMutiModuleFollowCard(int cardID)
        {
            _moduleFollowCardRepository.DeleteMulti(x => x.CardID == cardID);
        }

        public IEnumerable<ModuleFollowCard> GetByCardID(int cardID)
        {
            return _moduleFollowCardRepository.GetMulti(x => x.CardID == cardID);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ModuleFollowCard mdFollowCard)
        {
            _moduleFollowCardRepository.Update(mdFollowCard);
        }
    }
}
