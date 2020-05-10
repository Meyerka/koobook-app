using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KooBooKMVC.Extensions;
using KooBooKMVC.Models;
using KooBooKMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KooBooKMVC.Areas.Admin
{
    [Area("Admin")]
    public class IngredientsController : Controller
    {
        private readonly IIngredientData _ingredientData;
        public IngredientsController(IIngredientData ingredientData)
        {
            _ingredientData = ingredientData;
        }
        public IActionResult Index()
        {
            var ingredientList = _ingredientData.GetIngredientByName("").ToList();
            var viewModel = new IngredientListViewModel
            {
                Ingredients = ingredientList
            };
            
            
            return View(viewModel);
        }

        public IActionResult Detail(int ingredientId)
        {
            var ingredient = _ingredientData.GetById(ingredientId);
            var viewModel = new IngredientViewModel { Ingredient = ingredient };
            return View(viewModel);
        }

        public IActionResult Edit(int? ingredientId)
        {
            var ingredient = new Ingredient();
            if (ingredientId.HasValue)
            {
                ingredient = _ingredientData.GetById(ingredientId.Value);
            }

            var viewModel = new IngredientViewModel { Ingredient = ingredient };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Edit(Ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new IngredientViewModel { Ingredient = ingredient };
                return View(viewModel);
            }

            if (ingredient.Id > 0)
            {
                _ingredientData.Update(ingredient);
            }
            else
            {
                _ingredientData.Add(ingredient);
            }
            _ingredientData.Commit();
            return RedirectToAction("Detail", new { Area = "Common", ingredientId = ingredient.Id });
        }

        public IActionResult Delete(int ingredientId)
        {
            var ingredient = _ingredientData.GetById(ingredientId);
            var viewModel = new IngredientViewModel { Ingredient = ingredient };
            return View(viewModel);
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
    }
}