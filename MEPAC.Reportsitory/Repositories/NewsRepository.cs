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
    public interface INewsRepository : IRepository<News>
    {
    }

    public class NewsRepository : RepositoryBase<News>, INewsRepository
    {
        public NewsRepository(IDbFactory dbFactory) :
            base(dbFactory)
        {

        }

    }
}
