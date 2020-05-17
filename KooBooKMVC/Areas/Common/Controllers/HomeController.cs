using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KooBooKMVC.Models;
using KooBooKMVC.ViewModels;
using static KooBooKMVC.Models.Recipe;

namespace KooBooKMVC.Controllers
{
    [Area("Common")]
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
            var recentRecipe = _recipeData.GetRecentRecipe();
            var starterRecipe = _recipeData.GetRandom(MealType.Entrée);
            var mainRecipe= _recipeData.GetRandom(MealType.Plat);
            var dessertRecipe = _recipeData.GetRandom(MealType.Dessert);

            return View(new HomeViewModel { RecentRecipe = recentRecipe, RandomStarter = starterRecipe, RandomMainCourse = mainRecipe, RandomDessert = dessertRecipe });
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
