using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WorldVision.Commons.Enums;
using WorldVision.Services.IServices;
using WorldVision.Services.Models;

namespace WorldVision.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationUserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _usersService.GetUserByEmailAsync(model.Email);
                if (user == null || user.Delisted)
                {
                    await _usersService.CreateAsync(model);

                    return View("SuccessRegistration");
                }
                else
                {
                    ModelState.AddModelError("", "Such user is blocked or already exists!");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Authentication()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("SocialNetworkResponse") };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet]
        public IActionResult FacebookLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("SocialNetworkResponse") };

            return Challenge(properties, FacebookDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> SocialNetworkResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities.FirstOrDefault().Claims.ToList();

            var socialNetworkUser = new SocialNetworkAuthenticationModel
            {
                Email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                FName = claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value,
                LName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value
            };

            if (await _usersService.GetUserByEmailAsync(socialNetworkUser.Email) == null)
            {
                await _usersService.CreateAsync(socialNetworkUser);
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Login(AuthenticationUserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _usersService.GetUserByEmailAsync(userModel.Email);

                if (user != null && user.State != StateTypes.Blocked && user.Password == userModel.Password )
                {

                    await Authenticate(user.UserId, user.Email, user.FName, user.Role);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorrect login and/or password, or you are blocked");
            }

            return View("Authentication", userModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers(int currentPage)
        {
            var model = await _usersService.GetUsersAsync(currentPage);

            return View("Users", model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeState(int[] ids, string btn)
        {
            if (btn == "block")
            {
                await _usersService.BlockUsersAsync(ids);
            }
            else if (btn == "unblock")
            {
                await _usersService.UnblockUsersAsync(ids);
            }
            else if (btn == "delete")
            {
                await _usersService.DeleteUsersAsync(ids);
            }
            else if (btn == "getadminright")
            {
                await _usersService.GetAdminRightsAsync(ids);
            }

            return RedirectToAction("GetUsers");

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SearchByEmail(string search, int currentPage)
        {
            var users = await _usersService.SearchByEmailAsync(search, currentPage);

            return View("Users", users);
        }
         

        private async Task Authenticate(int userId, string email, string name, RoleTypes role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, Enum.GetName(typeof(RoleTypes), role))
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
