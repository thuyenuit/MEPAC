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
    public interface IRangeRepository : IRepository<Range>
    {
    }

    public class RangeRepository : RepositoryBase<Range>, IRangeRepository
    {
        public RangeRepository(IDbFactory dbFactory) :
            base(dbFactory)
        {

        }

    }
}
