﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldVision.Repositories.Interfaces;
using WorldVision.Services.IServices;
using WorldVision.Services.Mappers;
using WorldVision.Services.Models;

namespace WorldVision.Services.Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly IReviewsRepository _reviewsRepository;
        private readonly IReviewTypesRepository _reviewTypesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IReviewImagesRepository _reviewImagesRepository;
        private const int DefaultReviewsCount = 10;
        private const int DefaultCurrentPage = 1;

        public ReviewsService(IReviewsRepository reviewsRepository, IReviewTypesRepository reviewTypesRepository,
            IUsersRepository usersRepository, IReviewImagesRepository reviewImagesRepository)
        {
            _reviewsRepository = reviewsRepository;
            _reviewTypesRepository = reviewTypesRepository;
            _usersRepository = usersRepository;
            _reviewImagesRepository = reviewImagesRepository;
        }

        public async Task<int> CreateAsync(CompositeCreateReviewModel model)
        {
            var item = ReviewsMapper.Map(model.ReviewModel);

            await _reviewsRepository.CreateAsync(item);
            return item.ReviewId;
        }

        public async Task UpdateAsync(CompositeCreateReviewModel model)
        {
            var item = await _reviewsRepository.GetAsync(model.ReviewModel.ReviewId);

            var newItem = ReviewsMapper.UpdateMap(item, model.ReviewModel);

            await _reviewsRepository.UpdateAsync(newItem);
        }

        public async Task RemoveAsync(int reviewId)
        {
            var item = await _reviewsRepository.GetAsync(reviewId);

            item.Delisted = true;

            await _reviewsRepository.UpdateAsync(item);
        }

        public async Task<CreateReviewModel> GetReviewAsyncForUpdate(int reviewId)
        {
            var item = await _reviewsRepository.GetAsync(reviewId);

            var review = ReviewsMapper.Map(item);

            return review;
        }

        public async Task<PaginationReviewModel> GetUserReviewsAsync(int currentPage, string email)
        {
            if (currentPage == 0)
            {
                currentPage = DefaultCurrentPage;
            }

            var skip = (currentPage - 1) * DefaultReviewsCount;
            var take = DefaultReviewsCount;

            var user = await _usersRepository.GetByEmailAsync(email);
            var fullName = $"{user.FName} {user.LName}";

            var items = await _reviewsRepository.GetAsync(skip, take, user.UserId);
            var typeItems = await _reviewTypesRepository.GetAllAsync();

            var modelsList = items.Select(x => ReviewsMapper.Map(x, typeItems, fullName)).ToList();

            var elementsCount = await _reviewsRepository.GetCountAsync(user.UserId);

            var countOfPages = Convert.ToInt32(Math.Ceiling((double)elementsCount / take));

            var result = ReviewsMapper.Map(modelsList, countOfPages, currentPage);

            return result;
        }

        public async Task<List<ReviewTypesModel>> GetAllReviewTypesAsync()
        {
            var typeItems = await _reviewTypesRepository.GetAllAsync();

            var typeModels = typeItems.Select(x => ReviewsMapper.Map(x)).ToList();

            return typeModels;
        }

        public async Task CreateReviewImagesAsync(List<ReviewImageModel> models, int reviewId)
        {
            foreach (var model in models)
            {
                var item = ReviewsMapper.Map(model, reviewId);
                await _reviewImagesRepository.CreateAsync(item);
            }
        }

        public async Task<List<ReviewImageModel>> GetImagesAsync(int reviewId)
        {
            var items = await _reviewImagesRepository.GetByReviewIdAsync(reviewId);
            var models = items.Select(x => ReviewsMapper.Map(x)).ToList();

            return models;
        }

        public async Task RemoveImageAsync(int imageId)
        {
            var item = await _reviewImagesRepository.GetAsync(imageId);
            await _reviewImagesRepository.RemoveAsync(item);
        }

        public async Task<List<ReviewModel>> GetReviewsInCategory(int categoryId)
        {
            var take = DefaultReviewsCount;

            var items = await _reviewsRepository.GetReviewsInCategory(categoryId, take);

            var typeItems = await _reviewTypesRepository.GetAllAsync();

            var ids = items.Select(x => x.UserId).Distinct().ToList();

            var users = await _usersRepository.GetUsersAsync(ids);

            var usersToDict = users.ToDictionary(x => x.UserId);

            var models = items.Select(x => ReviewsMapper.Map(x, typeItems, usersToDict)).ToList();

            return models;
        }

        public async Task<CompositeReviewModel> GetReviewAsync(int reviewId, string type)
        {
            var types = await _reviewTypesRepository.GetAllAsync();
            var typeId = types.FirstOrDefault(x => x.ReviewType == type).ReviewTypeId;
            var review = await _reviewsRepository.GetAsync(reviewId);
            var userItem = await _usersRepository.GetAsync(review.UserId);
            var fullName = $"{userItem.FName} {userItem.LName}";

            return new CompositeReviewModel
            {
                Review = ReviewsMapper.Map(review, types, fullName),
                Images = await GetImagesAsync(reviewId),
                LastReviewsInCategory = await GetReviewsInCategory(typeId)
                
            };
        }
    } 
}
