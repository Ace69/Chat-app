using System;
using System.Collections.Generic;


namespace Communication.BL.Models
{
    public class ContributionModel : ModelBase
    {
        public UserModel User { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }
        public GroupModel Group { get; set; }
        public ICollection<CommentModel> Comments { get; set; }
    }
}
