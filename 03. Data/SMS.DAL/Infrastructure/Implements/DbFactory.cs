using SMS.DAL.DbContext;
using SMS.DAL.Infrastructure.Implements;
using SMS.DAL.Infrastructure.Interfaces;

namespace SMS.DAL.Infrastructure.Implements
{
    public class DbFactory : Disposable, IDbFactory
    {
        private SMSDbContext dbContext;

        public SMSDbContext Init()
        {
            return dbContext ?? (dbContext = new SMSDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}