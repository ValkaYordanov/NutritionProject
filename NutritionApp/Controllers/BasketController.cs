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
    }
}
