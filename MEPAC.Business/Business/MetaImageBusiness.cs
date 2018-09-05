using MEPAC.Reportsitory.Infrastructure.Interfaces;
using MEPAC.Reportsitory.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEPAC.Model.Models;

namespace MEPAC.Business.Business
{
    public interface IMetaImageBusiness
    {
        IQueryable<MetaImage> GetAll();
        void Create(MetaImage objImage);
        void Update(MetaImage objImage);
        void Delete(int Id);
        void Save();
    }

    public class MetaImageBusiness : IMetaImageBusiness
    {
        private readonly IMetaImageRepository _imageRepository;
        private readonly IUnitOfWork _unitOfWork;
        public MetaImageBusiness(
            IMetaImageRepository _imageRepository,
            IUnitOfWork _unitOfWork)
        {
            this._imageRepository = _imageRepository;
            this._unitOfWork = _unitOfWork;
        }

        public void Create(MetaImage objImage)
        {
            _imageRepository.Create(objImage);
        }

        public void Delete(int Id)
        {
            _imageRepository.Delete(Id);
        }

        public IQueryable<MetaImage> GetAll()
        {
            return _imageRepository.GetAll();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(MetaImage objImage)
        {
            _imageRepository.Update(objImage);
        }
    }
}
