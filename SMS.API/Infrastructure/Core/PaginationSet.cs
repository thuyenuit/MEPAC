using System.Collections.Generic;
using System.Linq;

namespace SMS.API.Infrastructure.Core
{
    public class PaginationSet<T>
    {
        public int PageSize { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<T> Items { get; set; }

        public int ShowFrom
        {
            get { return Items.Count() > 0 ? (Page * PageSize) + 1 : 0; }
        }
        public int ShowTo
        {
            get { return Items.Count() > 0 ? ((Page + 1) * PageSize > TotalCount ? TotalCount : ((Page + 1) * PageSize)) : 0; }
        }

        //public string StrDate { get; set; }
        //public string StrHour { get; set; }
        //public string StrUser { get; set; }
    }
}