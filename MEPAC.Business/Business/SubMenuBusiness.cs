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
        SubMenu GetById(int id);
        SubMenu Create(SubMenu objInpput);
        void Update(SubMenu objInpput);
        void Save();
        void Delete(int Id);
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

        public SubMenu GetById(int id)
        {
            return _subMenuRepository.GetSingleById(id);
        }

        public SubMenu Create(SubMenu objInpput)
        {
            return _subMenuRepository.Create(objInpput);
        }

        public void Update(SubMenu objInpput)
        {
            _subMenuRepository.Update(objInpput);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Delete(int Id)
        {
            _subMenuRepository.Delete(Id);
        }

    }
}
