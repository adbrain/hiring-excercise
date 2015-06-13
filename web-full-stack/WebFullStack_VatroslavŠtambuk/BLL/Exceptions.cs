using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class EndpointUnresponsiveException : Exception
    {
        public EndpointUnresponsiveException()
        {
        }
        public EndpointUnresponsiveException(string message)
            : base(message)
        {
        }
        public EndpointUnresponsiveException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
