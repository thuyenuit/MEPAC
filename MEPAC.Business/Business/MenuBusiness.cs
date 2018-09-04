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
    public interface IMenuBusiness
    {
        IQueryable<Menu> GetAll();
    }

    public class MenuBusiness : IMenuBusiness
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IUnitOfWork _unitOfWork;
        public MenuBusiness(
            IMenuRepository _menuRepository,
            IUnitOfWork _unitOfWork)
        {
            this._menuRepository = _menuRepository;
            this._unitOfWork = _unitOfWork;
        }

        public IQueryable<Menu> GetAll()
        {
            return _menuRepository.GetAll();
        }

    }
}
