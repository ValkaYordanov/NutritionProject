using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NutritionApp.Data;
using NutritionApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace NutritionApp.Controllers
{
    public class SettingsController : Controller
    {
        private readonly NutritionAppContext _context;
        private readonly UserManager<AppUser> _userManager;

        public SettingsController(NutritionAppContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<AppUser> CurrentUser => _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index(Settings settings)
        {
            ViewBag.SelectedPage = "Settings";
            AppUser user = await CurrentUser;
            if (user != null)
            {
                ViewData["Genders"] = new SelectList(_context.Genders, "GenderId", "Label");
                settings.GenderId = user.GenderId;
                settings.Weight = user.Weight;
                settings.WeightGoal = user.WeightGoal;
            }
            return View(settings);
        }

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
                    return RedirectToAction("Index");
                }
            }

            return Json(new { });
        }

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
                    return RedirectToAction("Index");
                }
            }

            return Json(new { });
        }

        [HttpPost]
        public async Task<IActionResult> WeightGoal(decimal weightGoal)
        {
            AppUser user = await CurrentUser;
            if (user != null)
            {
                user.WeightGoal = weightGoal;
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Redirect("Index");
                }
            }

            return Json(new { });
        }
    }
}
