using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WorldVision.Services.IServices;

namespace WorldVision.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReviewsService _reviewsService;
        public HomeController(IReviewsService reviewsService)
        {
            _reviewsService = reviewsService;
        }
        public async Task<IActionResult> Index()
        {
            var models =  await _reviewsService.GetPopularTagsAsync();
            return View(models);
        }
    }
}
