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
    public interface IPartnerBusiness
    {
        IQueryable<Partner> GetAll();
    }

    public class PartnerBusiness : IPartnerBusiness
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PartnerBusiness(
            IPartnerRepository _partnerRepository,
            IUnitOfWork _unitOfWork)
        {
            this._partnerRepository = _partnerRepository;
            this._unitOfWork = _unitOfWork;
        }

        public IQueryable<Partner> GetAll()
        {
            return _partnerRepository.GetAll();
        }

    }
}
