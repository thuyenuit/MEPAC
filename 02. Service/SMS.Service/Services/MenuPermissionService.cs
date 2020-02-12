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
    public class MenuPermissionService : IMenuPermissionService
    {
        private readonly IMenuPermissionRepository menuPermissionRepository;
        private readonly IUnitOfWork unitOfWork;

        public MenuPermissionService(
            IMenuPermissionRepository menuPermissionRepository,
            IUnitOfWork unitOfWork)
        {
            this.menuPermissionRepository = menuPermissionRepository;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<MenuPermission> GetAll()
        {
            return menuPermissionRepository.GetAll;
        }
    }
}
