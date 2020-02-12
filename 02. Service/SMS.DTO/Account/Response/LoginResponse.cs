using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DTO.Account.Response
{
    public class LoginResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public TokenResponse Data { get; set; }
    }
}
