using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldVision.Repositories.Interfaces;
using WorldVision.Repositories.Items;

namespace WorldVision.Repositories.Repositories
{
    public class ReviewTypesRepository : BaseRepository<ReviewTypeItem>, IReviewTypesRepository
    {
        public ReviewTypesRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<List<ReviewTypeItem>> GetAllAsync()
        {
            return await GetItems().OrderBy(x => x.ReviewTypeId).ToListAsync();
        }
    }
}
