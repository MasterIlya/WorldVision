using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
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
            var models =  await _reviewsService.GetGeneralPageModelAsync();
            return View(models);
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult SetTheme(string data)
        {
            CookieOptions cookies = new CookieOptions();
            cookies.Expires = DateTime.Now.AddDays(1);

            Response.Cookies.Append("theme", data, cookies);

            return Ok();
        }
    }
}
