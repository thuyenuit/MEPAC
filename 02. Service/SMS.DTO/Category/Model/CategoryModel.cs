using SMS.DTO.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DTO.Category.Model
{
    public class CategoryModel
    {
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Alias { get; set; }
        public byte[] Icon { get; set; }
        public int Sequence { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        public bool IsActive { get; set; }
        public bool HomeFlag { get; set; }
        //public DateTime CreateDate { get; set; }
        //public string CreateBy { get; set; }
        //public DateTime? ModifiedDate { get; set; }
        //public string UpdateBy { get; set; }
    }
}
