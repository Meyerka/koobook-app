using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ImageProcessor;
using ImageProcessor.Imaging;
using KooBooKMVC.Extensions;
using KooBooKMVC.Models;
using KooBooKMVC.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;
using static System.Net.Mime.MediaTypeNames;

namespace KooBooKMVC.Controllers
{
    [Area("Common")]
    public class RecipesController : Controller
    {
        private readonly IRecipeData _recipeData;
        private readonly IHtmlHelper _htmlHelper;
        private readonly IRecipeComponentData _recipeComponentData;
        private readonly IIngredientData _ingredientData;
        private readonly IUserRecipeData _userRecipeData;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RecipesController(   IHtmlHelper htmlHelper, 
                                    IRecipeData recipeData, 
                                    IRecipeComponentData recipeComponentData, 
                                    IIngredientData ingredientData, 
                                    IUserRecipeData userRecipeData,
                                    IWebHostEnvironment webHostEnvironment)
        {
            _htmlHelper = htmlHelper;
            _recipeData = recipeData;
            _recipeComponentData = recipeComponentData;
            _ingredientData = ingredientData;
            _userRecipeData = userRecipeData;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index(int? page, string sortBy, string searchTerm, string searchFilter)
        {

            
            var recipes = _recipeData.GetRecipeBy(searchFilter, searchTerm).ToList();


            if (!page.HasValue)
            {
                page = 1;
            }
            
            
            if (string.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = "name";
            }

            var viewModel = new RecipeListViewModel
            {
                Recipes = recipes,
                Page = page.Value,
                SortBy = sortBy

            };

            return View(viewModel);
        }


        public IActionResult Detail(int recipeId)
        {
            var recipe = _recipeData.GetById(recipeId);


            int divider = recipe.GetTotalNutrient("fat") + recipe.GetTotalNutrient("carbs") + recipe.GetTotalNutrient("proteins");

            var viewModel = new RecipeViewModel(_htmlHelper) { Recipe = recipe };

            if (divider != 0)
            {
                viewModel.ProteinRatio =100* recipe.GetTotalNutrient("proteins") / divider;
                viewModel.FatRatio =  100*recipe.GetTotalNutrient("fat") / divider;
                viewModel.CarbRatio = 100*recipe.GetTotalNutrient("carbs") / divider;
                
            }

            
            return View(viewModel);
        }

        public IActionResult Edit(int? recipeId)
        {
            var recipe = new Recipe();
            if (recipeId.HasValue)
            {
                recipe = _recipeData.GetById(recipeId.Value);
            }

            return View(recipe);
        }

        public IActionResult Delete(int recipeId)
        {
            var recipe = _recipeData.GetById(recipeId);
            var viewModel = new RecipeViewModel(_htmlHelper) { Recipe = recipe };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(Recipe recipe)
        {
            Ingredient tempIngredient;

            foreach (var component in recipe.RecipeComponents)
            {
                tempIngredient = _ingredientData.GetIngredientByExactName(component.Ingredient.Name);
                if (tempIngredient != null)
                {
                    component.Ingredient = tempIngredient;
                    component.IngredientId = tempIngredient.Id;
                }

                if (component.Id > 0)
                {
                    _recipeComponentData.Update(component);
                }
                else
                {
                    _recipeComponentData.Add(component);
                }
            }

          
            if (!ModelState.IsValid)
            {
                var viewModel = new RecipeViewModel(_htmlHelper) {Recipe = recipe };
                return View(recipe);
            }
            string webRootPath = _webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if (recipe.Id > 0)
            {
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\recipes");
                    var extension_new = Path.GetExtension(files[0].FileName);

                    var imagePath = Path.Combine(webRootPath, recipe.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                        
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension_new), FileMode.Create))
                    {

                        var imageFactory = new ImageFactory(true);
                        imageFactory.Load(files[0].OpenReadStream()).Resize(
                            new ResizeLayer(new Size(720, 480), ResizeMode.Max)).Save(fileStreams);

                        //files[0].CopyTo(fileStreams);
                    }
                    recipe.ImageUrl = @"\images\recipes\" + fileName + extension_new;

                }


                _recipeData.Update(recipe);
                
            }
            else
            {
                if (files.Count > 0)
                {


                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\recipes");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        var imageFactory = new ImageFactory(true);
                        imageFactory.Load(files[0].OpenReadStream()).Resize(
                            new ResizeLayer(new Size(1080, 720), ResizeMode.Max)).Save(fileStreams);


                        //files[0].CopyTo(fileStreams);
                    }
                    recipe.ImageUrl = @"\images\recipes\" + fileName + extension;
                }
                recipe.CreationDate = DateTime.Now;
                _recipeData.Add(recipe);
            }
            _recipeData.Commit();


            return RedirectToAction("Detail",new { recipeId = recipe.Id });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddRecipeComponent([Bind("Id, Name,Type,Instructions,RecipeComponents")] Recipe recipe)
        {
            
            
            recipe.RecipeComponents.Add(new RecipeComponent());
            return PartialView("RecipeComponent", recipe);
        }

        public IActionResult AddToGroceryList(int ingredientId)
        {
            List<int> sessionList = new List<int>();
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("GroceryList")))
            {
                sessionList.Add(ingredientId);
                HttpContext.Session.SetObject("GroceryList", sessionList);
            }
            else
            {
                sessionList = HttpContext.Session.GetObject<List<int>>("GroceryList");
                if (!sessionList.Contains(ingredientId))
                {
                    sessionList.Add(ingredientId);
                    HttpContext.Session.SetObject("GroceryList", sessionList);
                }
            }
            return RedirectToAction(nameof(Index));
        }


        public IActionResult AddToFavourites(int recipeId)
        {
            var userRecipe = new UserRecipe
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
,
                RecipeId = recipeId
            };
            _userRecipeData.Add(userRecipe);
            _userRecipeData.Commit();
            return RedirectToAction(nameof(Index));
        }

    }
}