using Microsoft.AspNetCore.Mvc;
using NutritionApp.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NutritionApp.Data;
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
            ViewBag.theDay = today;
            return View(await nutritionAppContext.ToListAsync());
            //return View();
        }

        public async Task<IActionResult> Yesterday()
        {
            AppUser user = await CurrentUser;
            var username = HttpContext.User.Identity.Name;
            var id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Id = id;

            var minusCount = 1;
            var theDay = DateTime.Today.AddDays(-minusCount);

            var nutritionAppContext = _context.Intakes
                    .Where(s =>
                    s.User.Id == id
                    && 
                    ( s.Day >= theDay
                    && s.Day < theDay.AddDays(1))
                    );
            ViewBag.theDay = theDay;
            return View(await nutritionAppContext.ToListAsync());
            //return View();
        }

        public async Task<IActionResult> Day(int id)
        {
            AppUser user = await CurrentUser;
            var username = HttpContext.User.Identity.Name;
            var theid = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id < 0) { id = 0; };
            ViewBag.Id = theid;
            ViewBag.Name = username;
            ViewBag.Count = id;

            var minusCount = (-1 * id);
            var theDay = DateTime.Today.AddDays(minusCount);

            var nutritionAppContext = _context.Intakes
                    .Where(s =>
                    s.User.Id == theid
                    &&
                    (s.Day >= theDay
                    && s.Day < theDay.AddDays(1))
                    ).Include(i => i.Meal).ThenInclude(i => i.Ingredients)
                    .Include(i => i.Product);
            ViewBag.theDay = theDay;
            return View(await nutritionAppContext.ToListAsync());
        }
    }
}
