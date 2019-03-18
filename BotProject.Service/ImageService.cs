using BotProject.Data.Infrastructure;
using BotProject.Data.Repositories;
using BotProject.Model.Models;
using System.Collections.Generic;
using System;

namespace BotProject.Service
{
    public interface IImageService
    {
        Image Add(ref Image file);
        Image Delete(int id);
        void Update(Image file);
        void DeleteMutiImage(int cardID);
        IEnumerable<Image> GetByCardID(int cardID);
        void Save();
    }
    public class ImageService : IImageService
    {
        IImageRepository _imageRepository;
        IUnitOfWork _unitOfWork;
        public ImageService(IImageRepository imageRepository, IUnitOfWork unitOfWork)
        {
            _imageRepository = imageRepository;
            _unitOfWork = unitOfWork;

        }
        public Image Add(ref Image image)
        {
            try
            {
                _imageRepository.Add(image);
                _unitOfWork.Commit();
                return image;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Image Delete(int id)
        {
            return _imageRepository.Delete(id);
        }

        public void DeleteMutiImage(int cardID)
        {
            _imageRepository.DeleteMulti(x => x.CardID == cardID);
        }

        public IEnumerable<Image> GetByCardID(int cardID)
        {
            return _imageRepository.GetMulti(x => x.CardID == cardID);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Image file)
        {
            _imageRepository.Update(file);
        }
    }
}
