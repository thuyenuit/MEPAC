using SMS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Service.IServices
{
    public interface IApplicationUserService
    {
        void Create(ApplicationUser entity);
        void Update(ApplicationUser entity);
        IQueryable<ApplicationUser> GetAll(IDictionary<string, object> dic);
        void SaveChanges();
    }
}
