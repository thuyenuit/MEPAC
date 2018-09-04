using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEPAC.Reportsitory.Infrastructure.Interfaces
{
    public interface IDbFactory : IDisposable
    {
        MEPACDbContext Init();
    }
}
