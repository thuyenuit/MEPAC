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
    public interface IInventoryDeliveryVoucherRepository : IRepository<InventoryDeliveryVoucher>
    {

    }

    public class InventoryDeliveryVoucherRepository : RepositoryBase<InventoryDeliveryVoucher>, IInventoryDeliveryVoucherRepository
    {

        public InventoryDeliveryVoucherRepository(IDbFactory dbFactory) :
            base(dbFactory)
        {

        }


    }
}
