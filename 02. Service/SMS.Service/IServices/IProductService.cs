using SMS.Model.Models;
using SMS.Service.ServiceObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Service.IServices
{
    public interface IProductService
    {
        void Create(ProductSO entity);
        void Update(ProductSO entity);
        IQueryable<Product> GetAll();
        IQueryable<ProductSO> Search(IDictionary<string, object> dic);
        void SaveChanges();
    }
}
