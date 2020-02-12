using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DTO.Base
{
    public class BasePaginationSet<T>
    {
        public int PageSize { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public List<T> Items { get; set; }

        public int ShowFrom
        {
            get { return Items.Count > 0 ? ((Page - 1) * PageSize) + 1 : 0; }
        }
        public int ShowTo
        {
            get { return Items.Count > 0 ? ((Page) * PageSize > TotalItems ? TotalItems : ((Page) * PageSize)) : 0; }
        }

        //public string StrDate { get; set; }
        //public string StrHour { get; set; }
        //public string StrUser { get; set; }
    }
}
