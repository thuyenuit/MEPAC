using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MEPAC.Web.Models
{
    public class SlideViewModel
    {
        public int SlideID { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string FullNameCreate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public string FullNameUpdate { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
    }
}