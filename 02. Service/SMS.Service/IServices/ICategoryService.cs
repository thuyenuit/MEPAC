using SMS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Service.IServices
{
    public interface ICategoryService
    {
        void Create(Category entity);
        void Update(Category entity);
        IQueryable<Category> GetAll();
        void SaveChanges();
    }
}
