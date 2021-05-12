using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutritionApp.Models.ViewModels
{
    public class IntakeViewModel : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
