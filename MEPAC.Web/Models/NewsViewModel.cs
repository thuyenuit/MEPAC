using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MEPAC.Web.Models
{
    public class NewsViewModel
    {
        public int NewsID { get; set; }
        public string DisplayName { get; set; }
        public int SubMenu { get; set; }
        public string Content { get; set; }
        public string ShortContent { get; set; }
        public bool IsActive { get; set; }  
        public string Image { get; set; }
        public int SubMenuID { get; set; }
        public DateTime? PostDate { get; set; }
        public string PostBy { get; set; }
        public string FullNamePost { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string FullNameCreate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string FullNameUpdate { get; set; }
        public string UpdateBy { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
    }
}