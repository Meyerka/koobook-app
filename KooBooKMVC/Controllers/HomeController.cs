using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KooBooKMVC.Models;

namespace KooBooKMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRecipeData _recipeData;

        public HomeController(ILogger<HomeController> logger, IRecipeData recipeData)
        {
            _logger = logger;
            _recipeData = recipeData;
        }

        public IActionResult Index()
        {
            var recipe = _recipeData.GetRecentRecipe();
            return View(recipe);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
