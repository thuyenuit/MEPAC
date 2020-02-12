using SMS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DTO.CategoryType.Resquest
{
    public class CTAddOrEditRequest<T> : BaseRequest
    {
        public T Model { get; set; }
    }
}
