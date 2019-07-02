using Microsoft.EntityFrameworkCore;
using Communication.DAL.Entities;

namespace Communication.DAL.Test.Configuration
{
    public class TestCommunicationDbContext : DbContext
    {
        public TestCommunicationDbContext()
        {

        }

        public TestCommunicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<GroupMemberEntity> GroupMembers { get; set; }
        public DbSet<GroupEntity> Groups { get; set; }
        public DbSet<CommentEntity>Comments { get; set; }
        public DbSet<ContributionEntity> Contributions { get; set; }
        public DbSet<ReactionEntity> Reactions { get; set; }
    }
}
