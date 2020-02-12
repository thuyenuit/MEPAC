using SMS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Service.IServices
{
    public interface IMenuService
    {
        IQueryable<Menu> GetAll();
    }
}
