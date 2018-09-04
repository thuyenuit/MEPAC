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
    public interface ISlideBusiness
    {
        IQueryable<Slide> GetAll();
    }

    public class SlideBusiness : ISlideBusiness
    {
        private readonly ISlideRepository _slideRepository;
        private readonly IUnitOfWork _unitOfWork;
        public SlideBusiness(
            ISlideRepository _slideRepository,
            IUnitOfWork _unitOfWork)
        {
            this._slideRepository = _slideRepository;
            this._unitOfWork = _unitOfWork;
        }

        public IQueryable<Slide> GetAll()
        {
            return _slideRepository.GetAll();
        }

    }
}
