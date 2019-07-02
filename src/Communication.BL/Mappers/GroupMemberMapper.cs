using Communication.BL.Models;
using Communication.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.BL.Mappers
{
    public class GroupMemberMapper : AbstractMapper<GroupMemberEntity, GroupMemberModel>
    {
        public override GroupMemberModel EntityToModel(GroupMemberEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new GroupMemberModel
            {
                Id = entity.Id,
                User = new UserMapper().EntityToModel(entity.User),
                Group = new GroupMapper().EntityToModel(entity.Group),
                Permission = entity.Permission
            };
        }

        public override GroupMemberEntity ModelToEntity(GroupMemberModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new GroupMemberEntity
            {
                Id = model.Id,
                User = new UserMapper().ModelToEntity(model.User),
                Group = new GroupMapper().ModelToEntity(model.Group),
                Permission = model.Permission
            };
        }
    }
}
