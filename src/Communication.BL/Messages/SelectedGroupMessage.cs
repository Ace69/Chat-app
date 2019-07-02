using System;


namespace Communication.BL.Messages
{
    public class SelectedGroupMessage : IMessage
    {
        public Guid Id { get; set; }
    }
}
