using System.Collections.Generic;
using System.Threading.Tasks;
using WorldVision.Services.Models;

namespace WorldVision.Services.IServices
{
    public interface IUsersService
    {
        public Task CreateAsync(RegistrationUserModel model);
        public Task CreateAsync(SocialNetworkAuthenticationModel model);
        public Task<UserModel> GetUserAsync(int id);
        public Task<PaginationUserModel> GetUsersAsync(int currentPage);
        public Task<UserModel> GetUserByEmailAsync(string email);
        public Task BlockUsersAsync(int[] ids);
        public Task UnblockUsersAsync(int[] ids);
        public Task DeleteUsersAsync(int[] ids);
        public Task GetAdminRightsAsync(int[] ids);
        public Task<PaginationUserModel> SearchByEmailAsync(string search, int currentPage);
    }
}
