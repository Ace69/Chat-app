using Communication.BL.Mappers;
using Communication.BL.Models;
using Communication.DAL;
using Communication.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace Communication.BL.Repositories
{
    public class GroupRepository : GenericRepository<GroupEntity,GroupModel>
    {
        public GroupRepository(CommunicationDbContext dbContext) : base(dbContext, new GroupMapper())
        {
            
        }

        public ICollection<GroupModel> GetAllGroups()
        {
            var query = dbSet;
            var list = query.ToList();
            return mapper.MapList(list);
        }

        public ICollection<GroupModel> GetAllGroupsByEmail(string email)
        {
            DbSet<GroupEntity> Groups = dbContext.Groups;
            DbSet<GroupMemberEntity> GroupMembers = dbContext.GroupMembers;

            var query = from g in Groups
                join gm in GroupMembers
                    on g.Id equals gm.Group.Id
                where gm.User.Email == email
                select g;

            var list = query.AsEnumerable().Select(p => new GroupEntity()
            {

                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Photo = p.Photo,
                Contributions = p.Contributions,
                GroupMembers = p.GroupMembers
            }).ToList();

            return mapper.MapList(list);
        }
    }
}
