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
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;

        public ApplicationUserService(
            IApplicationUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }


        public void Create(ApplicationUser entity)
        {
            userRepository.Create(entity);
        }

        public IQueryable<ApplicationUser> GetAll(IDictionary<string, object> dic)
        {
            string fullName = null;
            string userCode = null;
            string keyword = null;

            IQueryable<ApplicationUser> queryUser = userRepository.GetAll;

            if (!string.IsNullOrEmpty(fullName))
                queryUser = queryUser.Where(x => fullName.ToUpper().Contains(x.FullName.ToUpper()));

            if (!string.IsNullOrEmpty(userCode))
                queryUser = queryUser.Where(x => userCode.ToUpper().Contains(x.UserCode.ToUpper()));

            if (!string.IsNullOrEmpty(keyword))
                queryUser = queryUser.Where(x => keyword.ToUpper().Contains(x.UserCode.ToUpper()) 
                                             || keyword.ToUpper().Contains(x.FullName.ToUpper()));

            return queryUser;
        }

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }

        public void Update(ApplicationUser entity)
        {
            //var objEntity = userRepository.GetSingleById(entity.id);
        }
    }
}
