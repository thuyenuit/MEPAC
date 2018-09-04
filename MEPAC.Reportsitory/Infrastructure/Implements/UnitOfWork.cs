using MEPAC.Reportsitory.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEPAC.Reportsitory.Infrastructure.Implements
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private MEPACDbContext dbContext;

        public UnitOfWork(IDbFactory _dbFactory)
        {
            this.dbFactory = _dbFactory;
        }

        public MEPACDbContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}
