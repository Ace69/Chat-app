using System;


namespace Communication.BL.Messages
{
    public class DeletedGroupMessage : IMessage
    {
        public Guid Id { get; set; }
    }
}
