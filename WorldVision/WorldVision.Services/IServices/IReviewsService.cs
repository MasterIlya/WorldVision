using System.Collections.Generic;
using System.Threading.Tasks;
using WorldVision.Commons.Enums;
using WorldVision.Services.Models;

namespace WorldVision.Services.IServices
{
    public interface IReviewsService
    {
        public Task<int> CreateAsync(CompositeCreateReviewModel model);
        public Task<PaginationReviewModel> GetUserReviewsAsync(int currentPage, string email, FilterTypes filter, SortTypes sort);
        public Task<List<ReviewTypeModel>> GetAllReviewTypesAsync();
        public Task UpdateAsync(CompositeCreateReviewModel model);
        public Task<CreateReviewModel> GetReviewAsyncForUpdate(int reviewId);
        public Task RemoveAsync(int reviewId);
        public Task CreateReviewImagesAsync(List<ReviewImageModel> models, int reviewId);
        public Task<List<ReviewImageModel>> GetImagesAsync(int reviewId);
        public Task RemoveImageAsync(int imageId);
        public Task<CompositeReviewModel> GetReviewAsync(int reviewId, string type, string currentEmail);
        public Task<CompositeReviewModel> GetReviewAsync(int reviewId, string type);
        public Task<ReviewModel> GetReviewAsync(int reviewId);
        public Task AddLikeAsync(int reviewId, string email);
        public Task RemoveLikeAsync(int reviewId, string email);
        public Task<int> GetReviewRatingAsync(int reviewId);
        public Task<ReviewLikeModel> GetLikeCurrentUserAsync(int userId, int reviewId);
        public Task<List<PopularTagModel>> GetPopularTagsAsync();
        public Task<GeneralPageModel> GetGeneralPageModelAsync();
        public Task<PaginationReviewModel> GetReviewsInCategoryAsync(int categoryId, int currentPage, FilterTypes filter, SortTypes sort);
        public Task<int> CreateReviewCommentAsync(CreateCommentModel model);
        public Task<ReviewCommentModel> GetReviewCommentAsync(int commentId);
        public Task<List<ReviewCommentModel>> GetCommentsByReviewIdAsync(int reviewId, int skip);
        public Task<List<TagModel>> GetAllTags();
    }
}
