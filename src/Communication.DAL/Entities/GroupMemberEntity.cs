using System;
using System.Collections.Generic;
using System.Text;
using Communication.DAL.Enums;

namespace Communication.DAL.Entities
{
    public class GroupMemberEntity : EntityBase
    {
        public virtual UserEntity User { get; set; }
        public virtual GroupEntity Group { get; set; }
        public PermissionEnum Permission { get; set; }
    }
}
