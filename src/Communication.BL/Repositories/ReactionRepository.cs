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
    public class ReactionRepository : GenericRepository<ReactionEntity,ReactionModel>
    {
        public ReactionRepository(CommunicationDbContext dbContext) : base(dbContext, new ReactionMapper())
        {

        }

        public ICollection<ReactionModel> getReactionByUserId(Guid userId)
        {
            List<ReactionEntity> list = dbSet.Include(p => p.User).Where(p => p.User.Id == userId).ToList();
            return mapper.MapList(list);
        }
    }
}
