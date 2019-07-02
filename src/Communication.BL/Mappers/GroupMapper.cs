using Communication.BL.Models;
using Communication.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.BL.Mappers
{
    class GroupMapper : AbstractMapper<GroupEntity, GroupModel>
    {
        public override GroupModel EntityToModel(GroupEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new GroupModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Photo = entity.Photo
            };
        }

        public override GroupEntity ModelToEntity(GroupModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new GroupEntity
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Photo = model.Photo
            };
        }
    }
}
