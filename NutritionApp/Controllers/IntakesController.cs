using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    public class IntakesController : Controller
    {
        private readonly NutritionAppContext _context;

        private UserManager<AppUser> userManager;
        private readonly ILogger<IntakesController> _logger;
        public IntakesController(NutritionAppContext context, UserManager<AppUser> userMgr, ILogger<IntakesController> logger)
        {
            _context = context;
            userManager = userMgr;
            _logger = logger;
        }

        private Task<AppUser> CurrentUser =>
            userManager.FindByNameAsync(HttpContext.User.Identity.Name);

        // GET: Intakes
        public async Task<IActionResult> Index()
        {
            AppUser user = await CurrentUser;
            var username = HttpContext.User.Identity.Name;
            var id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Name = username;
            ViewBag.Id = id;
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


            SelectList prod  = new SelectList(_context.Products, "ProductId", "ProductId");
            SelectList mea  = new SelectList(_context.Meals, "MealId", "MealId");
            SelectList listt = new SelectList(prod.Concat(mea));
            Console.WriteLine(prod);
            Console.WriteLine(mea);
            Console.WriteLine(listt);
            ViewData["AllItems"] = listt;


            ViewData["MealId"] = new SelectList(_context.Meals, "MealId", "MealId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id");
            return View();
        }

        // POST: Intakes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
