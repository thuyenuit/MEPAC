using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DTO.ProductCategory.Resquest
{
    public class CSearchRequest
    {
        public string Keyword { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}
