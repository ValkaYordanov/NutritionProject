using Microsoft.AspNetCore.Mvc;
using NutritionApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NutritionApp.Data;
using NutritionApp.Models;

namespace NutritionApp.Controllers
{
    public class BasketController : Controller
    {
        private readonly NutritionAppContext _context;
        private Basket basket;
        public BasketController(Basket basketService, NutritionAppContext context)
        {
            basket = basketService;
            _context = context;
        }
        public IActionResult Index()
        {
            return View(new BasketIndexViewModel
            {
                Basket = basket
            });
        }

        [HttpPost]
        public IActionResult AddToBasket(Product product, int qunatity)
        {


            Product productObj = _context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);

            if (productObj == null)
            {
                return NotFound();
            }

            if (productObj != null)
            {
               
                basket.AddProduct(productObj, qunatity);
            }
            return RedirectToAction("Create", "Meals");
        }

        public IActionResult AddToBasketEditMeal (Product product, int qunatity, int MealId)
        {


            Product productObj = _context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
            var ingre = new Ingredient();
            ingre = _context.Ingredients.Where(i => i.ProductId == product.ProductId && i.MealId == MealId).FirstOrDefault();
            decimal qunatityFromOld = 0;
           

            if (productObj == null)
            {
                return NotFound();
            }

            if (ingre != null)
            {
                qunatityFromOld = ingre.Quantity;
                _context.Ingredients.Remove(ingre);
                _context.SaveChanges();

            }
                if (productObj != null)
                {
                    var resultOfQunatities = qunatity + qunatityFromOld;
                    basket.AddProduct(productObj, (int)resultOfQunatities);
                }
            
            return RedirectToAction("Edit", "Meals", new { id = MealId });
        }

        [HttpPost]
        public IActionResult RemoveFromBasket(int productId)
        {

            Product productObj = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (productObj != null)
            {
                basket.RemoveLine(productObj);
            }

            return RedirectToAction("Create", "Meals");
        }

        [HttpPost]
        public IActionResult RemoveFromBasketEditMeal(int productId,int MealId)
        {

            Product productObj = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (productObj != null)
            {
                basket.RemoveLine(productObj);
            }

            return RedirectToAction("Edit", "Meals", new { id = MealId });
        }
    }
}
