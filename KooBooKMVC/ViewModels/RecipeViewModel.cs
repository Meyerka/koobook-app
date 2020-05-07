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
        
        public List<RecipeComponent> RecipeComponents { get; set; }
        public RecipeViewModel(IHtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
            MealTypes = _htmlHelper.GetEnumSelectList<MealType>();
        }
    }
}