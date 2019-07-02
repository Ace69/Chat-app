using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Communication.BL.Models;
using Communication.BL.Repositories;
using Xunit;

namespace Communication.BL.Tests
{
    public class CommentRepositoryTests
    {
        [Fact]
        public void InsertComment()
        {
            InMemoryDbContextFactory Db = new InMemoryDbContextFactory();
            var dbContext = Db.CreateDbContext();

            ContributionRepository contributionRepository = new ContributionRepository(dbContext);
            CommentRepository commentRepository = new CommentRepository(dbContext);

            ContributionModel contributionModel = new ContributionModel
            {
                Id = Guid.NewGuid(),
                Message = "Doufáme, že z projektu dostaneme dostatečný počet bodů",
                Title = "We belive"
            };

            dbContext.DetachAllEntities();

            CommentModel commentModel = new CommentModel()
            {
                Id = Guid.NewGuid(),
                Message = "Prvni",
                Contribution = contributionModel
            };

            contributionRepository.Insert(contributionModel);
            var contributionRepositoryResponse = contributionRepository.GetById(contributionModel.Id);
            Assert.NotNull(contributionRepositoryResponse);

            commentRepository.Insert(commentModel);
            var commentRepositoryResponse = commentRepository.GetById(commentModel.Id);
            Assert.NotNull(commentRepositoryResponse);
        }

        [Fact]
        public void DeleteComment()
        {
            InMemoryDbContextFactory Db = new InMemoryDbContextFactory();
            var dbContext = Db.CreateDbContext();

            ContributionRepository contributionRepository = new ContributionRepository(dbContext);
            CommentRepository commentRepository = new CommentRepository(dbContext);

            ContributionModel contributionModel = new ContributionModel
            {
                Id = Guid.NewGuid(),
                Message = "Doufáme, že z projektu dostaneme dostatečný počet bodů",
                Title = "We belive"
            };

            dbContext.DetachAllEntities();

            CommentModel commentModel = new CommentModel()
            {
                Id = Guid.NewGuid(),
                Message = "Prvni",
                Contribution = contributionModel
            };

            contributionRepository.Insert(contributionModel);
            var contributionRepositoryResponse = contributionRepository.GetById(contributionModel.Id);
            Assert.NotNull(contributionRepositoryResponse);

            commentRepository.Insert(commentModel);
            var commentRepositoryResponse = commentRepository.GetById(commentModel.Id);
            Assert.NotNull(commentRepositoryResponse);

            dbContext.DetachAllEntities();

            //Pred smazanim je nutne odebrat prispevek z property
            commentModel.Contribution = null;

            commentRepository.Delete(commentModel);
            var commentRepositoryResponse1 = commentRepository.GetById(commentModel.Id);
            Assert.Null(commentRepositoryResponse1);
        }

        [Fact]
        public void GetGetAllCommentsOfContributionById()
        {
            InMemoryDbContextFactory Db = new InMemoryDbContextFactory();
            var dbContext = Db.CreateDbContext();

            ContributionRepository contributionRepository = new ContributionRepository(dbContext);
            CommentRepository commentRepository = new CommentRepository(dbContext);

            ContributionModel contributionModel1 = new ContributionModel()
            {
                Id = Guid.NewGuid(),
            };

            ContributionModel contributionModel2 = new ContributionModel()
            {
                Id = Guid.NewGuid(),
                
            };

            ContributionModel contributionModel3 = new ContributionModel()
            {
                Id = Guid.NewGuid(),
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

            dbContext.DetachAllEntities();

            CommentModel commentModel1 = new CommentModel
            {
                Id = Guid.NewGuid(),
                Message = "Ahoj",
                Contribution = contributionModel1
            };

            CommentModel commentModel2 = new CommentModel
            {
                Id = Guid.NewGuid(),
                Message = "světe",
                Contribution = contributionModel1
            };

            CommentModel commentModel3 = new CommentModel
            {
                Id = Guid.NewGuid(),
                Message = "Lorem",
                Contribution = contributionModel2
                
            };

            commentRepository.Insert(commentModel1);
            commentRepository.Insert(commentModel2);
            commentRepository.Insert(commentModel3);

            var commentRepositoryResponse1 = commentRepository.GetById(commentModel1.Id);
            var commentRepositoryResponse2 = commentRepository.GetById(commentModel2.Id);
            var commentRepositoryResponse3 = commentRepository.GetById(commentModel3.Id);

            Assert.NotNull(commentRepositoryResponse1);
            Assert.NotNull(commentRepositoryResponse2);
            Assert.NotNull(commentRepositoryResponse3);

            var contr1Id = contributionModel1.Id;
            var contr2Id = contributionModel2.Id;

            List<CommentModel> commentList1 = new List<CommentModel>();
            commentList1.Add(commentModel1);
            commentList1.Add(commentModel2);

            contributionModel1.Comments = commentList1;

            List<CommentModel> commentList2 = new List<CommentModel>();
            commentList2.Add(commentModel3);

            contributionModel2.Comments = commentList2;

            var commentRepositoryResponseCon1 = commentRepository.GetAllCommentsOfContributionById(contr1Id);
            Assert.NotNull(commentRepositoryResponseCon1);
            Assert.Equal(commentRepositoryResponseCon1.ElementAt(0).Id, commentModel1.Id);
            Assert.Equal(commentRepositoryResponseCon1.ElementAt(1).Id, commentModel2.Id);

            var commentRepositoryResponseCon2 = commentRepository.GetAllCommentsOfContributionById(contr2Id);
            Assert.NotNull(commentRepositoryResponseCon2);
            Assert.Equal(commentRepositoryResponseCon2.ElementAt(0).Id, commentModel3.Id);
        }
    }
}