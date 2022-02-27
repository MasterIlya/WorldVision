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

        public static ReviewModel Map(ReviewItem item, List<ReviewTypeItem> reviewTypes)
        {
            if (item == null)
            {
                return null;
            }
            return new ReviewModel
            {
                ReviewId = item.ReviewId,
                UserId = item.UserId,
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
    }
}
