using System;

namespace Communication.BL.Messages
{
    public class AddedGroupMessage : IMessage
    {
        public Guid Id { get; set; }
    }
}
