using MEPAC.Model.Models;
using MEPAC.Reportsitory.Infrastructure.Implements;
using MEPAC.Reportsitory.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEPAC.Reportsitory.Repositories
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        ApplicationUser GetSingleByUserCode(string userCode);
        ApplicationUser GetSingleById(string Id);
    }

    public class ApplicationUserRepository : RepositoryBase<ApplicationUser>, IApplicationUserRepository
    {

        public ApplicationUserRepository(IDbFactory dbFactory) :
            base(dbFactory)
        {

        }

        public ApplicationUser GetSingleById(string Id)
        {
            return DbContext.Users.Where(x => x.Id == Id).FirstOrDefault();
        }

        public ApplicationUser GetSingleByUserCode(string userCode)
        {
            return DbContext.Users.Where(x => x.UserCode.ToUpper() == userCode.ToUpper()).FirstOrDefault();
        }
    }
}
