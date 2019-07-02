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
    public class ContributionRepositoryTests
    {
        [Fact]
        public void InsertConstribution()
        {
            ContributionRepository contributionRepository = new ContributionRepository(new InMemoryDbContextFactory().CreateDbContext());

            UserModel userModel = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                Surname = "Vlach",
                Email = "xxxxx@vut.pl",
                Password = "heslo123_bude_hash",
                TelephoneNumber = "+420420420420"
            };

            GroupModel groupModel = new GroupModel
            {
                Id = Guid.NewGuid(),
                Name = "První cenová skupina",
                Description = "Popisek testovací skupiny První cenová skupina"
            };

            ContributionModel contributionModel = new ContributionModel
            {
                Id = Guid.NewGuid(),
                Group = groupModel,
                User = userModel,
                Time = DateTime.MaxValue,
                Message = "Doufáme, že z projektu dostaneme dostatečný počet bodů",
                Title = "We bealive"
            };

            contributionRepository.Insert(contributionModel);
            var contributionRepositoryResponse = contributionRepository.GetById(contributionModel.Id);
            Assert.NotNull(contributionRepositoryResponse);
            Assert.Equal(userModel.Name, contributionRepositoryResponse.User.Name);
            Assert.Equal(contributionRepositoryResponse.Group.Name, groupModel.Name);
        }

        [Fact]
        public void DeleteContribution()
        {
            ContributionRepository contributionRepository = new ContributionRepository(new InMemoryDbContextFactory().CreateDbContext());

            UserModel userModel = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                Surname = "Vlach",
                Email = "xxxxx@vut.pl",
                Password = "heslo123_bude_hash",
                TelephoneNumber = "+420420420420"
            };

            GroupModel groupModel = new GroupModel
            {
                Id = Guid.NewGuid(),
                Name = "První cenová skupina",
                Description = "Popisek testovací skupiny První cenová skupina"
            };

            ContributionModel contributionModel = new ContributionModel
            {
                Id = Guid.NewGuid(),
                Group = groupModel,
                User = userModel,
                Time = DateTime.MaxValue,
                Message = "Doufáme, že z projektu dostaneme dostatečný počet bodů",
                Title = "We bealive"
            };

            contributionRepository.Insert(contributionModel);
            var contributionRepositoryResponse = contributionRepository.GetById(contributionModel.Id);
            Assert.NotNull(contributionRepositoryResponse);
            ContributionRepository contributionRepository2 = new ContributionRepository(new InMemoryDbContextFactory().CreateDbContext());
            contributionRepository2.Delete(contributionModel);
            var contributionRepositoryResponse2 = contributionRepository2.GetById(contributionModel.Id);
            Assert.Null(contributionRepositoryResponse2);
        }

        [Fact]
        public void UpdateContribution()
        {
            ContributionRepository contributionRepository = new ContributionRepository(new InMemoryDbContextFactory().CreateDbContext());

            UserModel userModel = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = "Martin",
                Surname = "Vlach",
                Email = "xxxxx@vut.pl",
                Password = "heslo123_bude_hash",
                TelephoneNumber = "+420420420420"
            };

            GroupModel groupModel = new GroupModel
            {
                Id = Guid.NewGuid(),
                Name = "První cenová skupina",
                Description = "Popisek testovací skupiny První cenová skupina"
            };

            ContributionModel contributionModel = new ContributionModel
            {
                Id = Guid.NewGuid(),
                Group = groupModel,
                User = userModel,
                Time = DateTime.MaxValue,
                Message = "Doufáme, že z projektu dostaneme dostatečný počet bodů",
                Title = "We bealive"
            };

            contributionRepository.Insert(contributionModel);
            var contributionRepositoryResponse = contributionRepository.GetById(contributionModel.Id);
            Assert.NotNull(contributionRepositoryResponse);
            string new_title = "Nový popisek příspěvku";
            contributionModel.Title = new_title; 
            ContributionRepository contributionRepository2 = new ContributionRepository(new InMemoryDbContextFactory().CreateDbContext());
            contributionRepository2.Update(contributionModel);
            var contributionRepositoryResponse2 = contributionRepository2.GetById(contributionModel.Id);
            Assert.Equal(new_title, contributionRepositoryResponse2.Title);
            Assert.Equal(contributionModel.Message, contributionRepositoryResponse2.Message);
        }

        [Fact]
        public void GetGetAllContributionsInGroupById()
        {
            InMemoryDbContextFactory Db = new InMemoryDbContextFactory();
            var dbContext = Db.CreateDbContext();

            ContributionRepository contributionRepository = new ContributionRepository(dbContext);
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

            groupRepository.Insert(groupModel1);
            groupRepository.Insert(groupModel2);

            var groupRepositoryResponse1 = groupRepository.GetById(groupModel1.Id);
            var groupRepositoryResponse2 = groupRepository.GetById(groupModel2.Id);

            Assert.NotNull(groupRepositoryResponse1);
            Assert.NotNull(groupRepositoryResponse2);

            dbContext.DetachAllEntities();

            ContributionModel contributionModel1 = new ContributionModel()
            {
                Id = Guid.NewGuid(),
                Group = groupModel1
            };

            ContributionModel contributionModel2 = new ContributionModel()
            {
                Id = Guid.NewGuid(),
                Group = groupModel1
            };

            ContributionModel contributionModel3 = new ContributionModel()
            {
                Id = Guid.NewGuid(),
                Group = groupModel2
            };

            contributionRepository.Insert(contributionModel1);
            contributionRepository.Insert(contributionModel2);
            contributionRepository.Insert(contributionModel3); 

            var contributionRepositoryResponse1 = contributionRepository.GetById(contributionModel1.Id);
            var contributionRepositoryResponse2 = contributionRepository.GetById(contributionModel2.Id);
            var contributionRepositoryResponse3 = contributionRepository.GetById(contributionModel3.Id);

            Assert.NotNull(contributionRepositoryResponse1);
            Assert.NotNull(contributionRepositoryResponse2);
            Assert.NotNull(contributionRepositoryResponse3);

            var group1Id = groupModel1.Id;
            var group2Id = groupModel2.Id;

            List<ContributionModel> contrList1 = new List<ContributionModel>();
            contrList1.Add(contributionModel1);
            contrList1.Add(contributionModel2);

            groupModel1.Contributions = contrList1;

            List<ContributionModel> contrList2 = new List<ContributionModel>();
            contrList2.Add(contributionModel3);

            groupModel2.Contributions = contrList2;

            var contributionRepositoryResponseGroup1 = contributionRepository.GetAllContributionsInGroupById(group1Id);
            Assert.NotNull(contributionRepositoryResponseGroup1);
            Assert.Equal(contributionRepositoryResponseGroup1.ElementAt(0).Id, contributionModel1.Id);
            Assert.Equal(contributionRepositoryResponseGroup1.ElementAt(1).Id, contributionModel2.Id);

            var contributionRepositoryResponseGroup2 = contributionRepository.GetAllContributionsInGroupById(group2Id);
            Assert.NotNull(contributionRepositoryResponseGroup2);
            Assert.Equal(contributionRepositoryResponseGroup2.ElementAt(0).Id, contributionModel3.Id);
        }
    }
}

