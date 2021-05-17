using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;
        private readonly NutritionAppContext _context;

        public IntakesController(UserManager<AppUser> userMgr, NutritionAppContext context)
        {
            _userManager = userMgr;
            _context = context;
        }
        private Task<AppUser> CurrentUser => _userManager.FindByNameAsync(HttpContext.User.Identity.Name);


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
        public IActionResult Create(int id)
        {
         
            var minusCount = -1 * id;
            var theDay = DateTime.Today.AddDays(minusCount);
            ViewBag.theDay = theDay.ToString("dd/MM");
            ViewBag.Count = id;


            IntakeViewModel intakeView = new IntakeViewModel();
            intakeView.Day = DateTime.Now;
            intakeView.Quantity = 1.8M;
            intakeView.Ingredients = _context.Ingredients;
            intakeView.Intakes = _context.Intakes
               .Include(i => i.Meal).ThenInclude(i => i.Ingredients)
               .Include(i => i.Product)
               .Include(i => i.User).ToList();
            //intakeView.UserId = "1631d3b8-9724-4baf-a053-227c5ac06df6";
            var userid = _context.AppUsers.Where(i => i.Email == HttpContext.User.Identity.Name).First();
            intakeView.UserId = userid.Id;


            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "UserName");

        
            return View(intakeView);
        }

        public JsonResult SelectFromSp(string input, string userId)
        {
            SqlParameter inputs = new SqlParameter("@input", input);
            SqlParameter currentUserId = new SqlParameter("@userId", userId);

            List<Product> products =
                _context.Products.FromSqlRaw<Product>(
                    "exec spSearchProductAndMeal @input", inputs).ToList();

            List<Meal> meals =
                _context.Meals.FromSqlRaw<Meal>(
                    "exec spSearchMeal @input, @userId", inputs, currentUserId).ToList();

            var myResult = new
            {
                ProductList = products,
                MealList = meals
            };
            return Json(myResult);
        }


        public ActionResult GetCalories(int id)
        {
            SqlParameter currentId = new SqlParameter("@id", id);
            List<Intake> inta=_context.Intakes.FromSqlRaw<Intake>("select * from Intake where IntakeId > @id", currentId).ToList();
            List<Product> p=_context.Products.FromSqlRaw<Product>("select * from Product where ProductId > @id", currentId).ToList();
            List<Ingredient> ingre =_context.Ingredients.FromSqlRaw<Ingredient>("select * from Ingredient where IngredientId > @id", currentId).ToList();
               return Json(p);
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
                intake.Quantity = Convert.ToDecimal(data.Quantity, CultureInfo.InvariantCulture);
                intake.Day = data.Day;
                //intake.Day = ViewBag.theDay;

                intake.UserId = data.UserId;

                if (data.Type == "product")
                {
                    intake.Product = _context.Products.Where(p => p.ProductId == data.ItemId).First();

                }  else if (data.Type == "meal")
                {

                    intake.Meal = _context.Meals.Where(p => p.MealId == data.ItemId).First();

                   
                }



                _context.Add(intake);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch(Exception e)
                {

                    Console.WriteLine(e.Message);

                }
                return RedirectToAction(nameof(Create));
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

            //var intake = await _context.Intakes.FindAsync(id);
            var intake = await _context.Intakes.Include(i => i.Meal).Include(i => i.Product).FirstOrDefaultAsync(i => i.IntakeId == id);
            if (intake == null)
            {
                return NotFound();
            }

            IntakeViewModel intakeView = new IntakeViewModel();
            intakeView.Quantity = intake.Quantity;
            intakeView.Day = intake.Day;
            intakeView.UserId = intake.UserId;
            intakeView.IntakeId = intake.IntakeId;
            

             if (intake.Product != null)
            {
                intakeView.Type = "product";
                intakeView.ItemId = intake.Product.ProductId;
                intakeView.ItemName = intake.Product.ProductName;

            }
            else if (intake.Meal != null)
            {

                intakeView.Type = "meal";
                intakeView.ItemId = intake.Meal.MealId;
                intakeView.ItemName = intake.Meal.MealName;
            }

           
          
            //ViewData["MealId"] = new SelectList(_context.Meals, "MealId", "MealId", intake.MealId);
            //ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", intake.ProductId);
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id", intake.UserId);
            return View(intakeView);
        }

        // POST: Intakes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IntakeViewModel data)
        {
           
            if (id != data.IntakeId)
            {
                return NotFound();
            }
            Intake intake = _context.Intakes.Where(i => i.IntakeId == id).First();

            if (ModelState.IsValid)
            {
                try
                {
                    //intake.IntakeId = data.Id;
                    intake.Quantity = data.Quantity;
                    //intake.Day = data.Day;
                    //intake.UserId = data.UserId;

                    if (data.Type == "product")
                    {
                        intake.Product = _context.Products.Where(p => p.ProductId == data.ItemId).First();
                        if (intake.Meal != null)
                        {
                            intake.Meal = null;
                        }

                    }
                    else if (data.Type == "meal")
                    {

                        intake.Meal = _context.Meals.Where(p => p.MealId == data.ItemId).First();
                        if (intake.Product != null)
                        {
                            intake.Product = null;

                        }

                    }

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
            //ViewData["MealId"] = new SelectList(_context.Meals, "MealId", "MealId", intake.MealId);
            //ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", intake.ProductId);
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
            intake.IntakeId = (int) id;
            return View(intake);
        }

        // POST: Intakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //IntakeViewModel intakeView = new IntakeViewModel();

            //intakeView.IntakeId = id;

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
