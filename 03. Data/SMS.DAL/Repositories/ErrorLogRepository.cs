using SMS.DAL.Infrastructure.Interfaces;
using SMS.DAL.Infrastructure.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Model.Models;

namespace SMS.DAL.Repositories
{
    public interface IErrorLogRepository : IRepository<ErrorLog>
    {

    }

    public class ErrorLogRepository : RepositoryBase<ErrorLog>, IErrorLogRepository
    {

        public ErrorLogRepository(IDbFactory dbFactory) :
            base(dbFactory)
        {

        }


    }
}
