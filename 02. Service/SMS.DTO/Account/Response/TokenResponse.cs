using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DTO.Account.Response
{
    public class TokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string userId { get; set; }
        public string userName { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string issued { get; set; }
        public string expires { get; set; }
    }
}
