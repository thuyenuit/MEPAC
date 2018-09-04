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
    public interface IRangeBusiness
    {
        IQueryable<Range> GetAll();
    }

    public class RangeBusiness : IRangeBusiness
    {
        private readonly IRangeRepository _rangeRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RangeBusiness(
            IRangeRepository _rangeRepository,
            IUnitOfWork _unitOfWork)
        {
            this._rangeRepository = _rangeRepository;
            this._unitOfWork = _unitOfWork;
        }

        public IQueryable<Range> GetAll()
        {
            return _rangeRepository.GetAll();
        }

    }
}
