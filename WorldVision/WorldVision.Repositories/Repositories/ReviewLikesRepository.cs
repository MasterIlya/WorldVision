using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldVision.Repositories.Interfaces;
using WorldVision.Repositories.Items;

namespace WorldVision.Repositories.Repositories
{
    public class ReviewLikesRepository : BaseRepository<ReviewLikeItem>, IReviewLikesRepository
    {
        public ReviewLikesRepository(IRepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public async Task<int> GetReviewLikeCountAsync(int reviewId)
        {
            return await GetItems()
                .Where(x => x.ReviewId == reviewId)
                .CountAsync();
        }

        public async Task<ReviewLikeItem> GetByUserIdAsync(int userId)
        {
            return await GetItems().FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<Dictionary<int, int>> GetReviewsLikeCountAsync(List<int> reviewIds)
        {
            var likes = new Dictionary<int, int>();
            foreach(var id in reviewIds)
            {
                var reviewLikes = await GetItems().Where(x => x.ReviewId == id).CountAsync();
                likes.Add(id, reviewLikes);
            }

            return likes;
        }
    }
}
