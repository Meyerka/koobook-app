using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KooBooKMVC.Extensions;
using KooBooKMVC.Models;
using KooBooKMVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KooBooKMVC.Areas.Common.Controllers
{
    [Area("Common")]
    public class GroceryListController : Controller
    {
        [BindProperty]
        public GroceryListViewModel GroceryList { get; set; }
        private readonly IIngredientData _ingredientData;

        public GroceryListController(IIngredientData ingredientData)
        {
            _ingredientData = ingredientData;
            GroceryList = new GroceryListViewModel
            {
                IngredientList = new List<Ingredient>()
            };
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetObject<List<int>>("GroceryList") != null)
            {
                List<int> sessionList = new List<int>();
                sessionList = HttpContext.Session.GetObject<List<int>>("GroceryList");
                foreach (var id in sessionList)
                {
                    GroceryList.IngredientList.Add(_ingredientData.GetById(id));
                }
            }
            return View(GroceryList);
        }

        public IActionResult DeleteItem(int ingredientId)
        {
            if (HttpContext.Session.GetObject<List<int>>("GroceryList") != null)
            {
                List<int> sessionList = new List<int>();
                sessionList = HttpContext.Session.GetObject<List<int>>("GroceryList");
                sessionList.Remove(ingredientId);

                HttpContext.Session.SetObject("GroceryList", sessionList);

            }
            return RedirectToAction(nameof(Index));
        }
    }
}