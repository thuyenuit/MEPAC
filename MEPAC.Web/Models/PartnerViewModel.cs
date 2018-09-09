using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MEPAC.Web.Models
{
    public class PartnerViewModel
    {
        public string Address { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string Display { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Logo { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }
        public int Order { get; set; }
        public int PartID { get; set; }
        public string Phone { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Website { get; internal set; }
    }
}