using System;
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
        private readonly IReviewLikesRepository _reviewLikesRepository;
        private readonly IReviewTagCounterRepository _reviewTagCounterRepository;
        private readonly IReviewTagsRepository _reviewTagsRepository;
        private readonly IReviewRaitingRepository _reviewLikeCounterRepository;
        private const int DefaultReviewsCount = 10;
        private const int DefaultTagsCount = 20;
        private const int DefaultCurrentPage = 1;

        public ReviewsService(IReviewsRepository reviewsRepository, IReviewTypesRepository reviewTypesRepository,
            IUsersRepository usersRepository, IReviewImagesRepository reviewImagesRepository,
            IReviewLikesRepository reviewLikesRepository, IReviewTagCounterRepository reviewTagCounterRepository,
            IReviewTagsRepository reviewTagsRepository, IReviewRaitingRepository reviewLikeCounterRepository)
        {
            _reviewsRepository = reviewsRepository;
            _reviewTypesRepository = reviewTypesRepository;
            _usersRepository = usersRepository;
            _reviewImagesRepository = reviewImagesRepository;
            _reviewLikesRepository = reviewLikesRepository;
            _reviewTagCounterRepository = reviewTagCounterRepository;
            _reviewTagsRepository = reviewTagsRepository;
            _reviewLikeCounterRepository = reviewLikeCounterRepository;
        }

        public async Task<int> CreateAsync(CompositeCreateReviewModel model)
        {
            var item = ReviewsMapper.Map(model.ReviewModel);
            await _reviewsRepository.CreateAsync(item);
            var tags = model.ReviewModel.Tags.Split(", ");
            foreach (var tag in tags)
            {
                var tagItem = ReviewsMapper.Map(tag, item.ReviewId);
                await _reviewTagsRepository.CreateAsync(tagItem);
            }
            return item.ReviewId;
        }

        public async Task UpdateAsync(CompositeCreateReviewModel model)
        {
            var item = await _reviewsRepository.GetAsync(model.ReviewModel.ReviewId);
            var tags = await _reviewTagsRepository.GetReviewTagsAsync(model.ReviewModel.ReviewId);
            foreach(var tag in tags)
            {
                await _reviewTagsRepository.RemoveAsync(tag);
            }
            var newTags = model.ReviewModel.Tags.Split(", ");
            foreach (var tag in newTags)
            {
                var tagItem = ReviewsMapper.Map(tag, item.ReviewId);
                await _reviewTagsRepository.CreateAsync(tagItem);
            }
            var newItem = ReviewsMapper.UpdateMap(item, model.ReviewModel);

            await _reviewsRepository.UpdateAsync(newItem);
        }

        public async Task RemoveAsync(int reviewId)
        {
            var item = await _reviewsRepository.GetAsync(reviewId);
            var tags = await _reviewTagsRepository.GetReviewTagsAsync(reviewId);
            foreach (var tag in tags)
            {
                await _reviewTagsRepository.RemoveAsync(tag);
            }
            await _reviewsRepository.RemoveAsync(item);
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
            Dictionary<int, List<Repositories.Items.ReviewTagItem>> tags = new Dictionary<int, List<Repositories.Items.ReviewTagItem>>();
            Dictionary<int, int> ratings = new Dictionary<int, int>();
            foreach(var item in items)
            {
                var itemTags = await _reviewTagsRepository.GetReviewTagsAsync(item.ReviewId);
                var rating = await _reviewLikesRepository.GetReviewLikeCountAsync(item.ReviewId);
                tags.Add(item.ReviewId, itemTags);
                ratings.Add(item.ReviewId, rating);
            }

            var modelsList = items.Select(x => ReviewsMapper.Map(x, typeItems, fullName, tags[x.ReviewId], ratings[x.ReviewId])).ToList();

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

            var userIds = items.Select(x => x.UserId).Distinct().ToList();
            var users = await _usersRepository.GetUsersAsync(userIds);
            var usersToDict = users.ToDictionary(x => x.UserId);

            var reviewIds = items.Select(x => x.ReviewId).ToList();
            var likes = await _reviewLikesRepository.GetReviewsLikeCountAsync(reviewIds);

            var models = items.Select(x => ReviewsMapper.Map(x, typeItems, usersToDict, likes)).ToList();

            return models;
        }

        public async Task<ReviewModel> GetReviewAsync(int reviewId)
        {
            var item = await _reviewsRepository.GetAsync(reviewId);
            var types = await _reviewTypesRepository.GetAllAsync();
            var tags = await _reviewTagsRepository.GetReviewTagsAsync(reviewId);
            var rating = await _reviewLikesRepository.GetReviewLikeCountAsync(reviewId);
            var userItem = await _usersRepository.GetAsync(item.UserId);
            var fullName = $"{userItem.FName} {userItem.LName}";
            var model = ReviewsMapper.Map(item, types, fullName, tags, rating);

            return model;
        }

        public async Task<CompositeReviewModel> GetReviewAsync(int reviewId, string type)
        {
            var types = await _reviewTypesRepository.GetAllAsync();
            var typeId = types.FirstOrDefault(x => x.ReviewType == type).ReviewTypeId;
            var reviewItem = await _reviewsRepository.GetAsync(reviewId);
            var tags = await _reviewTagsRepository.GetReviewTagsAsync(reviewId);
            var userItem = await _usersRepository.GetAsync(reviewItem.UserId);
            var fullName = $"{userItem.FName} {userItem.LName}";

            var rating = await GetReviewRatingAsync(reviewId);
            var review = ReviewsMapper.Map(reviewItem, types, fullName, tags, rating);
            var images = await GetImagesAsync(reviewId);
            var lastReviews = await GetReviewsInCategory(typeId);

            var compositeModel = ReviewsMapper.Map(review, images, lastReviews);

            return compositeModel;
        }

        public async Task<CompositeReviewModel> GetReviewAsync(int reviewId, string type, string currentEmail)
        {
            var types = await _reviewTypesRepository.GetAllAsync();
            var typeId = types.FirstOrDefault(x => x.ReviewType == type).ReviewTypeId;
            var reviewItem = await _reviewsRepository.GetAsync(reviewId);
            var tags = await _reviewTagsRepository.GetReviewTagsAsync(reviewId);
            var userItem = await _usersRepository.GetAsync(reviewItem.UserId);
            var fullName = $"{userItem.FName} {userItem.LName}";
            var currentUser = await _usersRepository.GetByEmailAsync(currentEmail);

            var rating = await GetReviewRatingAsync(reviewId);
            var review = ReviewsMapper.Map(reviewItem, types, fullName, tags, rating);
            var images = await GetImagesAsync(reviewId);
            var lastReviews = await GetReviewsInCategory(typeId);
            var currentUserLike = await GetLikeCurrentUserAsync(currentUser.UserId);

            var compositeModel = ReviewsMapper.Map(review, images, lastReviews, currentUserLike);

            return compositeModel;
        }

        public async Task AddLikeAsync(int reviewId, string email)
        {
            var user = await _usersRepository.GetByEmailAsync(email);
            var item = ReviewsMapper.Map(reviewId, user.UserId);

            await _reviewLikesRepository.CreateAsync(item);
        }

        public async Task RemoveLikeAsync(int reviewId, string email)
        {
            var user = await _usersRepository.GetByEmailAsync(email);
            var item = await _reviewLikesRepository.GetByUserIdAsync(user.UserId);

            await _reviewLikesRepository.RemoveAsync(item);
        }

        public async Task<int> GetReviewRatingAsync(int reviewId)
        {
            return await _reviewLikesRepository.GetReviewLikeCountAsync(reviewId);
        }

        public async Task<ReviewLikeModel> GetLikeCurrentUserAsync(int userId)
        {
            var item = await _reviewLikesRepository.GetByUserIdAsync(userId);

            var model = ReviewsMapper.Map(item);

            return model;
        }
        public async Task<List<PopularTagModel>> GetPopularTagsAsync()
        {
            var items = await _reviewTagCounterRepository.GetPopularTagsAsync(DefaultTagsCount);
            var models = items.Select(x => ReviewsMapper.Map(x)).ToList();
            return models;
        }

        public async Task<GeneralPageModel> GetGeneralPageModelAsync()
        {
            var tagItems = await _reviewTagCounterRepository.GetPopularTagsAsync(DefaultTagsCount);
            var tags = tagItems.Select(x => ReviewsMapper.Map(x)).ToList();

            var reviewTypes = await _reviewTypesRepository.GetAllAsync();
            var skip = 0;
            var take = DefaultReviewsCount;
            
            var lastReviewItems = await _reviewsRepository.GetAsync(skip, take);
            var userIds = lastReviewItems.Select(x => x.UserId).Distinct().ToList();
            var reviewsIds = lastReviewItems.Select(x => x.ReviewId).ToList();
            var users = await _usersRepository.GetUsersAsync(userIds);
            var usersToDict = users.ToDictionary(x => x.UserId);
            var likes = await _reviewLikesRepository.GetReviewsLikeCountAsync(reviewsIds);
            var lastReviews = lastReviewItems.Select(x => ReviewsMapper.Map(x, reviewTypes, usersToDict, likes)).ToList();

            var popularItems = await _reviewLikeCounterRepository.GetPopularReviewsAsync(take);
            var popularReviews = popularItems.Select(x => ReviewsMapper.Map(x, reviewTypes, usersToDict)).ToList();

            var model = ReviewsMapper.Map(lastReviews, popularReviews, tags);

            return model;
        }
    }
}
