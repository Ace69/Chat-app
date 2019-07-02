using Microsoft.EntityFrameworkCore;
using Communication.DAL.Entities;
using System.IO;
using System.Linq;

namespace Communication.DAL
{
    public class CommunicationDbContext : DbContext
    {
        public CommunicationDbContext()
        {

        }
        public CommunicationDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog = CommunicationsDB;
                MultipleActiveResultSets = True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public void DetachAllEntities()
        {
            var changedEntriesCopy = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }
        public void RemoveEntities<T>() where T : class
        {
            this.Set<T>().Local.ToList().ForEach(p => this.Entry(p).State = EntityState.Detached);
        }

        public DbSet<UserEntity> Users{ get; set; }
        public DbSet<GroupMemberEntity> GroupMembers { get; set; }
        public DbSet<GroupEntity> Groups { get; set; }
        public DbSet<CommentEntity>Comments { get; set; }
        public DbSet<ContributionEntity> Contributions { get; set; }
        public DbSet<ReactionEntity> Reactions { get; set; }
    }
}
