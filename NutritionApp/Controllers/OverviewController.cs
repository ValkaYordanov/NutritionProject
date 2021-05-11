using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutritionApp.Controllers
{
    public class OverviewController : Controller
    {
        /*
        public IActionResult Index()
        {
            return View();
        }
        */
        public ActionResult Index(string id)
        {
            if (id != null)
            {
                // Lookup topic from id
                ViewBag.Msg = "You're logged in: Your ID is " + id;
                ViewBag.Id = id;
            } else
            {
                ViewBag.Msg = "You're NOT logged in";
            }

            
            return View();
        }
    }
}
