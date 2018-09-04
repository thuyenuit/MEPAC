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

        public IQueryable<MetaImage> GetAll()
        {
            return _imageRepository.GetAll();
        }

    }
}
