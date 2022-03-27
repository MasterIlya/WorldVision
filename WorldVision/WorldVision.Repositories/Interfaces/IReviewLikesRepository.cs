using System.Collections.Generic;
using System.Threading.Tasks;
using WorldVision.Repositories.Items;

namespace WorldVision.Repositories.Interfaces
{
    public interface IReviewLikesRepository
    {
        public Task<ReviewLikeItem> GetAsync(int id);
        public Task CreateAsync(ReviewLikeItem item);
        public Task RemoveAsync(ReviewLikeItem item);
        public Task<int> GetReviewLikeCountAsync(int reviewId);
        public Task<ReviewLikeItem> GetByUserIdAsync(int userId, int reviewId);
        public Task<Dictionary<int, int>> GetReviewsLikeCountAsync(List<int> reviewIds);
    }
}
