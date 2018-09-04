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
    public interface ISubMenuRepository : IRepository<SubMenu>
    {
    }

    public class SubMenuRepository : RepositoryBase<SubMenu>, ISubMenuRepository
    {
        public SubMenuRepository(IDbFactory dbFactory) :
            base(dbFactory)
        {

        }

    }
}
