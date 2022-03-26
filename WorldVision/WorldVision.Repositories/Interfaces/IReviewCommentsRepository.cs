using System.Collections.Generic;
using System.Threading.Tasks;
using WorldVision.Repositories.Items;

namespace WorldVision.Repositories.Interfaces
{
    public interface IReviewCommentsRepository
    {
        public Task<ReviewCommentItem> GetAsync(int id);
        public Task CreateAsync(ReviewCommentItem item);
        public Task RemoveAsync(ReviewCommentItem item);
        public Task<List<ReviewCommentItem>> GetByReviewIdAsync(int reviewId, int skip = 0, int take = 5);
        public Task<int> GetCountOfComments(int reviewId);

    }
}
