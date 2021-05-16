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

        
        public async Task<IActionResult> Index(int id, OverviewViewModel data)
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
            ViewBag.theDay = theDay;

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
            var plist = new List<Intake>();
            foreach (var item in nutritionAppContext.ToList() )
            {
                
                if (item.Product != null)
                {
                    KcalList.Add(item.Product.Kcal);
                    //KcalList.Add(item.Product.Kcal);
                }
                else
                {
                    //var MealKcals = new List<int>();
                    var totalMealKcal = 0;
                    foreach (var ingr in item.Meal.Ingredients)
                    {
                        //MealKcals.Add(ingr.Product.Kcal);

                        var prod_id = ingr.ProductId;
                        var prodContext = _context.Products
                            .Where(p => p.ProductId == prod_id)
                            .FirstOrDefault()
                            ;

                        Console.Write(prodContext);

                        
                        totalMealKcal += prodContext.Kcal;
                    }
                    KcalList.Add(totalMealKcal);
                }

                //var productIds = new List<int>();
                //if (item.Meal != null)
                //{
                //    var ingredients = item.Meal.Ingredients;
                //    foreach (var ingredient in ingredients)
                //    {
                //        var ing = ingredient.ProductId;
                //        productIds.Add(ing);
                //    }
                //    ViewBag.List = productIds;

                //    var kcals = new List<Product>();
                //    foreach (var pId in productIds)
                //    {
                //        var productContext = _context.Products
                //             .Where(p => p.ProductId == pId)
                //             .Include(p => p.Kcal)
                //           ;
                //    }
                //    i++;

                //    var total = 0;
                //    foreach (var k in kcals)
                //    {
                //        total += k.Kcal;
                //    }
                //    ViewBag.Kcals = total;
                //}
                
            }
            ViewData["KcalList"] = KcalList;
            ViewData["plist"] = plist;


            //EmployeeDBContext dbContext = new EmployeeDBContext();
            //List<Department> listDepartments = dbContext.Departments.ToList();
            //return View(listDepartments);

            return View(await nutritionAppContext.ToListAsync());
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
                    && s.Day < theDay.AddDays(1)))
                    .Include(i => i.Product)
                    .Include(i => i.Meal)
                    //.ThenInclude(i => i.Ingredients)
                   .ToListAsync()
                    ;

            var getMealId = _context.Intakes
                    .Where(s =>
                    s.User.Id == theid
                    &&
                    (s.Day >= theDay
                    && s.Day < theDay.AddDays(1)))
                    .Include(i => i.Meal)
                    .ToArrayAsync()
                    ;


            ViewBag.theDay = theDay;
            return View(await nutritionAppContext);
        }
    }
}
