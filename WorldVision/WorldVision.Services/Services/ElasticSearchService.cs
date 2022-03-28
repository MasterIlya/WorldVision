using Nest;
using System;
using System.Linq;
using System.Threading.Tasks;
using WorldVision.Services.IServices;
using WorldVision.Services.Mappers;
using WorldVision.Services.Models;

namespace WorldVision.Services.Services
{
    public class ElasticSearchService : IElasticSearchService
    {
        private readonly ElasticClient _client;
        private const int DefaultReviewsCount = 10;
        private const int DefaultCurrentPage = 1;

        public ElasticSearchService(ElasticClient client)
        {
            _client = client;
        }

        public async Task DeleteReviewAsync(int reviewId)
        {
            await _client.DeleteAsync<ReviewModel>(reviewId);
        }


        public async Task GetReviewIndexAsync(ReviewModel model)
        {
            await _client.IndexAsync(model, x => x.Id(model.ReviewId));
        }

        public async Task<PaginationReviewModel> SearchReviewsAsync(string search, int currentPage)
        {
            if (currentPage == 0)
            {
                currentPage = DefaultCurrentPage;
            }
            var take = DefaultReviewsCount;
            var skip = (currentPage - 1) * DefaultReviewsCount;
            var searchResponse = await _client.SearchAsync<ReviewModel>(s => s
                .Sort(x => x.Descending(a => a.CreateDate))
                .Skip(skip)
                .Take(take)
                .Query(q => q
                      .MultiMatch(m => m.Fields(x => x.Fields(a => a.Title, b => b.Content, c => c.ReviewType, i => i.Tags, f => f.UserName))
                                 .Query(search))));

            var elementsCount = await GetCountAsync(search);
            var pageCount = Convert.ToInt32(Math.Ceiling((double)elementsCount / take));
            var model = ReviewsMapper.Map(searchResponse.Documents.ToList(), pageCount, currentPage);

            return model;
        }

        public async Task<PaginationReviewModel> SearchReviewsByTagAsync(string search, int currentPage)
        {
            if (currentPage == 0)
            {
                currentPage = DefaultCurrentPage;
            }
            var take = DefaultReviewsCount;
            var skip = (currentPage - 1) * DefaultReviewsCount;
            var searchResponse = await _client.SearchAsync<ReviewModel>(s => s
                .Sort(x => x.Descending(a => a.CreateDate))
                .Skip(skip)
                .Take(take)
                .Query(q => q
                      .MultiMatch(m => m.Fields(x => x.Fields(i => i.Tags))
                                 .Query(search))));

            var elementsCount = await GetCountByTagAsync(search);
            var pageCount = Convert.ToInt32(Math.Ceiling((double)elementsCount / take));
            var model = ReviewsMapper.Map(searchResponse.Documents.ToList(), pageCount, currentPage);

            return model;
        }


        public async Task<int> GetCountByTagAsync(string search)
        {

            var searchResponse = await _client.SearchAsync<ReviewModel>(s => s.Size(2000).Query(q => q
                      .MultiMatch(m => m.Fields(x => x.Fields(i => i.Tags))
                                 .Query(search))));

            return searchResponse.Documents.Count;
        }

        public async Task<int> GetCountAsync(string search)
        {

            var searchResponse = await _client.SearchAsync<ReviewModel>(s => s.Size(2000).Query(q => q
                      .MultiMatch(m => m.Fields(x => x.Fields(a => a.Title, b => b.Content, c => c.ReviewType, i => i.Tags, f => f.UserName))
                                 .Query(search))));

            return searchResponse.Documents.Count;
        }
    }
}
