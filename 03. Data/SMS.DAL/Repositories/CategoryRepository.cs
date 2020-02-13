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
    // định nghĩa thêm các method cần thêm không có sẵn trong RepositoryBase
    public interface ICategoryRepository : IRepository<Category>
    {
        //IQueryable<ProductCategory> Search(IDictionary<string, object> dic);
        IEnumerable<Category> GetByAlias(string alias);
    }

    public class CategoryRepository: RepositoryBase<Category>, ICategoryRepository
    {

        public CategoryRepository(IDbFactory dbFactory) :
            base(dbFactory)
        {

        }

        /*public IEnumerable<ProductCategory> GetAllProductCategory()
        {
            return DbContext.ProductCategory;
        }*/

        public IEnumerable<Category> GetByAlias(string alias)
        {
            return DbContext.Categories.Where(x => x.Alias == alias);
        }
    }
}
