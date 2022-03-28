using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldVision.Services.Models;

namespace WorldVision.Services.IServices
{
    public interface IElasticSearchService
    {
        public Task DeleteReviewAsync(int reviewId);
        public Task GetReviewIndexAsync(ReviewModel model);
        public Task<PaginationReviewModel> SearchReviewsAsync(string search, int currentPage);
        public Task<PaginationReviewModel> SearchReviewsByTagAsync(string search, int currentPage);
    }
}
