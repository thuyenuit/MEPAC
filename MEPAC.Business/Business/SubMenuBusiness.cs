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
    public interface ISubMenuBusiness
    {
        IQueryable<SubMenu> GetAll();
    }

    public class SubMenuBusiness : ISubMenuBusiness
    {
        private readonly ISubMenuRepository _subMenuRepository;
        private readonly IUnitOfWork _unitOfWork;
        public SubMenuBusiness(
            ISubMenuRepository _subMenuRepository,
            IUnitOfWork _unitOfWork)
        {
            this._subMenuRepository = _subMenuRepository;
            this._unitOfWork = _unitOfWork;
        }

        public IQueryable<SubMenu> GetAll()
        {
            return _subMenuRepository.GetAll();
        }

    }
}
