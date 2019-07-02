using Communication.DAL.Enums;

namespace Communication.DAL.Entities
{
    public class ReactionEntity : EntityBase
    {
        public virtual ContributionEntity Contribution { get; set; }
        public virtual UserEntity User { get; set; }
        public virtual ReactionTypeEnum ReactionType { get; set; }
    }
}
