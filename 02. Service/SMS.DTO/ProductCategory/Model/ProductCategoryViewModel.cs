using SMS.DTO.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DTO.ProductCategory.Model
{
    public class ProductCategoryViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }       
        public int Sequence { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public bool HomeFlag { get; set; }
    }
}
