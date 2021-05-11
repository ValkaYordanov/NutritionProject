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
            return View();
        }

        [HttpPost]
        public IActionResult AddToBasket(int productId, int qunatity)
        {
            
           
            var product = _context.Products.Where(p => p.ProductId == productId);
            if (product == null)
            {
                return NotFound();
            }

            if (product != null)
            {
               
                basket.AddProduct(productId, qunatity);
            }
            return RedirectToAction("Create", "Meals");
        }

        [HttpPost]
        public IActionResult RemoveFromBasket(int productID)
        {
           
                basket.RemoveLine(productID);
            
            return RedirectToAction("Create", "Meals");
        }
    }
}
