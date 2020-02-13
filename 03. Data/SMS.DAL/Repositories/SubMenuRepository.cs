using SMS.DAL.Infrastructure.Implements;
using SMS.DAL.Infrastructure.Interfaces;
using SMS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DAL.Repositories
{
    public interface ISubMenuRepository : IRepository<FieldGroup>
    {

    }

    public class SubMenuRepository : RepositoryBase<FieldGroup>, ISubMenuRepository
    {

        public SubMenuRepository(IDbFactory dbFactory) :
            base(dbFactory)
        {

        }


    }
}
