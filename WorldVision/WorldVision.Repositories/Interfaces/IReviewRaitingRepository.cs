using System.Collections.Generic;
using System.Threading.Tasks;
using WorldVision.Repositories.Items;

namespace WorldVision.Repositories.Interfaces
{
    public interface IReviewRaitingRepository
    {
        public Task<List<ReviewRaitingItem>> GetPopularReviewsAsync(int take);
    }
}
