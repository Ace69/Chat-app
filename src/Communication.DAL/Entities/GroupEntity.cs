using System.Collections.Generic;

namespace Communication.DAL.Entities
{
    public class GroupEntity : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }
        public virtual ICollection<GroupMemberEntity> GroupMembers { get; set; }
        public virtual ICollection<ContributionEntity> Contributions { get; set; }

        private sealed class GroupTestComparer : IEqualityComparer<GroupEntity>
        {
            public bool Equals(GroupEntity x, GroupEntity y)
            {
                if (ReferenceEquals(x, y))
                {
                    return true;
                }

                if (ReferenceEquals(x, null))
                {
                    return false;
                }

                if (ReferenceEquals(y, null))
                {
                    return false;
                }

                if (x.GetType() != y.GetType())
                {
                    return false;
                }
                return string.Equals(x.Name, y.Name) && string.Equals(x.Description, y.Description);
            }

            public int GetHashCode(GroupEntity obj)
            {
                unchecked
                {
                    var hashCode = (obj.Name != null ? obj.Name.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (obj.Name != null ? obj.Name.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ obj.Id.GetHashCode();
                    return hashCode;
                }
            }
        }

        public static IEqualityComparer<GroupEntity> GroupTest { get; } = new GroupTestComparer();
    }
}