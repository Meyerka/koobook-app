using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KooBooKMVC.Models;
using KooBooKMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KooBooKMVC.Controllers
{
    public class RecipeComponentController : Controller
    {
        private readonly IRecipeComponentData _recipeComponentData;

        public RecipeComponentController(IRecipeComponentData recipeComponentData)
        {
            _recipeComponentData = recipeComponentData;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit(int? recipeComponentId)
        {
            var recipeComponent = new RecipeComponent();
            if (recipeComponentId.HasValue)
            {
                recipeComponent = _recipeComponentData.GetById(recipeComponentId.Value);
            }
            
            var viewModel = new RecipeComponentViewModel { RecipeComponent = recipeComponent};
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Edit(RecipeComponent recipeComponent)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new RecipeComponentViewModel { RecipeComponent = recipeComponent};
                return View(viewModel);
            }

            if (recipeComponent.Id > 0)
            {
                _recipeComponentData.Update(recipeComponent);
            }
            else
            {
                _recipeComponentData.Add(recipeComponent);
            }
            _recipeComponentData.Commit();
            return RedirectToAction("Detail", new { recipeComponentId= recipeComponent.Id });
        }

        public IActionResult Delete(int recipeComponentId)
        {
            var recipeComponent = _recipeComponentData.GetById(recipeComponentId);
            var viewModel = new RecipeComponentViewModel{ RecipeComponent= recipeComponent };
            return View(viewModel);
        }
    }
}