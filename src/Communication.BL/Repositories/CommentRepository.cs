using Communication.BL.Mappers;
using Communication.BL.Models;
using Communication.DAL;
using Communication.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Communication.BL.Repositories
{
    public class CommentRepository : GenericRepository<CommentEntity, CommentModel>
    {
        public CommentRepository(CommunicationDbContext dbContext) : base(dbContext, new CommentMapper())
        {

        }

        public ICollection<CommentModel> GetCommentByUserId(Guid userId)
        {
            var query = dbSet.Include(p => p.User).Where(p => p.User.Id == userId);
            var list = query.ToList();
            return mapper.MapList(list);
        }

        public ICollection<CommentModel> GetAllComments()
        {
            var query = dbSet.Include(p => p.Contribution).Include(p => p.User);
            var list = query.ToList();
            return mapper.MapList(list);
        }

        public ICollection<CommentModel> GetAllCommentsOfContributionById(Guid contributionId)
        {
            DbSet<CommentEntity> Commentars = dbContext.Comments;
            DbSet<ContributionEntity> Contributions = dbContext.Contributions;

            var query = from com in Commentars
                join c in Contributions
                    on com.Contribution.Id equals c.Id
                where com.Contribution.Id == contributionId
                orderby com.Time descending 
                select com;
            query = query.Include(p => p.User);
            var list = query.AsEnumerable().Select(p => new CommentEntity()
            {
                Id = p.Id,
                User = p.User,
                Contribution = p.Contribution,
                Message = p.Message,
                Time = p.Time,
                Reaction = p.Reaction
            }).ToList();

            var q1 = mapper.MapList(list);

            return mapper.MapList(list);
        }
    }
}
