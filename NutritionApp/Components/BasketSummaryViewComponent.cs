using Microsoft.AspNetCore.Mvc;
using NutritionApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutritionApp.Components
{
    public class BasketSummaryViewComponent : ViewComponent
    {
       
            private Basket basket;
            public BasketSummaryViewComponent(Basket basketService)
            {
            basket = basketService;
            }
            public IViewComponentResult Invoke()
            {
                return View(basket);
            }
        
    }
}
