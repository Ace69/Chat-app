using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.BL.Exceptions
{
    public class CommunicationException : System.Exception
    {
        public CommunicationException() : base() { }
        public CommunicationException(string message) : base(message) { }
    }
}
