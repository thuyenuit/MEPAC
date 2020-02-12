using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DTO.Base
{
    public class BaseResponse<T>
    {
        public BaseResponse() { }

        public int ResponseCode { get; set; }
        public string Message { get; set; }
        public string MsgType { get; set; }
        public T Data { get; set; }
    }
}
