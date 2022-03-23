using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldVision.Repositories.Items;

namespace WorldVision.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        public Task<UserItem> GetAsync(int id);
        public Task CreateAsync(UserItem item);
        public Task RemoveAsync(UserItem item);
        public Task UpdateAsync(UserItem item);
        public Task<List<UserItem>> GetAsync(int skip, int take);
        public Task<UserItem> GetByEmailAsync(string email);
        public Task<int> GetCountAsync();
        public Task<List<UserItem>> GetUsersAsync(List<int> ids);
        public Task<List<UserItem>> GetBySearchStringAsync(string searchString, int skip, int take);
        public Task<int> GetCountForSearchingAsync(string searchString);
    }
}
