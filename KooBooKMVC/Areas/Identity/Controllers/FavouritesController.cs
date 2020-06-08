using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KooBooKMVC.Models;
using Microsoft.AspNetCore.Mvc;



namespace KooBooKMVC.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class FavouritesController : Controller
    {
        private readonly IUserRecipeData _userRecipeData;
        public FavouritesController(IUserRecipeData userRecipeData)
        {
            _userRecipeData = userRecipeData;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var favouriteRecipes = _userRecipeData.GetUserRecipesById(userId).ToList();
            return View(favouriteRecipes);
        }
    }
}
