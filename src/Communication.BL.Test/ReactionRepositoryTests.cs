using System;
using System.Collections.Generic;
using System.Text;
using Communication.BL.Models;
using Communication.BL.Repositories;
using Communication.DAL.Enums;
using Xunit;

namespace Communication.BL.Tests
{
    public class ReactionRepositoryTests
    {
        [Fact]
        public void InsertReaction()
        {
            ReactionRepository reactionRepository = new ReactionRepository(new InMemoryDbContextFactory().CreateDbContext());

            UserModel userModel = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = "Peter",
                Surname = "Petigriu",
                Email = "bradavickyEskort@volej.cz",
                Password = "qwertzu",
                TelephoneNumber = "4206666666666"
            };

            GroupModel groupModel = new GroupModel
            {
                Id = Guid.NewGuid(),
                Name = "Yzomandias",
                Description = "Skupina pro organizaci vystoupení spolu s umělcem PTK",

            };

            GroupMemberModel groupMemberModel = new GroupMemberModel
            {
                Id = Guid.NewGuid(),
                User = userModel,
                Group = groupModel,
                Permission = PermissionEnum.Moderator
            };

            ContributionModel contributionModel = new ContributionModel
            {
                Id = Guid.NewGuid(),
                User = userModel,
                Group = groupModel,
                Message = "Přijede muzika sraz v 9 večer Severka",
                Title = "Přijede muzika",
                Time = DateTime.MaxValue
            };
          
            ReactionModel reactionModel = new ReactionModel
            {
                Id = Guid.NewGuid(),
                User = userModel,
                Contribution = contributionModel,
                ReactionType = ReactionTypeEnum.Booze
            };
            reactionRepository.Insert(reactionModel);
            var reactionRepositoryResponse = reactionRepository.getReactionByUserId(userModel.Id);
            Assert.NotNull(reactionRepositoryResponse);
        }
    }
}
