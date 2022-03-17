using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WorldVision.Services.IServices;
using WorldVision.Services.Models;

namespace WorldVision.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewsService _reviewsService;
        private readonly IUsersService _usersService;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IElasticSearchService _elasticSearchService;

        public ReviewsController(IReviewsService reviewsService, IUsersService usersService,
            ICloudinaryService cloudinaryService, IElasticSearchService elasticSearchService)
        {
            _reviewsService = reviewsService;
            _usersService = usersService;
            _cloudinaryService = cloudinaryService;
            _elasticSearchService = elasticSearchService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateReview(string imgUrl)
        {
            var model = new CompositeCreateReviewModel
            {
                Types = await _reviewsService.GetAllReviewTypesAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task UploadImage(IFormFile file, string email, string pageId)
        {
            var fileName = file.FileName;
            var fileResult = await _cloudinaryService.LoadAsync(fileName, file.OpenReadStream());
            var imgUrl = fileResult.Url.ToString();
            var imgSize = fileResult.Bytes;
            _cloudinaryService.AddToCache(imgUrl, imgSize, fileName, email, pageId);
        }

        [HttpPost]
        public async Task DeleteImage(IFormFile file, string email, string pageId)
        {
            var models = _cloudinaryService.GetFromCache(pageId, email);
            var url = models.FirstOrDefault(x => x.ImageName == file.FileName).ImageURL;
            await _cloudinaryService.DeleteAsync(url);
            _cloudinaryService.DeleteCacheElement(pageId, email, file.FileName);
        }

        [HttpPost]
        public async Task DeleteImageOnUpdate(string fileName, int imageId)
        {
            await _cloudinaryService.DeleteImageOnUpdateAsync(fileName);
            await _reviewsService.RemoveImageAsync(imageId);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(CompositeCreateReviewModel model, string email, string pageId)
        {
            if (ModelState.IsValid)
            {
                var user = await _usersService.GetUserByEmailAsync(email);

                model.ReviewModel.UserId = user.UserId;

                var reviewId = await _reviewsService.CreateAsync(model);

                await _elasticSearchService.GetReviewIndexAsync(await _reviewsService.GetReviewAsync(reviewId));

                var models = _cloudinaryService.GetFromCache(pageId, email);

                await _reviewsService.CreateReviewImagesAsync(models, reviewId);

                return RedirectToAction("GetUserReviews", new { email });
            }
            else
            {
                model.Types = await _reviewsService.GetAllReviewTypesAsync();

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateReview(int reviewId)
        {
            var model = new CompositeCreateReviewModel
            {
                ReviewModel = await _reviewsService.GetReviewAsyncForUpdate(reviewId),
                Types = await _reviewsService.GetAllReviewTypesAsync(),
                Images = await _reviewsService.GetImagesAsync(reviewId)
            };

            return View("CreateReview", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReview(CompositeCreateReviewModel model, string email, string pageId)
        {
            if (ModelState.IsValid)
            {
                var user = await _usersService.GetUserByEmailAsync(email);

                model.ReviewModel.UserId = user.UserId;

                await _elasticSearchService.GetReviewIndexAsync(await _reviewsService.GetReviewAsync(model.ReviewModel.ReviewId));

                await _reviewsService.UpdateAsync(model);

                var models = _cloudinaryService.GetFromCache(pageId, email);

                if (models != null)
                {
                    await _reviewsService.CreateReviewImagesAsync(models, model.ReviewModel.ReviewId);
                }

                return RedirectToAction("GetUserReviews", new { email });
            }
            else
            {
                model.Types = await _reviewsService.GetAllReviewTypesAsync();
                model.Images = await _reviewsService.GetImagesAsync(model.ReviewModel.ReviewId);

                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveReview(int reviewId, string email)
        {
            await _reviewsService.RemoveAsync(reviewId);
            await _elasticSearchService.DeleteReviewAsync(reviewId);
            return RedirectToAction("GetUserReviews", new { email });
        }

        public async Task<IActionResult> GetUserReviews(string email, int currentPage)
        {
            var reviews = await _reviewsService.GetUserReviewsAsync(currentPage, email);

            return View("UserReviews", reviews);
        }

        public async Task<IActionResult> GetReview(int reviewId, string type, string email)
        {
            var model = await _reviewsService.GetReviewAsync(reviewId, type, email);

            return View("Review", model);
        }

        [HttpPost]
        public async Task AddLike(int reviewId, string email)
        {
            await _reviewsService.AddLikeAsync(reviewId, email);
        }

        [HttpPost]
        public async Task RemoveLike(int reviewId, string email)
        {
            await _reviewsService.RemoveLikeAsync(reviewId, email);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string search, int currentPage)
        {
            var models = await _elasticSearchService.SearchReviewsAsync(search, currentPage);

            return View("Reviews", models);
        }
    }
}
