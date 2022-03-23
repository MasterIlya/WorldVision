using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldVision.Commons.Enums;
using WorldVision.Repositories.Interfaces;
using WorldVision.Services.IServices;
using WorldVision.Services.Mappers;
using WorldVision.Services.Models;

namespace WorldVision.Services.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private const int DefaultUsersCount = 10;
        private const int DefaultCurrentPage = 1;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task CreateAsync(RegistrationUserModel model)
        {
            var item = UsersMapper.Map(model);

            await _usersRepository.CreateAsync(item);
        }

        public async Task CreateAsync(SocialNetworkAuthenticationModel model)
        {
            var item = UsersMapper.Map(model);

            await _usersRepository.CreateAsync(item);
        }

        public async Task<UserModel> GetUserAsync(int id)
        {
            var item = await _usersRepository.GetAsync(id);
            var model = UsersMapper.Map(item);

            return model;
        }

        public async Task<PaginationUserModel> GetUsersAsync(int currentPage)
        {
            if (currentPage == 0)
            {
                currentPage = DefaultCurrentPage;
            }

            var skip = (currentPage - 1) * DefaultUsersCount;
            var take = DefaultUsersCount;

            var items = await _usersRepository.GetAsync(skip, take);

            var modelsList = items.Select(x => UsersMapper.Map(x)).ToList();

            var elementsCount = await _usersRepository.GetCountAsync();

            var countOfPages = Convert.ToInt32(Math.Ceiling((double)elementsCount / take));

            var result = UsersMapper.Map(modelsList, countOfPages, currentPage);

            return result;
        }

        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            var user = await _usersRepository.GetByEmailAsync(email);

            return UsersMapper.Map(user);
        }

        public async Task BlockUsersAsync(int[] ids)
        {
            foreach (var id in ids)
            {
                var item = await _usersRepository.GetAsync(id);
                item.State = StateTypes.Blocked;
                await _usersRepository.UpdateAsync(item);
            }
        }

        public async Task UnblockUsersAsync(int[] ids)
        {
            foreach (var id in ids)
            {
                var item = await _usersRepository.GetAsync(id);
                item.State = StateTypes.Unblocked;
                await _usersRepository.UpdateAsync(item);
            }
;
        }

        public async Task DeleteUsersAsync(int[] ids)
        {
            foreach (var id in ids)
            {
                var item = await _usersRepository.GetAsync(id);
                item.Delisted = true;
                await _usersRepository.UpdateAsync(item);
            }
        }

        public async Task GetAdminRightsAsync(int[] ids)
        {
            foreach (var id in ids)
            {
                var item = await _usersRepository.GetAsync(id);
                item.Role = RoleTypes.Admin;
                await _usersRepository.UpdateAsync(item);
            }
        }

        public async Task<PaginationUserModel> SearchByEmailAsync(string search, int currentPage)
        {
            if (currentPage == 0)
            {
                currentPage = DefaultCurrentPage;
            }

            var skip = (currentPage - 1) * DefaultUsersCount;
            var take = DefaultUsersCount;

            var items = await _usersRepository.GetBySearchStringAsync(search, skip, take);

            var modelsList = items.Select(x => UsersMapper.Map(x)).ToList();

            var elementsCount = await _usersRepository.GetCountForSearchingAsync(search);

            var countOfPages = Convert.ToInt32(Math.Ceiling((double)elementsCount / take));

            var result = UsersMapper.Map(modelsList, countOfPages, currentPage);

            result.Search = search;

            return result;
        }
    }
}
