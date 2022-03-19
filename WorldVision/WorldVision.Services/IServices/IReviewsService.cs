using System.Collections.Generic;
using System.Threading.Tasks;
using WorldVision.Services.Models;

namespace WorldVision.Services.IServices
{
    public interface IReviewsService
    {
        public Task<int> CreateAsync(CompositeCreateReviewModel model);
        public Task<PaginationReviewModel> GetUserReviewsAsync(int currentPage, string email);
        public Task<List<ReviewTypesModel>> GetAllReviewTypesAsync();
        public Task UpdateAsync(CompositeCreateReviewModel model);
        public Task<CreateReviewModel> GetReviewAsyncForUpdate(int reviewId);
        public Task RemoveAsync(int reviewId);
        public Task CreateReviewImagesAsync(List<ReviewImageModel> models, int reviewId);
        public Task<List<ReviewImageModel>> GetImagesAsync(int reviewId);
        public Task RemoveImageAsync(int imageId);
        public Task<CompositeReviewModel> GetReviewAsync(int reviewId, string type, string currentEmail);
        public Task<CompositeReviewModel> GetReviewAsync(int reviewId, string type);
        public Task<List<PopularTagModel>> GetPopularTagsAsync();

    }
}
