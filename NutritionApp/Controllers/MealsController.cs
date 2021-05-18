using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NutritionApp.Data;
using NutritionApp.Models;
using Microsoft.AspNetCore.Identity;
using NutritionApp.Models.ViewModels;

namespace NutritionApp.Controllers
{
    public class MealsController : Controller
    {
        private readonly NutritionAppContext _context;
        private Basket basket;
        private Basket bas = new Basket();
        private readonly UserManager<AppUser> _userManager;

        public MealsController(NutritionAppContext context, Basket basketSesion, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            basket = basketSesion;
        }

        private Task<AppUser> CurrentUser => _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

        // GET: Meals
        public async Task<IActionResult> Index()
        {
            var user = await CurrentUser;
            var nutritionAppContext = _context.Meals.Where(m => m.User == user).Include(i => i.Ingredients)
                    .ThenInclude(i => i.Product);
            return View(await nutritionAppContext.ToListAsync());
        }

        // GET: Meals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MealId == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // GET: Meals/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "UserName");
            ViewData["AllProd"] = new SelectList(_context.Products, "ProductId", "ProductName");
            BasketIndexViewModel model = new BasketIndexViewModel();
          
            return View(model);
        }

        // POST: Meals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MealId,UserId,Quantity,MealName")] Meal meal)
        {
            IngredientController ingredientCtr = new IngredientController(_context, basket);
            if (ModelState.IsValid)
            {
                _context.Add(meal);
                await _context.SaveChangesAsync();
                int id = meal.MealId;
                ingredientCtr.AddIngredient(id,basket);

                basket.Clear();
                
                return RedirectToAction(nameof(Index));
            }
           
            //ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id", meal.UserId);
            //BasketIndexViewModel model = new BasketIndexViewModel();
            return View(meal);
        }
        


        // GET: Meals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }

            var ingredientsList = _context.Ingredients.Where(i => i.MealId == id);

                foreach (var ingredient in ingredientsList)
                {
                  
                        BasketLine line = new BasketLine();
                        Product prod = _context.Products.First(i => i.ProductId == ingredient.ProductId);
                   
                        line.Product = prod;
                        line.Quantity = Convert.ToInt32(ingredient.Quantity);
                        bas.Lines.Add(line);
                    
                }
            
            

            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id", meal.UserId);
            BasketIndexViewModel model = new BasketIndexViewModel();
            model.Meal = meal;
            model.Basket = bas;
            return View(model);
        }

        // POST: Meals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MealId,UserId,Quantity,MealName")] Meal meal)
        {
            if (id != meal.MealId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealExists(meal.MealId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id", meal.UserId);
            BasketIndexViewModel model = new BasketIndexViewModel();
            model.Meal = meal;
            model.Basket = bas;
            return View(model);
        }


        public RedirectToActionResult DeleteIngredientFromBasket(int ProductId, int MealId)
        {

            var ingredientsList = _context.Ingredients.Where(i => i.MealId == MealId);
            foreach (var ingredient in ingredientsList)
            {
                if (ingredient.ProductId == ProductId)
                {
                    //bas.Lines.Remove(ingredient);
                    var ingre = _context.Ingredients.Where(i => i.ProductId == ProductId && i.MealId == MealId).First();
                    _context.Ingredients.Remove(ingre);

                }
            }
                    _context.SaveChanges();
            //BasketIndexViewModel model = new BasketIndexViewModel();
            //model.Basket = bas;
            //model.Meal = _context.Meals.Where(i => i.MealId == MealId).First();
            return RedirectToAction("Edit",new { id=MealId });
        }


        // GET: Meals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MealId == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // POST: Meals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meal = await _context.Meals.FindAsync(id);
            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MealExists(int id)
        {
            return _context.Meals.Any(e => e.MealId == id);
        }
    }
}
