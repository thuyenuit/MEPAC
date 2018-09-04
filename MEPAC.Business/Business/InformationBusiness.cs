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
    public interface IInformationBusiness
    {
        IQueryable<Information> GetAll();
    }

    public class InformationBusiness : IInformationBusiness
    {
        private readonly IInformationRepository _informationRepository;
        private readonly IUnitOfWork _unitOfWork;
        public InformationBusiness(
            IInformationRepository _informationRepository,
            IUnitOfWork _unitOfWork)
        {
            this._informationRepository = _informationRepository;
            this._unitOfWork = _unitOfWork;
        }

        public IQueryable<Information> GetAll()
        {
            return _informationRepository.GetAll();
        }

    }
}
