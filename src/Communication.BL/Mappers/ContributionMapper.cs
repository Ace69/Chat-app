using Communication.BL.Models;
using Communication.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.BL.Mappers
{
    public class ContributionMapper : AbstractMapper<ContributionEntity, ContributionModel>
    {
        public override ContributionModel EntityToModel(ContributionEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new ContributionModel
            {
                Id = entity.Id,
                Title = entity.Title,
                Time = entity.Time,
                Message = entity.Message,
                User = new UserMapper().EntityToModel(entity.User),
                Group = new GroupMapper().EntityToModel(entity.Group)
            };
        }

        public override ContributionEntity ModelToEntity(ContributionModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new ContributionEntity
            {
                Id = model.Id,
                Title = model.Title,
                Time = model.Time,
                Message = model.Message,
                User = new UserMapper().ModelToEntity(model.User),
                Group = new GroupMapper().ModelToEntity(model.Group)
            };
        }
    }
}
