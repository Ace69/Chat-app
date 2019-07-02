using Communication.BL.Models;
using Communication.BL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Communication.BL.Mappers;
using Communication.DAL.Enums;
using Xunit;

namespace Communication.BL.Tests
{
    public class GroupRepositoryTests
    {
        [Fact]
        public void InsertGroup()
        {
            GroupRepository groupRepository = new GroupRepository(new InMemoryDbContextFactory().CreateDbContext());

            GroupModel groupModel = new GroupModel
            {
                Id = Guid.NewGuid(),
                Name = "První cenová skupina",
                Description = "Popisek testovací skupiny První cenová skupina",
            };

            groupRepository.Insert(groupModel);
            var groupRepositoryResponse = groupRepository.GetById(groupModel.Id);
            Assert.NotNull(groupRepositoryResponse);
            UserModel userModel = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                Surname = "Vlach",
                Email = "xxxxx@vut.pl",
                Password = "heslo123_bude_hash",
                TelephoneNumber = "+420420420420",
            };
        }

        [Fact]
        public void GetAllGroupsOfUser()
        {
            InMemoryDbContextFactory Db = new InMemoryDbContextFactory();
            var dbContext = Db.CreateDbContext();

            GroupMemberRepository groupMemberRepository = new GroupMemberRepository(dbContext);
            GroupRepository groupRepository = new GroupRepository(dbContext);

            GroupModel groupModel1 = new GroupModel
            {
                Id = Guid.NewGuid(),
                Name = "První skupina",
                Description = "Popisek1",
            };

            GroupModel groupModel2 = new GroupModel
            {
                Id = Guid.NewGuid(),
                Name = "Druhá skupina",
                Description = "Popisek2",
            };

            GroupModel groupModel3 = new GroupModel
            {
                Id = Guid.NewGuid(),
                Name = "Třetí skupina",
                Description = "Popisek3",
            };

            UserModel userModel1 = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = "Karel",
                Surname = "Vlach",
                Email = "az@vut.pl",
                Password = "heslo123_bude_hash",
                TelephoneNumber = "+420420420420",
            };

