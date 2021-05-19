using NutritionApp.Models;
using NutritionApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ComponentModel.DataAnnotations;


namespace NutritionApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private readonly ILogger<AccountController> _logger;

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly NutritionAppContext _context;
        public AccountController(UserManager<AppUser> userMgr,
        SignInManager<AppUser> signinMgr,
        ILogger<AccountController> logger,
        NutritionAppContext context)
        {
            _userManager = userMgr;
            _signInManager = signinMgr;
            _logger = logger;
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(details.Email);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result =
                        await _signInManager.PasswordSignInAsync(user, details.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Create", "Intakes");
                    }
                }
                ModelState.AddModelError(nameof(LoginModel.Email), "Invalid user or password");
            }
            return View(details);
        }

        // GET: /Account/Create
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: /Account/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.Email, Email = model.Email, GenderId = 3 };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User created a new account with password.");
                    return RedirectToAction("Details");
                }
                AddErrors(result);
            }

            return View(model);
        }

        // GET: /Account/Details
        [HttpGet]
        [Authorize]
        public IActionResult Details()
        {
            ViewData["Genders"] = new SelectList(_context.Genders, "GenderId", "Label");

            return View();
        }

        private Task<AppUser> CurrentUser => _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

        // POST: /Account/Gender
        [HttpPost]
        public async Task<IActionResult> Gender(int genderId)
        {
            AppUser user = await CurrentUser;
            if (user != null)
            {
                user.GenderId = genderId;
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Details");
                }
                else
                {
                    AddErrors(result);
                }
            }

            return Json(new { });
        }

        // POST: /Account/Weight
        [HttpPost]
        public async Task<IActionResult> Weight(decimal weight)
        {
            AppUser user = await CurrentUser;
            if (user != null)
            {
                user.Weight = weight;
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Details");
                }
                else
                {
                    AddErrors(result);
                }
            }

            return Json(new { });
        }

        // POST: /Account/WeightGoal
        [HttpPost]
        public async Task<IActionResult> WeightGoal(decimal weightGoal, decimal weight, int genderId)
        {
            AppUser user = await CurrentUser;
            if (user != null)
            {
                user.WeightGoal = weightGoal;
                user.GenderId = genderId;
                user.Weight = weight;

                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Create", "Intakes");
                }
                else
                {
                    AddErrors(result);
                }
            }

            return Json(new { });
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
