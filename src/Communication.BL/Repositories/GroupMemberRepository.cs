using Communication.BL.Mappers;
using Communication.BL.Models;
using Communication.DAL;
using Communication.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace Communication.BL.Repositories
{
    public class GroupMemberRepository : GenericRepository<GroupMemberEntity,GroupMemberModel>
    {
        public GroupMemberRepository(CommunicationDbContext dbContext) : base(dbContext, new GroupMemberMapper())
        {

        }

        public ICollection<GroupMemberModel> GetGroupMemberByIDS(Guid userID, Guid groupID)
        {
            DbSet<GroupEntity> Groups = dbContext.Groups;
            DbSet<UserEntity> Users = dbContext.Users;
            DbSet<GroupMemberEntity> GroupMembers = dbContext.GroupMembers;


            var query = from gm in GroupMembers
                        where gm.User.Id == userID && gm.Group.Id == groupID
                        select gm;

            var model = query.AsEnumerable().Select(p => new GroupMemberEntity()
            {

                Id = p.Id,
                Group = p.Group,
                User = p.User,
                Permission = p.Permission
            }).ToList();

            return mapper.MapList(model);
        }


    }
}
