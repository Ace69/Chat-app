using System;
using Microsoft.EntityFrameworkCore;
using Communication.DAL.Entities;
using Xunit;
using System.Linq;
using Communication.DAL.Enums;
using Communication.DAL.Test.Configuration;

namespace Communication.DAL.Test.Tests
{
    public class CommunicationDbContextTests : IClassFixture<CommunicationDbContextTestsClassSetupFixture>
    {
        public CommunicationDbContextTests(CommunicationDbContextTestsClassSetupFixture testContext)
        {
            _testContext = testContext;
        }

        private readonly CommunicationDbContextTestsClassSetupFixture _testContext;

        [Fact]
        public void AddUserTest()
        {
            var userEntity = new UserEntity
            {
                Name = "Pavel",
                Surname = "Dvorak",
                Email = "kocicak@vut.pl",
                Password = "heslo123_bude_hash",
                TelephoneNumber = "+420420420420"

            };

            _testContext.CommunicationDbContextSut.Users.Add(userEntity);
            _testContext.CommunicationDbContextSut.SaveChanges();

            using (var dbx = _testContext.CreateCommunicationDbContext())
            {
                var retrievedUser = dbx.Users.First(entity => entity.Id == userEntity.Id);
                Assert.Equal(userEntity, retrievedUser, UserEntity.UserTest);
            }
        }

        [Fact]
        public void AddUserTest2()
        {
            var userEntity = new UserEntity
            {
                Name = "Martin",
                Surname = "Vlach",
                Email = "opicaci@vut.de",
                Password = "passwd123",
                TelephoneNumber = "+420123456789"

            };

            _testContext.CommunicationDbContextSut.Users.Add(userEntity);
            _testContext.CommunicationDbContextSut.SaveChanges();

            using (var dbx1 = _testContext.CreateCommunicationDbContext())
            {
                var retrievedUser = dbx1.Users.First(entity => entity.Id == userEntity.Id);
                Assert.Equal(userEntity, retrievedUser, UserEntity.UserTest);
            }
        }

        [Fact]
        public void AddGroupTest()
        {
            var groupEntity = new GroupEntity()
            {
                Name = "Nase skupina ICS",
                Description = "Skupina pro projekt do predmetu ICS",
            };
            _testContext.CommunicationDbContextSut.Groups.Add(groupEntity);
            _testContext.CommunicationDbContextSut.SaveChanges();

            using (var dbx2 = _testContext.CreateCommunicationDbContext())
            {
                var retrievedGroup = dbx2.Groups.First(entity=> entity.Id == groupEntity.Id);
                Assert.Equal(groupEntity, retrievedGroup, GroupEntity.GroupTest);
            }
        }

        [Fact]
        public void AddGroupTest2()
        {
            var groupEntity2 = new GroupEntity()
            {
                Name = "Purkynovy koleje",
                Description = "Skupina purkynovy koleje",
            };
            _testContext.CommunicationDbContextSut.Groups.Add(groupEntity2);
            _testContext.CommunicationDbContextSut.SaveChanges();

            using (var dbx2 = _testContext.CreateCommunicationDbContext())
            {
                var retrievedGroup = dbx2.Groups.First(entity => entity.Id == groupEntity2.Id);
                Assert.Equal(groupEntity2, retrievedGroup, GroupEntity.GroupTest);
            }
        }
    }
}
