using Microsoft.EntityFrameworkCore;
using WorldVision.Repositories.Items;

namespace WorldVision.Repositories
{
    public class RepositoryContext : DbContext, IRepositoryContext
    {
        private readonly string _conectionString;

        public DbSet<UserItem> UserItems { get; set; }
        public DbSet<ReviewItem> ReviewItems { get; set; }
        public DbSet<ReviewTypeItem> ReviewTypeItems { get; set; }
        public DbSet<ReviewImageItem> ReviewImageItems { get; set; }
        public DbSet<ReviewLikeItem> ReviewLikeItems { get; set; }
        public DbSet<ReviewTagItem> ReviewTagItems { get; set; }
        public DbSet<ReviewTagCounterItem> ReviewTagCounterItems { get; set; }
        public DbSet<ReviewRaitingItem> ReviewRaitingItems { get; set; }
        public DbSet<ReviewCommentItem> ReviewCommentItems { get; set; }


        public RepositoryContext(string conectionString)
        {
            _conectionString = conectionString;

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_conectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserItem).Assembly);
        }
    }
}
