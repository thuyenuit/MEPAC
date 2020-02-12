using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DTO.Base
{
    public class BusinessException: Exception
    {
        public string MessageKey;
        public List<object> Params;
        public BusinessException()
            : base()
        {
            // Add implementation (if required)
        }

        public BusinessException(string message)
            : base(message)
        {
            // Add implementation (if required)
            this.MessageKey = message;
            this.Params = new List<object>();
        }

        public BusinessException(string message, System.Exception inner)
            : base(message, inner)
        {
            // Add implementation (if required)
        }

        protected BusinessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Add implementation (if required)
        }

        public BusinessException(string MessageKey, List<object> Params)
        {
            this.MessageKey = MessageKey;
            this.Params = Params;
        }

        public BusinessException(string MessageKey, params object[] Params)
        {
            this.MessageKey = MessageKey;
            this.Params = Params.ToList();
        }
    }
}
