using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldVision.Repositories.Interfaces;
using WorldVision.Repositories.Items;

namespace WorldVision.Repositories.Repositories
{
    public class ReviewTagCounterRepository : BaseRepository<ReviewTagCounterItem>, IReviewTagCounterRepository
    {
        private const string TakePopularTagsProcedureCode = "TakePopularTags @take";
        public ReviewTagCounterRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<List<ReviewTagCounterItem>> GetPopularTagsAsync(int take)
        {
            SqlParameter parameter = new SqlParameter("@take", take);
            return await ExecuteProcedure(TakePopularTagsProcedureCode, parameter).ToListAsync();
        }
    }

}
