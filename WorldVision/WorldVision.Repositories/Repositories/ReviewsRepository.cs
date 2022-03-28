using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldVision.Commons.Enums;
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

        public async Task<List<ReviewItem>> GetAsync(int skip, int take, int userId, SortTypes sort, FilterTypes filter)
        {
            var result = GetItems().Where(x => x.UserId == userId);

            if (filter == FilterTypes.Date && sort == SortTypes.Descending)
            {
                result = result.OrderByDescending(x => x.CreateDate);
            }
            else if (filter == FilterTypes.Date && sort == SortTypes.Ascending)
            {
                result = result.OrderBy(x => x.CreateDate);
            }
            else if (filter == FilterTypes.Assessment && sort == SortTypes.Descending)
            {
                result = result.OrderByDescending(x => x.AuthorScore);
            }
            else if (filter == FilterTypes.Assessment && sort == SortTypes.Descending)
            {
                result = result.OrderBy(x => x.AuthorScore);
            }
            return await result
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<List<ReviewItem>> GetAsync(int skip, int take)
        {
            return await GetItems()
                .OrderByDescending(x => x.CreateDate)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<List<ReviewItem>> GetReviewsInCategory(int categoryId, int skip, int take)
        {
            return await GetItems()
                .Where(x => x.ReviewTypeId == categoryId)
                .OrderByDescending(x => x.UpdateDate)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<List<ReviewItem>> GetReviewsInCategory(int categoryId, int skip, int take, SortTypes sort, FilterTypes filter)
        {
            var result = GetItems().Where(x => x.ReviewTypeId == categoryId);

            if (filter == FilterTypes.Date && sort == SortTypes.Descending)
            {
                result = result.OrderByDescending(x => x.CreateDate);
            }
            else if (filter == FilterTypes.Date && sort == SortTypes.Ascending)
            {
                result = result.OrderBy(x => x.CreateDate);
            }
            else if (filter == FilterTypes.Assessment && sort == SortTypes.Descending)
            {
                result = result.OrderByDescending(x => x.AuthorScore);
            }
            else if (filter == FilterTypes.Assessment && sort == SortTypes.Ascending)
            {
                result = result.OrderBy(x => x.AuthorScore);
            }
            return await result
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync(int userId)
        {
            return await GetItems()
                .Where(x => x.UserId == userId)
                .CountAsync();
        }

        public async Task<int> GetCountInCategoryAsync(int categoryId)
        {
            return await GetItems()
                .Where(x => x.ReviewTypeId == categoryId)
                .CountAsync();
        }
    }
}
