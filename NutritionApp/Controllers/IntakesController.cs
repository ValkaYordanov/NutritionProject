using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NutritionApp.Data;
using NutritionApp.Models;
using NutritionApp.Models.ViewModels;

namespace NutritionApp.Controllers
{
    public class IntakesController : Controller
    {
        private readonly NutritionAppContext _context;

        public IntakesController(NutritionAppContext context)
        {
            _context = context;
        }

        // GET: Intakes
        public async Task<IActionResult> Index()
        {
            var nutritionAppContext = _context.Intakes.Include(i => i.Meal).Include(i => i.Product).Include(i => i.User);
            return View(await nutritionAppContext.ToListAsync());
        }

        // GET: Intakes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intake = await _context.Intakes
                .Include(i => i.Meal)
                .Include(i => i.Product)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.IntakeId == id);
            if (intake == null)
            {
                return NotFound();
            }

            return View(intake);
        }

        // GET: Intakes/Create
        public IActionResult Create()
        {
            IntakeViewModel intake = new IntakeViewModel();
            intake.Day = DateTime.Now;
            intake.Quantity = 1;

            //ViewData["MealId"] = new SelectList(_context.Meals, "MealId", "MealName");
            //ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "UserName");
            ViewBag.Intakes = _context.Intakes;
            return View(intake);
        }

        public JsonResult SelectFromSp(string input)
        {
            SqlParameter inputs = new SqlParameter("@input", input);

            List<Product> products =
                _context.Products.FromSqlRaw<Product>(
                    "exec spSearchProductAndMeal @input", inputs).ToList();

            List<Meal> meals =
                _context.Meals.FromSqlRaw<Meal>(
                    "exec spSearchMeal @input", inputs).ToList();

            var myResult = new
            {
                ProductList = products,
                MealList = meals
            };
            return Json(myResult);


        }

        // POST: Intakes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IntakeViewModel data)
        {
            Intake intake = new Intake();
            if (ModelState.IsValid)
            {

                //intake.IntakeId = data.Id;
                intake.Quantity = data.Quantity;
                intake.Day = data.Day;
                intake.UserId = data.UserId;

                if (data.Type == "product")
                {
                    intake.ProductId = data.ItemId;
                  
                 
                }  else if (data.Type == "meal")
                {
                    intake.MealId = data.ItemId;
                   
                   
                }

                _context.Add(intake);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["MealId"] = new SelectList(_context.Meals, "MealId", "MealId", intake.MealId);
            //ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", intake.ProductId);
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id", data.UserId);
            return View(data);
        }

        // GET: Intakes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intake = await _context.Intakes.FindAsync(id);
            if (intake == null)
            {
                return NotFound();
            }
            ViewData["MealId"] = new SelectList(_context.Meals, "MealId", "MealId", intake.MealId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", intake.ProductId);
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id", intake.UserId);
            return View(intake);
        }

        // POST: Intakes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IntakeId,UserId,MealId,ProductId,Quantity,Day")] Intake intake)
        {
            if (id != intake.IntakeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(intake);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IntakeExists(intake.IntakeId))
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
            ViewData["MealId"] = new SelectList(_context.Meals, "MealId", "MealId", intake.MealId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", intake.ProductId);
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id", intake.UserId);
            return View(intake);
        }

        // GET: Intakes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intake = await _context.Intakes
                .Include(i => i.Meal)
                .Include(i => i.Product)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.IntakeId == id);
            if (intake == null)
            {
                return NotFound();
            }

            return View(intake);
        }

        // POST: Intakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var intake = await _context.Intakes.FindAsync(id);
            _context.Intakes.Remove(intake);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IntakeExists(int id)
        {
            return _context.Intakes.Any(e => e.IntakeId == id);
        }
    }
}
