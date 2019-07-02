using System;


namespace Communication.BL.Messages
{
    public class ContributionSelected : IMessage
    {
        public Guid Id { get; set; }
    }
}
