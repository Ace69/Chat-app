using Communication.DAL.Enums;


namespace Communication.BL.Models
{
    public class GroupMemberModel : ModelBase
    {
        public UserModel User { get; set; }
        public GroupModel Group { get; set; }
        public PermissionEnum Permission { get; set; }
    }
}
