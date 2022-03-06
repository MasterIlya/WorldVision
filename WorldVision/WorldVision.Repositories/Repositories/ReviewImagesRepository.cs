using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldVision.Repositories.Interfaces;
using WorldVision.Repositories.Items;

namespace WorldVision.Repositories.Repositories
{
    public class ReviewImagesRepository : BaseRepository<ReviewImageItem>, IReviewImagesRepository
    {
        public ReviewImagesRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<List<ReviewImageItem>> GetByReviewIdAsync(int reviewId)
        {
            return await GetItems()
                .Where(x => x.ReviewId == reviewId)
                .OrderBy(x => x.ImageId)
                .ToListAsync();
        }
    }
}
