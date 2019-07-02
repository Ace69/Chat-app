using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.BL.Messages
{
    public class AddedMemberMessage : IMessage
    {
        public Guid Id { get; set; }
    }
}
