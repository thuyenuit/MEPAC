using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MEPAC.Web.Models
{
    public class RangeViewModel
    {
        public int RangeID { get; set; }

        public int SubMenuID { get; set; }

        public string Cotntent { get; set; }
        public string SubMenuName { get; set; }
        public string IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public string LinkImage { get; set; }
        public string MenuName { get; set; }
        public int MenuID { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        public int OrderBy { get; set; }
    }
}