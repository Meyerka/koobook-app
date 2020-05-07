using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KooBooKMVC.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public int Calories { get; set; }
        public int Proteins { get; set; }
        public int Carbohydrates { get; set; }
        public int Fat { get; set; }

        public List<RecipeComponent> RecipeComponents { get; set; }
    }
}