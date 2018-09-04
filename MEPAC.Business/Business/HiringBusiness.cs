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

    public interface IHiringBusiness
    {
        IQueryable<Hiring> GetAll();
    }

    public class HiringBusiness : IHiringBusiness
    {
        private readonly IHiringRepository _hiringRepository;
        private readonly IUnitOfWork _unitOfWork;
        public HiringBusiness(
            IHiringRepository _hiringRepository,
            IUnitOfWork _unitOfWork)
        {
            this._hiringRepository = _hiringRepository;
            this._unitOfWork = _unitOfWork;
        }

        public IQueryable<Hiring> GetAll()
        {
            return _hiringRepository.GetAll();
        }

    }
}
