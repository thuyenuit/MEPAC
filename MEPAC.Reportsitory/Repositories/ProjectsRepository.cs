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
    public interface IProjectsRepository : IRepository<Projects>
    {
    }

    public class ProjectsRepository : RepositoryBase<Projects>, IProjectsRepository
    {
        public ProjectsRepository(IDbFactory dbFactory) :
            base(dbFactory)
        {

        }

    }
}
