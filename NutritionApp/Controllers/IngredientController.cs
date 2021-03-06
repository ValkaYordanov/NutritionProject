using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NutritionApp.Models.ViewModels;
using NutritionApp.Data;
using NutritionApp.Models;
using System.Web;

using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace NutritionApp.Controllers
{
    public class IngredientController : Controller
    {

        private readonly NutritionAppContext _context;
        private Basket basket;
        public static HttpContext Current { get; set; }

        public IngredientController(NutritionAppContext context, Basket basketService)
        {
            _context = context;
            basket = basketService;
        }

        //public IngredientController()
        //{
        //}

        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Ingredients.ToListAsync());
        //}
        public void AddIngredient(int mealId, Basket basketObj)
        {
            foreach (BasketLine line in basketObj.Lines)
            {
            Ingredient ingredient = new Ingredient();
               
                ingredient.MealId = mealId;
                ingredient.ProductId = line.Product.ProductId;
                ingredient.Quantity = line.Quantity;
                _context.Add(ingredient);
                
            }
          
              _context.SaveChanges();
         

        }

        //public async void Delete(int id)
        //{
        //    var meal = await _context.Ingredients.FindAsync(id);
        //    _context.Ingredients.Remove(meal);
        //    await _context.SaveChangesAsync();

        //}

        //// POST: Meals/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async void DeleteConfirmed(int id)
        //{
        //    var meal = await _context.Ingredients.FindAsync(id);
        //    _context.Ingredients.Remove(meal);
        //    await _context.SaveChangesAsync();
        //}
    }
}
