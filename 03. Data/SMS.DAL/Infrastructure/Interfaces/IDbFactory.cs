using SMS.DAL.DbContext;
using System;

namespace SMS.DAL.Infrastructure.Interfaces
{
    public interface IDbFactory : IDisposable
    {
        SMSDbContext Init();
    }
}