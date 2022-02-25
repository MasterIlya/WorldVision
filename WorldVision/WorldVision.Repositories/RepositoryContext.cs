using Microsoft.EntityFrameworkCore;
using WorldVision.Repositories.Items;

namespace WorldVision.Repositories
{
    public class RepositoryContext : DbContext, IRepositoryContext
    {
        private readonly string _conectionString;

        public DbSet<UserItem> UserItems { get; set; }

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
