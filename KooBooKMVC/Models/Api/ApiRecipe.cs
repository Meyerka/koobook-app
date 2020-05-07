using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static KooBooKMVC.Models.Recipe;

namespace KooBooKMVC.Models.Api
{
    public class ApiRecipe
    {

        public int Id { get; set; }
        [Required]
        public String Name { get; set; }

        public MealType Type { get; set; }
        [Required]
        public string Instructions { get; set; }

        public ICollection<ApiRecipeComponent> RecipeComponents { get; set; }


    }
}
