using System.Collections.Generic;
using System.Threading.Tasks;
using WorldVision.Repositories.Items;

namespace WorldVision.Repositories.Interfaces
{
    public interface IReviewImagesRepository
    {
        public Task<ReviewImageItem> GetAsync(int id);
        public Task CreateAsync(ReviewImageItem item);
        public Task RemoveAsync(ReviewImageItem item);
        public Task<List<ReviewImageItem>> GetByReviewIdAsync(int reviewId);
    }
}
