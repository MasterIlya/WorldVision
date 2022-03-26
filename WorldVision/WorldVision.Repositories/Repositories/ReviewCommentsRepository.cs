using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldVision.Repositories.Interfaces;
using WorldVision.Repositories.Items;

namespace WorldVision.Repositories.Repositories
{
    public class ReviewCommentsRepository : BaseRepository<ReviewCommentItem>, IReviewCommentsRepository
    {
        public ReviewCommentsRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<List<ReviewCommentItem>> GetByReviewIdAsync(int reviewId, int skip, int take)
        {
            return await GetItems()
                .Where(x => x.ReviewId == reviewId)
                .OrderByDescending(x => x.CreateDate)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> GetCountOfComments(int reviewId)
        {
            return await GetItems().Where(x => x.ReviewId == reviewId).CountAsync();
        }
    }
}
