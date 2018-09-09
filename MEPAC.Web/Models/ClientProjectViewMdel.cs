using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MEPAC.Web.Models
{
    public class ClientProjectViewMdel
    {
        public int ProjectID { get; set; }
        public string Display { get; set; }
        public string LinkImage { get; set; }
        public string Year { get; set; }
        public string MetaDescription { get; set; }
        public bool IsComplete { get; set; }
    }

    public class ClientYearViewModel
    {
        public int YearID { get; set; }
        public string Display { get; set; }
        public bool Selected { get; set; }
    }
}