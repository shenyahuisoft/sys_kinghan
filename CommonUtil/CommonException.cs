using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CommonUtil
{
    [Serializable]
    public class CommonException : Exception
    {
        // Methods
        public CommonException()
        {
        }

        public CommonException(string message)
            : base(message)
        {
        }

        protected CommonException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public CommonException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

 

}
