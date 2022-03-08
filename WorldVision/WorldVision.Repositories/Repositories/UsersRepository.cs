using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldVision.Repositories.Interfaces;
using WorldVision.Repositories.Items;

namespace WorldVision.Repositories.Repositories
{
    public class UsersRepository : BaseRepository<UserItem>, IUsersRepository
    {
        public UsersRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<List<UserItem>> GetAsync(int skip, int take)
        {
            return await GetItems()
                .OrderBy(x => x.UserId)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<UserItem> GetByEmailAsync(string email)
        {
            return await GetItems().FirstOrDefaultAsync(x => x.Email == email.Trim().ToLower());
        }

        public async Task<List<UserItem>> GetUsersAsync(List<int> ids)
        {
            List<UserItem> users = new List<UserItem>();
            foreach(var id in ids)
            {
                users.Add(await GetAsync(id));
            }

            return users.ToList();
        }

        public async Task<int> GetCountAsync()
        {
            return await GetItems().CountAsync();
        }
    }
}
