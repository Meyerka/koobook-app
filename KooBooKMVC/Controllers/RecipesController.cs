using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using AspNetCore;
using KooBooKMVC.Models;
using KooBooKMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KooBooKMVC.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipeData _recipeData;
        private readonly IHtmlHelper _htmlHelper;
        private readonly IRecipeComponentData _recipeComponentData;
        private readonly IIngredientData _ingredientData;
        public RecipesController(IHtmlHelper htmlHelper, IRecipeData recipeData, IRecipeComponentData recipeComponentData, IIngredientData ingredientData)
        {
            _htmlHelper = htmlHelper;
            _recipeData = recipeData;
            _recipeComponentData = recipeComponentData;
            _ingredientData = ingredientData;
            
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
                var viewModel = new RecipeViewModel(_htmlHelper) {Recipe = recipe };
                return View(recipe);
            }
            if (recipe.Id > 0)
            {
                _recipeData.Update(recipe);
                
            }
            else
            {
                recipe.CreationDate = DateTime.Now;
                _recipeData.Add(recipe);
            }
            _recipeData.Commit();


            return RedirectToAction("Detail",new { recipeId = recipe.Id });
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