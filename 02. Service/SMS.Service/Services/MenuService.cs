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
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository menuRepository;
        private readonly IUnitOfWork unitOfWork;

        public MenuService(
            IMenuRepository menuRepository,
            IUnitOfWork unitOfWork)
        {
            this.menuRepository = menuRepository;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<Menu> GetAll()
        {
            return menuRepository.GetAll;
        }
    }
}
