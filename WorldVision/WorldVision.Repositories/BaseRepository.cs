using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace WorldVision.Repositories
{
    public class BaseRepository<T>
         where T : class
    {
        private readonly IRepositoryContext _repositoryContext;

        public BaseRepository(IRepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public async Task<T> GetAsync(int id)
        {
            return await DbSet().FindAsync(id);
        }

        public async Task CreateAsync(T item)
        {
            await DbSet().AddAsync(item);
            await _repositoryContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(T item)
        {
            DbSet().Remove(item);
            await _repositoryContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T item)
        {
            DbSet().Update(item);
            await _repositoryContext.SaveChangesAsync();
        }
        public IQueryable<T> ExecuteProcedure(string sqlCode, object parameters = null)
        {
            return DbSet().FromSqlRaw(sqlCode, parameters);
        }

        protected IQueryable<T> GetItems()
        {
            return DbSet().AsQueryable();
        }

        private DbSet<T> DbSet()
        {
            return _repositoryContext.Set<T>();
        }
    }
}
