using System;


namespace Communication.BL.Messages
{
    public class NewContributionMessage : IMessage
    {
        public Guid Id { get; set; }
    }
}
