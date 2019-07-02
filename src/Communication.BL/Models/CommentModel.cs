using System;

namespace Communication.BL.Models
{
    public class CommentModel : ModelBase
    {
        public UserModel User { get; set; }
        public ContributionModel Contribution { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }
        public ReactionModel Reaction { get; set; }
    }
}
