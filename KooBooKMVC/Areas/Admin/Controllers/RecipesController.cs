using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageProcessor;
using ImageProcessor.Imaging;
using KooBooKMVC.Models;
using KooBooKMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KooBooKMVC.Areas.Admin
{
    [Authorize(Roles = Utility.Admin)]
    [Area("Admin")]
    public class RecipesController : Controller
    {
        private readonly IRecipeData _recipeData;
        private readonly IHtmlHelper _htmlHelper;
        private readonly IRecipeComponentData _recipeComponentData;
        private readonly IIngredientData _ingredientData;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public RecipesController(IHtmlHelper htmlHelper, IRecipeData recipeData, IRecipeComponentData recipeComponentData, IIngredientData ingredientData, IWebHostEnvironment webHostEnvironment)
        {
            _htmlHelper = htmlHelper;
            _recipeData = recipeData;
            _recipeComponentData = recipeComponentData;
            _ingredientData = ingredientData;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index(int? page, string sortBy)
        {
            var recipes = _recipeData.GetRecipeByName("").ToList();


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
            var viewModel = new RecipeViewModel(_htmlHelper) { Recipe = recipe };
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
                var viewModel = new RecipeViewModel(_htmlHelper) { Recipe = recipe };
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
                    string currentImg = "";
                    if (recipe.ImageUrl != null)
                    {
                        currentImg = recipe.ImageUrl.TrimStart('\\');
                    }

                    var imagePath = Path.Combine(webRootPath, currentImg);
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
                            new ResizeLayer(new Size(720, 480), ResizeMode.Max)).Save(fileStreams);


                        //files[0].CopyTo(fileStreams);
                    }
                    recipe.ImageUrl = @"\images\recipes\" + fileName + extension;
                }
                recipe.CreationDate = DateTime.Now;
                _recipeData.Add(recipe);
            }
            _recipeData.Commit();


            return RedirectToAction("Detail", new { area = "Common", recipeId = recipe.Id });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<ActionResult> AddRecipeComponent([Bind("Id, Name,Type,Instructions,RecipeComponents")] Recipe recipe)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {


            recipe.RecipeComponents.Add(new RecipeComponent());
            //_recipeData.Update(recipe);
            //_recipeData.Commit();
            return PartialView("RecipeComponent", recipe);
        }
    }
}