using System.Collections.Generic;
using System.Threading.Tasks;
using WorldVision.Repositories.Items;

namespace WorldVision.Repositories.Interfaces
{
    public interface IReviewTagCounterRepository
    {
        public Task<List<ReviewTagCounterItem>> GetPopularTagsAsync(int take);
    }
}
