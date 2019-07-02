using System.Collections.Generic;

namespace Communication.DAL.Entities
{
    public class UserEntity : EntityBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string TelephoneNumber { get; set; }
        public byte[] Photo { get; set; }
        public bool isEnabled { get; set; }
        public virtual ICollection<GroupMemberEntity> GroupMembers { get; set; }
        public virtual ICollection<ContributionEntity> Contirbutions { get; set; }
        public virtual ICollection<CommentEntity> Comments { get; set; }
        public virtual ICollection<ReactionEntity> Reactions { get; set; }
        private sealed class UserTestComparer : IEqualityComparer<UserEntity>
        {
            public bool Equals(UserEntity x, UserEntity y)
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
                return string.Equals(x.Name, y.Name) && string.Equals(x.Surname, y.Surname) && x.Id.Equals(y.Id) && string.Equals(x.Email, y.Email) && string.Equals(x.Password, y.Password) && string.Equals(x.TelephoneNumber, y.TelephoneNumber);
            }

            public int GetHashCode(UserEntity obj)
            {
                unchecked
                {
                    var hashCode = (obj.Name != null ? obj.Name.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (obj.Surname != null ? obj.Surname.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ obj.Id.GetHashCode();
                    return hashCode;
                }
            }
        }

        public static IEqualityComparer<UserEntity> UserTest { get; } = new UserTestComparer();
    }
}