using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldVision.Services.Models;

namespace WorldVision.Services.IServices
{
    public interface IUsersService
    {
        public Task CreateAsync(RegistrationUserModel model);
        public Task CreateAsync(SocialNetworkAuthenticationModel model);
        public Task<PaginationUserModel> GetUsersAsync(int currentPage);
        public Task<UserModel> GetUserByEmailAsync(string email);

    }
}
