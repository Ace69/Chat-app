using Communication.DAL.Enums;


namespace Communication.BL.Models
{
    public class ReactionModel : ModelBase
    {
        public ContributionModel Contribution { get; set; }
        public UserModel User { get; set; }
        public ReactionTypeEnum ReactionType { get; set; }
    }
}
