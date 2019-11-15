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
    public interface IFileDocumentService
    {
        FileDocument Add(ref FileDocument file);
        FileDocument Delete(int id);
        void Update(FileDocument file);
        void DeleteMutiFile(int cardID);
        IEnumerable<FileDocument> GetByCardID(int cardID);
        void Save();
    }
    public class FileDocumentService : IFileDocumentService
    {
        IFileDocumentRepository _fileDocumentRepository;
        IUnitOfWork _unitOfWork;
        public FileDocumentService(IFileDocumentRepository fileDocumentRepository, IUnitOfWork unitOfWork)
        {
            _fileDocumentRepository = fileDocumentRepository;
            _unitOfWork = unitOfWork;
        }

        public FileDocument Add(ref FileDocument file)
        {
            try
            {
                _fileDocumentRepository.Add(file);
                _unitOfWork.Commit();
                return file;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public FileDocument Delete(int id)
        {
            return _fileDocumentRepository.Delete(id);
        }

        public void Update(FileDocument file)
        {
            _fileDocumentRepository.Update(file);
        }

        public void DeleteMutiFile(int cardID)
        {
            _fileDocumentRepository.DeleteMulti(x => x.CardID == cardID);
        }

        public IEnumerable<FileDocument> GetByCardID(int cardID)
        {
            return _fileDocumentRepository.GetMulti(x => x.CardID == cardID);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
