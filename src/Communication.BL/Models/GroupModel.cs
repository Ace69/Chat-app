using System.Collections.Generic;

namespace Communication.BL.Models
{
    public class GroupModel : ModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }
        public ICollection<GroupMemberModel> GroupMembers { get; set; }
        public ICollection<ContributionModel> Contributions { get; set; }

    }
}
