using System;


namespace Communication.DAL.Entities
{
    public class CommentEntity : EntityBase
    {
        public virtual UserEntity User { get; set; }
        public virtual ContributionEntity Contribution { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }
        public virtual ReactionEntity Reaction { get; set; }
    }
}
