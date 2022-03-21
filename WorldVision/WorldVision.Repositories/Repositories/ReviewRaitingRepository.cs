using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldVision.Repositories.Interfaces;
using WorldVision.Repositories.Items;

namespace WorldVision.Repositories.Repositories
{
    public class ReviewRaitingRepository : BaseRepository<ReviewRaitingItem>, IReviewRaitingRepository
    {
        private const string TakePopularTagsProcedureCode = "TakePopularReviews @take";
        public ReviewRaitingRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<List<ReviewRaitingItem>> GetPopularReviewsAsync(int take)
        {
            SqlParameter parameter = new SqlParameter("@take", take);
            return await ExecuteProcedure(TakePopularTagsProcedureCode, parameter).ToListAsync();
        }
    }
}
