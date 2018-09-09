using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MEPAC.Web.Models
{
    public class HiringViewModel
    {
        public string Content { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? EnddDate { get; set; }
        public int HiringID { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }
        public string Position { get; set; }
        public string PostBy { get; set; }
        public DateTime? PostDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsShow { get; set; }
        public string LinkImage { get; set; }
    }
}