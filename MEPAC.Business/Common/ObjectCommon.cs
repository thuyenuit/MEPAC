using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEPAC.Business.Common
{
    public class ObjectCommon
    {
        public List<Status> lstStatus = new List<Status>();
    }

    public class Status
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }
    }
}
