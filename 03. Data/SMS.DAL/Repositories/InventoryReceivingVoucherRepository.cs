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
    public interface IInventoryReceivingVoucherRepository : IRepository<InventoryReceivingVoucher>
    {

    }

    public class InventoryReceivingVoucherRepository : RepositoryBase<InventoryReceivingVoucher>, IInventoryReceivingVoucherRepository
    {

        public InventoryReceivingVoucherRepository(IDbFactory dbFactory) :
            base(dbFactory)
        {

        }


    }
}
