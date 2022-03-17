using System;
using System.Collections.Generic;
using System.Text;
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
        public Task<ReviewLikeItem> GetByUserIdAsync(int userId);
    }
}
