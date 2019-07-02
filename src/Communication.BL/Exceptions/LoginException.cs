using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.BL.Exceptions
{
    public class LoginException : CommunicationException
    {
        public LoginException() : base() { }
        public LoginException(string message) : base(message) { }
    }
}
