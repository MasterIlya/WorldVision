using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldVision.Repositories.Items;

namespace WorldVision.Repositories.Interfaces
{
    public interface IReviewTypesRepository
    {
        public Task<ReviewTypeItem> GetAsync(int id);
        public Task<List<ReviewTypeItem>> GetAllAsync();
    }
}
