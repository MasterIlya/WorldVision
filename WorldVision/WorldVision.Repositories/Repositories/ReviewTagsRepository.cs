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
    public class ReviewTagsRepository : BaseRepository<ReviewTagItem>, IReviewTagsRepository
    {
        
        public ReviewTagsRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<List<ReviewTagItem>> GetReviewTagsAsync(int reviewId)
        {
            return await GetItems()
                .Where(x => x.ReviewId == reviewId)
                .ToListAsync();
        }
    }
}
