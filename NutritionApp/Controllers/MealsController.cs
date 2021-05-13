using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NutritionApp.Data;
using NutritionApp.Models;
using NutritionApp.Models.ViewModels;

namespace NutritionApp.Controllers
{
    public class MealsController : Controller
    {
        private readonly NutritionAppContext _context;
        private Basket basket;
      

        public MealsController(NutritionAppContext context, Basket basketSesion)
        {
            _context = context;
            basket = basketSesion;
        }

        // GET: Meals
        public async Task<IActionResult> Index()
        {
            var nutritionAppContext = _context.Meals.Include(m => m.User);
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
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id", meal.UserId);
            return View(meal);
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
            return View(meal);
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
