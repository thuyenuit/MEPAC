using SMS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Service.IServices
{
    public interface IProductCategoryService
    {
        void Create(ProductCategory entity);
        void Update(ProductCategory entity);
        IQueryable<ProductCategory> GetAll();
        void SaveChanges();
    }
}
