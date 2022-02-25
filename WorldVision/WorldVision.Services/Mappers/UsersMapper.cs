using SmartStore.Commons.Enums;
using System;
using System.Collections.Generic;
using WorldVision.Repositories.Items;
using WorldVision.Services.Models;

namespace WorldVision.Services.Mappers
{
    public static class UsersMapper
    {
        public static UserItem Map(RegistrationUserModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new UserItem
            {
                Email = model.Email.Trim().ToLower(),
                Password = model.Password,
                FName = model.FName,
                LName = model.LName,
                RegistrationDate = DateTime.Now,
                Role = RoleTypes.User
            };
        }

        public static UserItem Map(SocialNetworkAuthenticationModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new UserItem
            {
                Email = model.Email.Trim().ToLower(),
                FName = model.FName,
                LName = model.LName,
                RegistrationDate = DateTime.Now,
                Role = RoleTypes.User
            };
        }

        public static UserModel Map(UserItem item)
        {
            if (item == null)
            {
                return null;
            }
            return new UserModel
            {
                UserId = item.UserId,
                Email = item.Email,
                Password = item.Password,
                FName = item.FName,
                LName = item.LName,
                RegistrationDate = item.RegistrationDate,
                Role = item.Role
            };
        }

        public static PaginationUserModel Map(List<UserModel> users, int countOfPages, int currentPage)
        {
            return new PaginationUserModel
            {
                ModelList = users,
                CountOfPages = countOfPages,
                CurrentPage = currentPage
            };
        }
    }
}
