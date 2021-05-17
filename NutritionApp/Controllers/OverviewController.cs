using Microsoft.AspNetCore.Mvc;
using NutritionApp.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NutritionApp.Data;
using System.Security.Claims;
using System.Collections.Generic;
using NutritionApp.Models.ViewModels;
using System.Collections;

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

        
        public async Task<IActionResult> Index(int id)
        {
            AppUser user = await CurrentUser;
            var username = HttpContext.User.Identity.Name;
            var theid = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var weight = user.Weight;
            var weightGoal = user.WeightGoal;
            var kcalStandard = 28;
            
            if (weightGoal < weight)
            {
                kcalStandard = 23;
            }

            var kcalGoal = Convert.ToInt32(kcalStandard * weight);

            ViewBag.KcalGoal = kcalGoal;

            if (id < 0) { id = 0; };
            ViewBag.Id = theid;
            ViewBag.Name = username;
            ViewBag.Count = id;

            var minusCount = (-1 * id);
            var theDay = DateTime.Today.AddDays(minusCount);
            ViewBag.theDay = theDay.ToString("dd/MM");

            var nutritionAppContext = _context.Intakes
                    .Where(s =>
                    s.User.Id == theid
                    &&
                    (s.Day >= theDay
                    && s.Day < theDay.AddDays(1)))
                    .Include(i => i.Product)
                    .Include(i => i.Meal)
                    .ThenInclude(i => i.Ingredients)
                    ;

            var i = 0;
            var KcalList = new List<int>();
            var Proteins = new List<decimal>();
            var Fats = new List<decimal>();
            var Carbs = new List<decimal>();
            var Stats = new List<string>();
            var plist = new List<Intake>();

            var vm = new List<OverviewViewModel>();
            foreach (var item in nutritionAppContext.ToList() )
            {
                
                if (item.Product != null)
                {
                    KcalList.Add(item.Product.Kcal);
                    Proteins.Add(item.Product.Protein);
                    Carbs.Add(item.Product.Carbs);
                    Fats.Add(item.Product.Fat);
                    //KcalList.Add(item.Product.Kcal);
                    // use viewmodel to collect data - send viewmodel to vm list ?
                }
                else
                {
                    //var MealKcals = new List<int>();
                    var totalMealKcal = 0;
                    decimal totalProtein = 0;
                    decimal totalFat = 0;
                    decimal totalCarbs = 0;
                    foreach (var ingr in item.Meal.Ingredients)
                    {
                        //MealKcals.Add(ingr.Product.Kcal);

                        var prod_id = ingr.ProductId;
                        var prodContext = _context.Products
                            .Where(p => p.ProductId == prod_id)
                            .FirstOrDefault()
                            ;

                        
                        totalMealKcal += prodContext.Kcal;
                        totalProtein += prodContext.Protein;
                        totalFat += prodContext.Fat;
                        totalCarbs += prodContext.Carbs;
                    }
                    KcalList.Add(totalMealKcal);
                    Proteins.Add(totalProtein);
                    Fats.Add(totalFat);
                    Carbs.Add(totalCarbs);
                }
            }

            ViewData["KcalList"] = KcalList;
            ViewData["plist"]   = plist;
            ViewData["Proteins"] = Proteins;
            ViewData["Fats"] = Fats;
            ViewData["Carbs"] = Carbs;

            return View(await nutritionAppContext.ToListAsync());
        }
    }
}
