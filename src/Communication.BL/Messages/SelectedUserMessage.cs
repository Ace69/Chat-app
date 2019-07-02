using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.BL.Messages
{
    public class SelectedUserMessage : IMessage
    {
        public Guid Id { get; set; }
    }
}
