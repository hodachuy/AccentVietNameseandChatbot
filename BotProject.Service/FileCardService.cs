using BotProject.Data.Infrastructure;
using BotProject.Data.Repositories;
using BotProject.Model.Models;
using System.Collections.Generic;
using System;

namespace BotProject.Service
{
    public interface IFileCardService
    {
        FileCard Add(ref FileCard file);
        FileCard Delete(int id);
        void Update(FileCard file);
        void DeleteMutiFileCard(int cardID);
        IEnumerable<FileCard> GetByCardID(int cardID);
        void Save();
    }
    public class FileCardService : IFileCardService
    {
        IFileCardRepository _FileCardRepository;
        IUnitOfWork _unitOfWork;
        public FileCardService(IFileCardRepository FileCardRepository, IUnitOfWork unitOfWork)
        {
            _FileCardRepository = FileCardRepository;
            _unitOfWork = unitOfWork;

        }
        public FileCard Add(ref FileCard FileCard)
        {
            try
            {
                _FileCardRepository.Add(FileCard);
                _unitOfWork.Commit();
                return FileCard;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public FileCard Delete(int id)
        {
            return _FileCardRepository.Delete(id);
        }

        public void DeleteMutiFileCard(int cardID)
        {
            _FileCardRepository.DeleteMulti(x => x.CardID == cardID);
        }

        public IEnumerable<FileCard> GetByCardID(int cardID)
        {
            return _FileCardRepository.GetMulti(x => x.CardID == cardID);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(FileCard file)
        {
            _FileCardRepository.Update(file);
        }
    }
}
