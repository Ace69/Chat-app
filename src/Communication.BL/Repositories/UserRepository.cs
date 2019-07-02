using System;
using System.Collections.Generic;
using System.Linq;
using Communication.BL.Mappers;
using Communication.BL.Models;
using Communication.DAL;
using Communication.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Communication.BL.Repositories
{
    public class UserRepository : GenericRepository<UserEntity, UserModel>
    {
        public UserRepository(CommunicationDbContext dbContext) : base(dbContext, new UserMapper())
        {

        }
        public ICollection<UserModel> GetUsersByUsername(string username)
        {
            var query = dbSet.Where(p => p.Name == username).AsNoTracking();
            var list = query.ToList();
            return mapper.MapList(list);
        }
        public UserModel GetUserByEmail(string email)
        {
            var query = dbSet.AsNoTracking().SingleOrDefault(p => p.Email == email);
            return mapper.EntityToModel(query);
        }

        public ICollection<UserModel> GetMembers()
        {
            var query = dbSet.ToList();
            return mapper.MapList(query);
        }


        public ICollection<UserModel> GetAllUsersOfGroupByGroupId(Guid groupId)
        {
            DbSet<UserEntity> Users = dbContext.Users;
            DbSet<GroupMemberEntity> GroupMembers = dbContext.GroupMembers;

            var query = from gm in GroupMembers
                join u in Users
                    on gm.User.Id equals u.Id
                where gm.Group.Id == groupId
                select u;

            var list = query.AsEnumerable().Select(p => new UserEntity()
            {
                Id = p.Id,
                Name = p.Name,
                Surname = p.Surname,
                Email = p.Email,
                Password = p.Password,
                TelephoneNumber = p.TelephoneNumber,
                Photo = p.Photo,
                isEnabled = p.isEnabled,
                GroupMembers = p.GroupMembers,
                Contirbutions = p.Contirbutions,
                Comments = p.Comments,
                Reactions = p.Reactions
            }).ToList();

            return mapper.MapList(list);
        }

    }
}
