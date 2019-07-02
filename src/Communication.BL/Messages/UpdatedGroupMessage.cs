using System;

namespace Communication.BL.Messages
{
    public class UpdatedGroupMessage :IMessage
    {
        public Guid Id { get; set; }
    }
}
