using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        //context
        private readonly NutritionAppContext _context;

        //authorize
        private UserManager<AppUser> _userManager;
        private readonly ILogger<OverviewController> _logger;
        public OverviewController(NutritionAppContext context, UserManager<AppUser> userMgr, ILogger<OverviewController> logger)
        {
            _context = context;
            _userManager = userMgr;
            _logger = logger;

        }

        private Task<AppUser> CurrentUser => _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

        //private Task<AppUser> CurrentUser => _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
        public async Task<IActionResult> Index()
        {
            AppUser user = await CurrentUser;
            var username = HttpContext.User.Identity.Name;
            var id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

           
                ViewBag.Name = username;
                ViewBag.Id = id;
                //var nutritionAppContext = _context.Intakes.Include(i => i.Meal).Include(i => i.Product).Include(i => i.User);
                //return View(await nutritionAppContext.ToListAsync());
                return View();
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IntakeId,UserId,MealId,ProductId,Quantity,Day")] Intake intake)
        {
            if (ModelState.IsValid)
            {
                _context.Add(intake);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MealId"] = new SelectList(_context.Meals, "MealId", "MealId", intake.MealId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", intake.ProductId);
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id", intake.UserId);
            return View(intake);
        }

    }
}
