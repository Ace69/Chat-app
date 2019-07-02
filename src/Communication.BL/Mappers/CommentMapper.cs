using Communication.BL.Models;
using Communication.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.BL.Mappers
{
    class CommentMapper : AbstractMapper<CommentEntity, CommentModel>
    {
        public override CommentModel EntityToModel(CommentEntity entity)
        {
            if(entity == null)
            {
                return null;
            }

            return new CommentModel
            {
                Id = entity.Id,
                Message = entity.Message,
                Time = entity.Time,
                User = new UserMapper().EntityToModel(entity.User),
                Contribution = new ContributionMapper().EntityToModel(entity.Contribution),
                Reaction = new ReactionMapper().EntityToModel(entity.Reaction),
            }; 
        }

        public override CommentEntity ModelToEntity(CommentModel model)
        {
            if(model == null)
            {
                return null;
            }

            return new CommentEntity
            {
                Id = model.Id,
                Message = model.Message,
                Time = model.Time,
                User = new UserMapper().ModelToEntity(model.User),
                Contribution = new ContributionMapper().ModelToEntity(model.Contribution),
                Reaction = new ReactionMapper().ModelToEntity(model.Reaction),
            };
        }
    }
}
