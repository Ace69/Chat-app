using Communication.BL.Mappers;
using Communication.BL.Models;
using Communication.DAL;
using Communication.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Communication.BL.Repositories
{
    public class ContributionRepository : GenericRepository<ContributionEntity,ContributionModel>
    {
        public ContributionRepository(CommunicationDbContext dbContext) : base(dbContext, new ContributionMapper())
        {

        }

        public ICollection<ContributionModel> GetContributionsByUserId(Guid userId)
        {
            var query = dbSet.Include(p => p.User).Where(p => p.User.Id == userId);
            var list = query.ToList();
            return mapper.MapList(list);
        }

        public ICollection<ContributionModel> GetAllContributions()
        {
            var query = dbSet;
            var list = query.ToList();
            return mapper.MapList(list);
        }

        public ICollection<ContributionModel> GetAllContributionsIncludeUser()
        {
            var query = dbSet.Include(p => p.User).OrderBy(p => p.Time);
            var list = query.ToList();
            return mapper.MapList(list);
        }
        public ICollection<ContributionModel> GetAllContributionsInGroupById(Guid groupId)
        {
            DbSet<GroupEntity> Groups = dbContext.Groups;
            DbSet<ContributionEntity> Contributions = dbContext.Contributions;

            var query = from g in Groups
                        join c in Contributions
                            on g.Id equals c.Group.Id
                        where c.Group.Id == groupId
                        orderby c.Time descending
                        select c;
            query = query.Include(p => p.User).Include(p => p.Comments);
            var list = query.AsEnumerable().Select(p => new ContributionEntity()
            {
                Id = p.Id,
                User = p.User,
                Title = p.Title,
                Time = p.Time,
                Message = p.Message,
                Comments = p.Comments,
                Group = p.Group,
                Reactions = p.Reactions
            }).ToList();

            return mapper.MapList(list);
        }

        public void InsertAttach(ContributionModel model)
        {
            dbSet.Add(mapper.ModelToEntity(model));
            //dbContext.SaveChanges();
            //dbContext.Set<ContributionEntity>().Local.ToList().ForEach(p => dbContext.Entry(p).State = EntityState.Detached);
            //if (model.User != null)
            //{
            //    dbContext.Set<UserEntity>().Local.ToList().ForEach(p => dbContext.Entry(p).State = EntityState.Detached);
            //}

        }
        public void UpdateAttach(ContributionModel model)
        {
            dbSet.Update(mapper.ModelToEntity(model));
            dbContext.SaveChanges();
            dbContext.Set<ContributionEntity>().Local.ToList().ForEach(p => dbContext.Entry(p).State = EntityState.Detached);
            if(model.User != null)
            {
                dbContext.Set<UserEntity>().Local.ToList().ForEach(p => dbContext.Entry(p).State = EntityState.Detached);
            }

        }
    }
}
