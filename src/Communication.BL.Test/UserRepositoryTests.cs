using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Communication.BL.Models;
using Communication.BL.Repositories;
using Communication.DAL.Enums;
using Xunit;

namespace Communication.BL.Tests
{
    public class UserRepositoryTests
    {
        [Fact]
        public void Insert_User()
        {
            UserRepository userRepository = new UserRepository(new InMemoryDbContextFactory().CreateDbContext());
             
            UserModel userModel = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                Surname = "Vlach",
                Email = "xxxxx@vut.pl",
                Password = "heslo123_bude_hash",
                TelephoneNumber = "+420420420420"
            };
            userRepository.Insert(userModel);
            var userRepositoryResponse = userRepository.GetById(userModel.Id);
            Assert.NotNull(userRepositoryResponse);
        }

        [Fact]
        public void Delete_User()
        {
            UserRepository userRepository = new UserRepository(new InMemoryDbContextFactory().CreateDbContext());
            UserModel userModel = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                Surname = "Vlach",
                Email = "xxxxx@vut.pl",
                Password = "heslo123_bude_hash",
                TelephoneNumber = "+420420420420"
            };
            userRepository.Insert(userModel);
            var userRepositoryResponse = userRepository.GetById(userModel.Id);
            Assert.NotNull(userRepositoryResponse);
            userRepositoryResponse = userRepository.GetById(userModel.Id);
            UserRepository userRepository2 = new UserRepository(new InMemoryDbContextFactory().CreateDbContext());
            userRepository2.Delete(userModel);
            var userRepositoryResponse2 = userRepository2.GetById(userModel.Id);
            Assert.Null(userRepositoryResponse2);
        }

        [Fact]
        public void Correct_Data_GetById_User()
        {
            UserRepository userRepository = new UserRepository(new InMemoryDbContextFactory().CreateDbContext());
            UserModel userModel1 = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                Surname = "Vlach",
                Email = "xxxxx@vut.pl",
                Password = "heslo123_bude_hash",
                TelephoneNumber = "+420420420420"
            };
            UserModel userModel2 = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = "Pavel",
                Surname = "Elstak",
                Email = "email@email.com",
                Password = "passwd",
                TelephoneNumber = "+0000000000"
            };

            UserModel userModel3 = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = "Leoš",
                Surname = "Mareš",
                Email = "email@volejbenga.cz",
                Password = "ferrari123",
                TelephoneNumber = "+158158158"
            };

            userRepository.Insert(userModel1);
            userRepository.Insert(userModel2);
            userRepository.Insert(userModel3);

            var userRepositoryResponse1 = userRepository.GetById(userModel1.Id);
            var userRepositoryResponse2 = userRepository.GetById(userModel2.Id);
            var userRepositoryResponse3 = userRepository.GetById(userModel3.Id);

            Assert.NotNull(userRepositoryResponse1);
            Assert.NotNull(userRepositoryResponse2);
            Assert.NotNull(userRepositoryResponse3);

            Assert.Equal(userModel1.Id,userRepositoryResponse1.Id);
            Assert.Equal(userModel1.Name, userRepositoryResponse1.Name);
            Assert.Equal(userModel1.Surname, userRepositoryResponse1.Surname);
            Assert.Equal(userModel1.Email, userRepositoryResponse1.Email);
            Assert.Equal(userModel1.TelephoneNumber, userRepositoryResponse1.TelephoneNumber);
            Assert.Equal(userModel1.Password,userRepositoryResponse1.Password);

            Assert.Equal(userModel2.Id, userRepositoryResponse2.Id);
            Assert.Equal(userModel2.Name, userRepositoryResponse2.Name);
            Assert.Equal(userModel2.Surname, userRepositoryResponse2.Surname);
            Assert.Equal(userModel2.Email, userRepositoryResponse2.Email);
            Assert.Equal(userModel2.TelephoneNumber, userRepositoryResponse2.TelephoneNumber);
            Assert.Equal(userModel2.Password, userRepositoryResponse2.Password);

            Assert.Equal(userModel3.Id, userRepositoryResponse3.Id);
            Assert.Equal(userModel3.Name, userRepositoryResponse3.Name);
            Assert.Equal(userModel3.Surname, userRepositoryResponse3.Surname);
            Assert.Equal(userModel3.Email, userRepositoryResponse3.Email);
            Assert.Equal(userModel3.TelephoneNumber, userRepositoryResponse3.TelephoneNumber);
            Assert.Equal(userModel3.Password, userRepositoryResponse3.Password);
        }

        [Fact]
        public void UpdateUser()
        {
            UserRepository userRepository = new UserRepository(new InMemoryDbContextFactory().CreateDbContext());
            UserModel userModel= new UserModel
            {
                Id = Guid.NewGuid(),
                Name = "TestName",
                Surname = "TestSurname",
                Email = "tmp@tmp.cz",
                Password = "heslo123",
                TelephoneNumber = "+420123456789"
            };
            userRepository.Insert(userModel);
            var userRepositoryResponse = userRepository.GetById(userModel.Id);
            Assert.Equal(userRepositoryResponse.Name,userModel.Name);
            string new_name = "Sir Lukas Podowky z Valchštejna";
            userModel.Name = new_name;
            UserRepository userRepository2 = new UserRepository(new InMemoryDbContextFactory().CreateDbContext());
            userRepository2.Update(userModel);
            var userRepositoryResponse2 = userRepository2.GetById(userModel.Id);
            Assert.Equal(userRepositoryResponse2.Name,new_name);
            Assert.Equal(userRepositoryResponse2.Surname, userModel.Surname);
        }


        [Fact]
        public void GetUserByEmail_Test()
        {
            UserRepository userRepository = new UserRepository(new InMemoryDbContextFactory().CreateDbContext());
            UserModel userModel1 = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                Surname = "Vlach",
                Email = "lalala@vut.pl",
                Password = "heslo123_bude_hash",
                TelephoneNumber = "+420420420420"
            };
            userRepository.Insert(userModel1);

            var userRepositoryResponse1 = userRepository.GetUserByEmail(userModel1.Email);
            var userRepositoryResponse2 = userRepository.GetUserByEmail("neplatnyEmail@seznam.cz");

            Assert.NotNull(userRepositoryResponse1);
            Assert.Null(userRepositoryResponse2);

            Assert.Equal(userModel1.Id, userRepositoryResponse1.Id);
            Assert.Equal(userModel1.Name, userRepositoryResponse1.Name);
            Assert.Equal(userModel1.Surname, userRepositoryResponse1.Surname);
            Assert.Equal(userModel1.Email, userRepositoryResponse1.Email);
            Assert.Equal(userModel1.TelephoneNumber, userRepositoryResponse1.TelephoneNumber);
            Assert.Equal(userModel1.Password, userRepositoryResponse1.Password);
        }

        [Fact]
        public void GetAllUsersOfGroupByGroupId()
        {
            InMemoryDbContextFactory Db = new InMemoryDbContextFactory();
            var dbContext = Db.CreateDbContext();

            GroupMemberRepository groupMemberRepository = new GroupMemberRepository(dbContext);
            UserRepository userRepository = new UserRepository(dbContext);

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


            userRepository.Insert(userModel1);
            userRepository.Insert(userModel2);
            userRepository.Insert(userModel3);

            var userRepositoryResponse1 = userRepository.GetById(userModel1.Id);
            var userRepositoryResponse2 = userRepository.GetById(userModel2.Id);
            var userRepositoryResponse3 = userRepository.GetById(userModel3.Id);

            Assert.NotNull(userRepositoryResponse1);
            Assert.NotNull(userRepositoryResponse2);
            Assert.NotNull(userRepositoryResponse3);


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
                Group = groupModel1,
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

            var userRepositoryResponseUsers = userRepository.GetAllUsersOfGroupByGroupId(groupModel1.Id);
            Assert.NotNull(userRepositoryResponseUsers);
            Assert.Equal(userRepositoryResponseUsers.ElementAt(0).Id, userModel1.Id);
            Assert.Equal(userRepositoryResponseUsers.ElementAt(1).Id, userModel2.Id);
            Assert.Equal(userRepositoryResponseUsers.Count, 2);
        }
    }
}