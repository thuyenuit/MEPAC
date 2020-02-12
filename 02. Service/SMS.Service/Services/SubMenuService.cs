using SMS.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Model.Models;
using SMS.DAL.Repositories;
using SMS.DAL.Infrastructure.Interfaces;

namespace SMS.Service.Services
{
    public class SubMenuService : ISubMenuService
    {
        private readonly ISubMenuRepository subMenuRepository;
        private readonly IUnitOfWork unitOfWork;

        public SubMenuService(
            ISubMenuRepository subMenuRepository,
            IUnitOfWork unitOfWork)
        {
            this.subMenuRepository = subMenuRepository;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<SubMenu> GetAll()
        {
            return subMenuRepository.GetAll;
        }
    }
}
