using Microsoft.AspNetCore.Mvc;
using NutritionApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutritionApp.Controllers
{
    public class OverviewController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        public OverviewController(UserManager<AppUser> userMgr)
        {
            _userManager = userMgr;
        }

        private Task<AppUser> CurrentUser => _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

        public IActionResult Index()
        {
            return View();
        }
    }
}
