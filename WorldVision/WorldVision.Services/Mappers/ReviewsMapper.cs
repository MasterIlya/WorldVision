using System;
using System.Collections.Generic;
using System.Linq;
using WorldVision.Repositories.Items;
using WorldVision.Services.Models;

namespace WorldVision.Services.Mappers
{
    public static class ReviewsMapper
    {
        public static ReviewItem Map(CreateReviewModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new ReviewItem
            {
                UserId = model.UserId,
                ReviewTypeId = model.ReviewTypeId,
                Tags = model.Tags,
                Title = model.Title,
                Content = model.Content,
                AuthorScore = model.AuthorScore,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                Delisted = false
            };
        }

        public static ReviewModel Map(ReviewItem item, List<ReviewTypeItem> reviewTypes, string name)
        {
            if (item == null)
            {
                return null;
            }
            return new ReviewModel
            {
                ReviewId = item.ReviewId,
                UserId = item.UserId,
                UserName = name,
                ReviewType = reviewTypes.FirstOrDefault(x => x.ReviewTypeId == item.ReviewTypeId).ReviewType,
                Tags = item.Tags,
                Title = item.Title,
                Content = item.Content,
                AuthorScore = item.AuthorScore,
                CreateDate = item.CreateDate,
                UpdateDate = item.UpdateDate,
                Delisted = item.Delisted
            };
        }

        public static ReviewModel Map(ReviewItem item, List<ReviewTypeItem> reviewTypes, Dictionary<int, UserItem> users)
        {
            if (item == null)
            {
                return null;
            }
            return new ReviewModel
            {
                ReviewId = item.ReviewId,
                UserId = item.UserId,
                UserName = $"{users[item.UserId].FName} {users[item.UserId].LName}",
                ReviewType = reviewTypes.FirstOrDefault(x => x.ReviewTypeId == item.ReviewTypeId).ReviewType,
                Tags = item.Tags,
                Title = item.Title,
                Content = item.Content,
                AuthorScore = item.AuthorScore,
                CreateDate = item.CreateDate,
                UpdateDate = item.UpdateDate,
                Delisted = item.Delisted
            };
        }

        public static PaginationReviewModel Map(List<ReviewModel> reviews, int countOfPages, int currentPage)
        {
            return new PaginationReviewModel
            {
                Models = reviews,
                CountOfPages = countOfPages,
                CurrentPage = currentPage
            };
        }

        public static ReviewTypesModel Map(ReviewTypeItem item)
        {
            return new ReviewTypesModel
            {
                TypeId = item.ReviewTypeId,
                Type = item.ReviewType
            };
        }

        public static CreateReviewModel Map(ReviewItem item)
        {
            if (item == null)
            {
                return null;
            }
            return new CreateReviewModel
            {
                ReviewId = item.ReviewId,
                UserId = item.UserId,
                ReviewTypeId = item.ReviewTypeId,
                Tags = item.Tags,
                Title = item.Title,
                Content = item.Content,
                AuthorScore = item.AuthorScore
            };
        }

        public static ReviewImageItem Map(ReviewImageModel model, int reviewId)
        {
            if (model == null)
            {
                return null;
            }
            return new ReviewImageItem
            {
                ReviewId = reviewId,
                ImageURL = model.ImageURL,
                Size = model.ImageSize
            };
        }

        public static ReviewImageModel Map(ReviewImageItem item)
        {
            if (item == null)
            {
                return null;
            }
            return new ReviewImageModel
            {
                ImageId = item.ImageId,
                ImageName = item.ImageURL.Split(new char[] { '/' }).Last(),
                ImageURL = item.ImageURL,
                ImageSize = item.Size
            };
        }

        public static ReviewItem UpdateMap(ReviewItem item, CreateReviewModel model)
        {
            if (item == null)
            {
                return null;
            }

            item.AuthorScore = model.AuthorScore;
            item.Content = model.Content;
            item.Tags = model.Tags;
            item.Title = model.Title;
            item.UpdateDate = DateTime.UtcNow;
            item.ReviewTypeId = model.ReviewTypeId;

            return item;
        }

        public static ReviewLikeItem Map(int reviewId, int userId)
        {
            return new ReviewLikeItem
            {
                ReviewId = reviewId,
                UserId = userId
            };
        }

        public static CompositeReviewModel Map(ReviewModel review, List<ReviewImageModel> images, List<ReviewModel> lastReviews, int rating, ReviewLikeModel currentUserLike)
        {
            return new CompositeReviewModel
            {
                Review = review,
                Images = images,
                LastReviewsInCategory = lastReviews,
                Rating = rating,
                CurrentUserLike = currentUserLike
            };
        }

        public static ReviewLikeModel Map(ReviewLikeItem item)
        {
            if (item == null)
                return null;

            return new ReviewLikeModel
            {
                ReviewId = item.ReviewId,
                UserId = item.UserId
            };
        }
    }
}
