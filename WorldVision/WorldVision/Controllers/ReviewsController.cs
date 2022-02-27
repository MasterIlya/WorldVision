using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        public ReviewsController(IReviewsService reviewsService, IUsersService usersService)
        {
            _reviewsService = reviewsService;
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateReview()
        {
            var model = new CompositeCreateReviewModel
            { 
                Types = await _reviewsService.GetAllReviewTypesAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(CompositeCreateReviewModel model, string email)
        {
            if (ModelState.IsValid)
            {
                var user = await _usersService.GetUserByEmailAsync(email);

                model.ReviewModel.UserId = user.UserId;

                await _reviewsService.CreateAsync(model);

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
                ReviewModel = await _reviewsService.GetReviewAsync(reviewId),
                Types = await _reviewsService.GetAllReviewTypesAsync()
            };

            return View("CreateReview", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReview(CompositeCreateReviewModel model, string email)
        {
            if (ModelState.IsValid)
            {
                var user = await _usersService.GetUserByEmailAsync(email);

                model.ReviewModel.UserId = user.UserId;

                await _reviewsService.UpdateAsync(model);

                return RedirectToAction("GetUserReviews", new { email });
            }
            else
            {
                model.Types = await _reviewsService.GetAllReviewTypesAsync();

                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveReview(int reviewId, string email)
        {
            await _reviewsService.RemoveAsync(reviewId);

            return RedirectToAction("GetUserReviews", new { email });
        }

        public async Task<IActionResult> GetUserReviews(string email, int currentPage)
        {
            var reviews = await _reviewsService.GetUserReviewsAsync(currentPage, email);

            return View("UserReviews", reviews);
        }
    }
}
