using Microsoft.AspNetCore.Mvc;
using NutritionApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutritionApp.Components
{
    public class MealEditViewComponent : ViewComponent
    {

        private Basket basket;
      
        public MealEditViewComponent(Basket basketService)
        {
            basket = basketService;
           
        }
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
