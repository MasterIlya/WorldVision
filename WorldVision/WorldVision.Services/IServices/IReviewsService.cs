using System.Collections.Generic;
using System.Threading.Tasks;
using WorldVision.Services.Models;

namespace WorldVision.Services.IServices
{
    public interface IReviewsService
    {
        public Task CreateAsync(CompositeCreateReviewModel model);
        public Task<PaginationReviewModel> GetUserReviewsAsync(int currentPage, string email);
        public Task<List<ReviewTypesModel>> GetAllReviewTypesAsync();
        public Task UpdateAsync(CompositeCreateReviewModel model);
        public Task<CreateReviewModel> GetReviewAsync(int reviewId);
        public Task RemoveAsync(int reviewId);
    }
}
