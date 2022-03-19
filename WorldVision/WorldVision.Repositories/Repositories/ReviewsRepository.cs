using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldVision.Repositories.Interfaces;
using WorldVision.Repositories.Items;

namespace WorldVision.Repositories.Repositories
{
    public class ReviewsRepository : BaseRepository<ReviewItem>, IReviewsRepository
    {
        public ReviewsRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<List<ReviewItem>> GetAsync(int skip, int take, int userId)
        {
            return await GetItems()
                .Where(x => x.UserId == userId && !x.Delisted)
                .OrderByDescending(x => x.CreateDate)
                .Take(take)
                .ToListAsync();
        }

        public async Task<List<ReviewItem>> GetAsync(int skip, int take)
        {
            return await GetItems()
                .Where(x => !x.Delisted)
                .OrderByDescending(x => x.CreateDate)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<List<ReviewItem>> GetReviewsInCategory(int categoryId, int take)
        {
            return await GetItems()
                .Where(x => x.ReviewTypeId == categoryId)
                .OrderByDescending(x => x.UpdateDate)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync(int userId)
        {
            return await GetItems()
                .Where(x => x.UserId == userId && !x.Delisted)
                .CountAsync();
        }

    }
}
