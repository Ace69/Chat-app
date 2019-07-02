using System;
using System.Collections.Generic;
using System.Text;
using Communication.BL.Models;
using Communication.BL.Repositories;
using Communication.DAL.Enums;
using Xunit;

namespace Communication.BL.Tests
{
    public class GroupMemberRespositoryTests
    {
        [Fact]
        public void InsertGroupMember()
        {
            GroupMemberRepository groupMemberRepository = new GroupMemberRepository(new InMemoryDbContextFactory().CreateDbContext());
            GroupModel groupModel = new GroupModel
            {
                Id = Guid.NewGuid(),
                Name = "První cenová skupina",
                Description = "Popisek testovací skupiny První cenová skupiina",
            };

            UserModel userModel = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                Surname = "Vlach",
                Email = "xxxxx@vut.pl",
                Password = "heslo123_bude_hash",
                TelephoneNumber = "+420420420420"
            };

            GroupMemberModel groupMemberModel = new GroupMemberModel
            {
                Id = Guid.NewGuid(),
                Group = groupModel,
                User = userModel,
                Permission = PermissionEnum.Moderator
            };

            groupMemberRepository.Insert(groupMemberModel);
            var groupMemberRepositoryResponse = groupMemberRepository.GetById(groupMemberModel.Id);
            Assert.NotNull(groupMemberRepositoryResponse);
            Assert.Equal(userModel.Name, groupMemberModel.User.Name);
            Assert.Equal(groupModel.Name, groupMemberModel.Group.Name);
        }

        [Fact]
        public void DeleteGroupMember()
        {
            GroupMemberRepository groupMemberRepository = new GroupMemberRepository(new InMemoryDbContextFactory().CreateDbContext());
            GroupModel groupModel = new GroupModel
            {
                Id = Guid.NewGuid(),
                Name = "První cenová skupina",
                Description = "Popisek testovací skupiny První cenová skupiina",
            };

            UserModel userModel = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                Surname = "Vlach",
                Email = "xxxxx@vut.pl",
                Password = "heslo123_bude_hash",
                TelephoneNumber = "+420420420420"
            };

            GroupMemberModel groupMemberModel = new GroupMemberModel
            {
                Id = Guid.NewGuid(),
                Group = groupModel,
                User = userModel,
                Permission = PermissionEnum.Moderator
            };

            groupMemberRepository.Insert(groupMemberModel);
            var groupMemberRepositoryResponse = groupMemberRepository.GetById(groupMemberModel.Id);
            Assert.NotNull(groupMemberRepositoryResponse);
            GroupMemberRepository groupMemberRepository2 = new GroupMemberRepository(new InMemoryDbContextFactory().CreateDbContext());
            groupMemberRepository2.Delete(groupMemberModel);
            var groupMemberRepositoryResponse2 = groupMemberRepository2.GetById(groupMemberModel.Id);
            Assert.Null(groupMemberRepositoryResponse2);
        }
    }
}
