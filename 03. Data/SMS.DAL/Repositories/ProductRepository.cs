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
    public interface IProductRepository : IRepository<Product>
    {
        IQueryable<Product> Search(IDictionary<string, object> dic);
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {

        public ProductRepository(IDbFactory dbFactory) :
            base(dbFactory)
        {

        }

        public IQueryable<Product> Search(IDictionary<string, object> dic)
        {
            return null;
            /*int productId = Utils.GetInt(dic, "ProductID");
            string productName = Utils.GetString(dic, "ProductName");
            string productCode = Utils.GetString(dic, "ProductCode");
            string alias = Utils.GetString(dic, "ProductAlias");
            string description = Utils.GetString(dic, "Description");
            int quantity = Utils.GetInt(dic, "Quantity");
            decimal productPrice = Utils.GetDecimal(dic, "Price");
            int producerID = Utils.GetInt(dic, "ProducerID");
            int categoryID = Utils.GetInt(dic, "CategoryID");
            int status = Utils.GetInt(dic, "Status");

            IQueryable<Product> query = DbContext.Product;

            if (status == 1)
                query = query.Where(x => x.IsActive == true);
            else if (status == 2)
                query = query.Where(x => x.IsActive == false);

            if (producerID > 0)
                query = query.Where(x => x.ProducerID == producerID);
            if (categoryID > 0)
                query = query.Where(x => x.CategoryID == categoryID);
            if (productId > 0)
                query = query.Where(x => x.ProductID == productId);
            if (!string.IsNullOrEmpty(productName))
                query = query.Where(x => x.ProductName.ToUpper().Contains(productName.ToUpper()));
            if (!string.IsNullOrEmpty(productCode))
                query = query.Where(x => x.ProductCode.ToUpper().Contains(productCode.ToUpper()));
            if (!string.IsNullOrEmpty(alias))
                query = query.Where(x => x.Alias.ToUpper().Contains(alias.ToUpper()));
            if (!string.IsNullOrEmpty(description))
                query = query.Where(x => !string.IsNullOrEmpty(x.Description) 
                && x.Description.ToUpper().Contains(description.ToUpper()));
            if (quantity > 0)
                query = query.Where(x => x.Quantity == quantity);
            if (productPrice != 0)
                query = query.Where(x => x.PriceSell == productPrice);
            //if (!string.IsNullOrEmpty(keyword))
            //    query = query.Where(x => x.ProductCode.ToUpper().Contains(keyword.ToUpper()) 
            //                    || x.ProductName.ToUpper().Contains(keyword.ToUpper())
            //                    || x.Alias.ToUpper().Contains(keyword.ToUpper()));

            return query;*/
        }
    }
}
