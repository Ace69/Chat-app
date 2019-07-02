using Communication.BL.Models;
using Communication.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.BL.Mappers
{
    public class ReactionMapper : AbstractMapper<ReactionEntity,ReactionModel>
    {
        public override ReactionModel EntityToModel(ReactionEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new ReactionModel
            {
                Id = entity.Id,
                ReactionType = entity.ReactionType,
                User = new UserMapper().EntityToModel(entity.User),
                Contribution = new ContributionMapper().EntityToModel(entity.Contribution),
            };
        }

        public override ReactionEntity ModelToEntity(ReactionModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new ReactionEntity
            {
                Id = model.Id,
                ReactionType  = model.ReactionType,
                User = new UserMapper().ModelToEntity(model.User),
                Contribution = new ContributionMapper().ModelToEntity(model.Contribution)
            };
        }
    }
}