            UserModel userModel2 = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = "Pavel",
                Surname = "Vlach",
                Email = "xyz@vut.pl",
                Password = "heslo123_bude_hash",
                TelephoneNumber = "+420420420420",
            };

            UserModel userModel3 = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                Surname = "Vlach",
                Email = "abc@vut.pl",
                Password = "heslo123_bude_hash",
                TelephoneNumber = "+420420420420",
            };


            groupRepository.Insert(groupModel1);
            groupRepository.Insert(groupModel2);
            groupRepository.Insert(groupModel3);

            var groupRepositoryResponse1 = groupRepository.GetById(groupModel1.Id);
            var groupRepositoryResponse2 = groupRepository.GetById(groupModel2.Id);
            var groupRepositoryResponse3 = groupRepository.GetById(groupModel3.Id);

            Assert.NotNull(groupRepositoryResponse1);
            Assert.NotNull(groupRepositoryResponse2);
            Assert.NotNull(groupRepositoryResponse3);


            GroupMemberModel groupMemberModel1 = new GroupMemberModel
            {
                Id = Guid.NewGuid(),
                Group = groupModel1,
                User = userModel1,
                Permission = PermissionEnum.User
            };

            GroupMemberModel groupMemberModel2 = new GroupMemberModel
            {
                Id = Guid.NewGuid(),
                Group = groupModel2,
                User = userModel2,
                Permission = PermissionEnum.User
            };

            GroupMemberModel groupMemberModel3 = new GroupMemberModel
            {
                Id = Guid.NewGuid(),
                Group = groupModel2,
                User = userModel1,
                Permission = PermissionEnum.Admin
            };

            dbContext.DetachAllEntities();

            groupMemberRepository.Insert(groupMemberModel1);
            groupMemberRepository.Insert(groupMemberModel2);
            groupMemberRepository.Insert(groupMemberModel3);

            var groupMemberRepositoryResponse1 = groupMemberRepository.GetById(groupMemberModel1.Id);
            var groupMemberRepositoryResponse2 = groupMemberRepository.GetById(groupMemberModel2.Id);
            var groupMemberRepositoryResponse3 = groupMemberRepository.GetById(groupMemberModel3.Id);

            Assert.NotNull(groupMemberRepositoryResponse1);
            Assert.NotNull(groupMemberRepositoryResponse2);
            Assert.NotNull(groupMemberRepositoryResponse3);

            var groupRepositoryResponseUsers = groupRepository.GetAllGroupsByEmail("az@vut.pl");
            Assert.NotNull(groupRepositoryResponseUsers);
            Assert.Equal(groupRepositoryResponseUsers.ElementAt(0).Id, groupModel1.Id);
            Assert.Equal(groupRepositoryResponseUsers.ElementAt(1).Id, groupModel2.Id);
        }


        [Fact]
        public void DeleteGroup()
        {
            GroupRepository groupRepository = new GroupRepository(new InMemoryDbContextFactory().CreateDbContext());
            GroupModel groupModel = new GroupModel
            {
                Id = Guid.NewGuid(),
                Name = "První cenová skupina",
                Description = "Popisek testovací skupiny První cenová skupina"
            };
            groupRepository.Insert(groupModel);
            var groupRepositoryResponse = groupRepository.GetById(groupModel.Id);
            Assert.NotNull(groupRepositoryResponse);
            GroupRepository groupRepository2 = new GroupRepository(new InMemoryDbContextFactory().CreateDbContext());
            groupRepository2.Delete(groupModel);
            var groupRepositoryResponse2 = groupRepository2.GetById(groupModel.Id);
            Assert.Null(groupRepositoryResponse2);
        }

        [Fact]
        public void Correct_Data_GetById_Group()
        {
            GroupRepository groupRepository = new GroupRepository(new InMemoryDbContextFactory().CreateDbContext());
            GroupModel groupModel1 = new GroupModel
            {
                Id = Guid.NewGuid(),
                Name = "První cenová skupina",
                Description = "Popisek testovací skupiny První cenová skupina"
            };

            GroupModel groupModel2 = new GroupModel
            {
                Id = Guid.NewGuid(),
                Name = "Druhá cenová skupina",
                Description = "Popisek testovací skupiny Druhá cenová skupina"
            };


            GroupModel groupModel3 = new GroupModel
            {
                Id = Guid.NewGuid(),
                Name = "Třetí cenová skupina",
                Description = "Popisek testovací skupiny Třetí cenová skupina"
            };

            groupRepository.Insert(groupModel1);
            groupRepository.Insert(groupModel2);
            groupRepository.Insert(groupModel3);

            var groupRepositoryResponse1 = groupRepository.GetById(groupModel1.Id);
            var groupRepositoryResponse2 = groupRepository.GetById(groupModel2.Id);
            var groupRepositoryResponse3 = groupRepository.GetById(groupModel3.Id);

            Assert.NotNull(groupRepositoryResponse1);
            Assert.NotNull(groupRepositoryResponse2);
            Assert.NotNull(groupRepositoryResponse3);

            Assert.Equal(groupRepositoryResponse1.Id, groupModel1.Id);
            Assert.Equal(groupRepositoryResponse1.Name, groupModel1.Name);
            Assert.Equal(groupRepositoryResponse1.Description, groupModel1.Description);

            Assert.Equal(groupRepositoryResponse2.Id, groupModel2.Id);
            Assert.Equal(groupRepositoryResponse2.Name, groupModel2.Name);
            Assert.Equal(groupRepositoryResponse2.Description, groupModel2.Description);

            Assert.Equal(groupRepositoryResponse3.Id, groupModel3.Id);
            Assert.Equal(groupRepositoryResponse3.Name, groupModel3.Name);
            Assert.Equal(groupRepositoryResponse3.Description, groupModel3.Description);

        }

        [Fact]
        public void UpdateGroup()
        {
            GroupRepository groupRepository = new GroupRepository(new InMemoryDbContextFactory().CreateDbContext());
            GroupModel groupModel = new GroupModel
            {
                Id = Guid.NewGuid(),
                Name = "První cenová skupina",
                Description = "Popisek testovací skupiny První cenová skupina"
            };
            groupRepository.Insert(groupModel);
            var groupRepositoryResponse = groupRepository.GetById(groupModel.Id);
            Assert.Equal(groupRepositoryResponse.Name, groupModel.Name);
            string new_name = "Kurátoři z Moravy";
            groupModel.Name = new_name;
            GroupRepository groupRepository2 = new GroupRepository(new InMemoryDbContextFactory().CreateDbContext());
            groupRepository2.Update(groupModel);
            var groupRepositoryResponse2 = groupRepository2.GetById(groupModel.Id);
            Assert.Equal(groupRepositoryResponse2.Name, new_name);
            Assert.Equal(groupRepositoryResponse2.Description, groupModel.Description);
        }
    }
}
