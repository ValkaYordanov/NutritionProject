using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NutritionApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace NutritionApp.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<AppUser> userManager;
        public HomeController(UserManager<AppUser> userMgr)
        {
            userManager = userMgr;
        }

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index() => View(GetData(nameof(Index)));

        [Authorize(Roles = "Users")]
        public IActionResult OtherAction() => View("Index", GetData(nameof(OtherAction)));

        private Dictionary<string, object> GetData(string actionName) => new Dictionary<string, object>
        {
            ["Action"] = actionName,
            ["User"] = HttpContext.User.Identity.Name,
            ["Authenticated"] = HttpContext.User.Identity.IsAuthenticated,
            ["Auth Type"] = HttpContext.User.Identity.AuthenticationType,
            ["In Users Role"] = HttpContext.User.IsInRole("Users"),
            ["Gender"] = CurrentUser.Result.Gender,
        };

        [Authorize]
        public async Task<IActionResult> UserProps()
        {
            return View(await CurrentUser);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UserProps(
        [Required] Gender gender)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await CurrentUser;
                user.Gender = gender;
                await userManager.UpdateAsync(user);
                return RedirectToAction("Index");
            }
            return View(await CurrentUser);
        }
        private Task<AppUser> CurrentUser =>
            userManager.FindByNameAsync(HttpContext.User.Identity.Name);

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
