using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DTO.Base
{
    public class BaseCode
    {

        public const int SUCCESS = 200;
        public const int UN_AUTHORIZED = 401;
        public const int BAD_REQUEST = 400;
        public const int NOT_FOUND = 404;
        public const int VALIDATE_ERROR = 1;
        public const int CRUD_ERROR = 2;

        public const string SUCCESS_TYPE = "success";
        public const string ERROR_TYPE = "error";
    }
}
