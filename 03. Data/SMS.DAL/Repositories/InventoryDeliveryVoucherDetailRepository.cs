﻿using SMS.DAL.Infrastructure.Implements;
using SMS.DAL.Infrastructure.Interfaces;
using SMS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DAL.Repositories
{

    public interface IInventoryDeliveryVoucherDetailRepository : IRepository<InventoryDeliveryVoucherDetail>
    {

    }

    public class InventoryDeliveryVoucherDetailRepository : RepositoryBase<InventoryDeliveryVoucherDetail>, IInventoryDeliveryVoucherDetailRepository
    {

        public InventoryDeliveryVoucherDetailRepository(IDbFactory dbFactory) :
            base(dbFactory)
        {

        }


    }
}
