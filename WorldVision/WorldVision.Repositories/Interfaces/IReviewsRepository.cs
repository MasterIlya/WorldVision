using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldVision.Repositories.Items;

namespace WorldVision.Repositories.Interfaces
{
    public interface IReviewsRepository
    {
        public Task<ReviewItem> GetAsync(int id);
        public Task CreateAsync(ReviewItem item);
        public Task RemoveAsync(ReviewItem item);
        public Task UpdateAsync(ReviewItem item);
        public Task<List<ReviewItem>> GetAsync(int skip, int take, int userId);
        public Task<List<ReviewItem>> GetAsync(int skip, int take);
        public Task<int> GetCountAsync(int userId);
    }
}
