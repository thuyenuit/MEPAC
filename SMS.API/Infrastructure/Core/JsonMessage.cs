using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMS.Shared.Shares;

namespace SMS.API.Infrastructure.Core
{
    public class JsonMessage
    {
        //public JsonMessage()
        //{
        //}

        public string Type { get; set; }
        public string Message { get; set; }

        public JsonMessage(string message = SystemParam.MESSAGE_ADD_SUCCESS, string type = SystemParam.SUCCESS)
        {
            this.Type = type;
            this.Message = message;
        }
    }
}