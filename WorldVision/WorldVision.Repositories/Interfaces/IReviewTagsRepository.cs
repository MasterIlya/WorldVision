using System.Collections.Generic;
using System.Threading.Tasks;
using WorldVision.Repositories.Items;

namespace WorldVision.Repositories.Interfaces
{
    public interface IReviewTagsRepository
    {
        public Task<ReviewTagItem> GetAsync(int id);
        public Task CreateAsync(ReviewTagItem item);
        public Task RemoveAsync(ReviewTagItem item);
        public Task<List<ReviewTagItem>> GetReviewTagsAsync(int reviewId);
    }
}
