using KooBooKMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using static KooBooKMVC.Models.Recipe;

namespace KooBooKMVC.ViewModels
{
    public class RecipeViewModel
    {
        private readonly IHtmlHelper _htmlHelper;
        public Recipe Recipe { get; set; }
        public IEnumerable<SelectListItem> MealTypes { get; set; }


        public int ProteinRatio { get; set; }
        public int FatRatio { get; set; }
        public int CarbRatio { get; set; }

        public List<RecipeComponent> RecipeComponents { get; set; }
        public RecipeViewModel(IHtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
            MealTypes = _htmlHelper.GetEnumSelectList<MealType>();

            int divider = Recipe.GetTotalNutrient("fat") + Recipe.GetTotalNutrient("carbs") + Recipe.GetTotalNutrient("proteins");
            if (divider != 0) 
            {
                ProteinRatio = Recipe.GetTotalNutrient("proteins") / divider;
                ProteinRatio = Recipe.GetTotalNutrient("proteins") / divider;
            }
        }
    }
}