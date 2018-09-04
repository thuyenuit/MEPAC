using MEPAC.Reportsitory.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEPAC.Reportsitory.Infrastructure.Implements
{
    public class DbFactory : Disposable, IDbFactory
    {
        private MEPACDbContext dbContext;

        public MEPACDbContext Init()
        {
            return dbContext ?? (dbContext = new MEPACDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
