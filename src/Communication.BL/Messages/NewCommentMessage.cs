using System;

namespace Communication.BL.Messages
{
    public class NewCommentMessage : IMessage
    {
        public Guid Id { get; set; }
    }
}
