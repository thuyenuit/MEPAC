using MEPAC.Model.Models;
using MEPAC.Reportsitory.Infrastructure.Interfaces;
using MEPAC.Reportsitory.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEPAC.Business.Business
{
    public interface IApplicationUserBusiness
    {
        IQueryable<ApplicationUser> GetAll();
        ApplicationUser GetSingleById(string Id);
        ApplicationUser GetSingleByUserCode(string userCode);
    }

    public class ApplicationUserBusiness : IApplicationUserBusiness
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ApplicationUserBusiness(
            IApplicationUserRepository _applicationUserRepository,
            IUnitOfWork _unitOfWork)
        {
            this._applicationUserRepository = _applicationUserRepository;
            this._unitOfWork = _unitOfWork;
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            return _applicationUserRepository.GetAll();
        }

        public ApplicationUser GetSingleById(string Id)
        {
            return _applicationUserRepository.GetSingleById(Id);
        }

        public ApplicationUser GetSingleByUserCode(string userCode)
        {
            return _applicationUserRepository.GetSingleByUserCode(userCode);
        }
    }
}
