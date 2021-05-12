using Microsoft.AspNetCore.Mvc;
using NutritionApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NutritionApp.Data;
using NutritionApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using System.Security.Claims;


namespace NutritionApp.Controllers
{
    public class OverviewController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly NutritionAppContext _context;
        public OverviewController(UserManager<AppUser> userMgr, NutritionAppContext context)
        {
            _userManager = userMgr;
            _context = context;
        }

        private Task<AppUser> CurrentUser => _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

        public async Task<IActionResult> Index()
        {
            AppUser user = await CurrentUser;
            var username = HttpContext.User.Identity.Name;
            var id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Id = id;

            var today = DateTime.Today;

            var nutritionAppContext = _context.Intakes
                    .Where(s => 
                    s.User.Id == id 
                    && s.Day >= DateTime.Today
                    );
            ViewBag.theDay = "oWo";
            return View(await nutritionAppContext.ToListAsync());
            //return View();
        }

        public async Task<IActionResult> Today()
        {
            AppUser user = await CurrentUser;
            var username = HttpContext.User.Identity.Name;
            var id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Id = id;

            var today = DateTime.Today;

            var nutritionAppContext = _context.Intakes
                    .Where(s =>
                    s.User.Id == id
                    && s.Day >= DateTime.Today
                    );
            ViewBag.theDay = "Today";
            return View(await nutritionAppContext.ToListAsync());
            //return View();
        }

        public async Task<IActionResult> Yesterday()
        {
            AppUser user = await CurrentUser;
            var username = HttpContext.User.Identity.Name;
            var id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Id = id;

            var Yesterday = DateTime.Today;

            var nutritionAppContext = _context.Intakes
                    .Where(s =>
                    s.User.Id == id
                    && 
                    ( s.Day >= DateTime.Today.AddDays(-1) 
                    && s.Day <= DateTime.Today )
                    );
            ViewBag.theDay = "YEsterday";
            //return View(await nutritionAppContext.ToListAsync());
            return View();
        }
    }
}
