using System;
using System.Collections.Generic;

namespace Communication.DAL.Entities
{
    public class ContributionEntity : EntityBase
    {
        public virtual UserEntity User { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }
        public virtual ICollection<CommentEntity> Comments { get; set; }
        public virtual GroupEntity Group { get; set; }
        public virtual ICollection<ReactionEntity> Reactions { get; set; }
    }
}
