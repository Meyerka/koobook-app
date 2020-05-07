using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KooBooKMVC.Models
{

    public class RecipeComponent
    {
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public Measurement Unit { get; set; }
        public Recipe Recipe { get; set; }
        public int RecipeId { get; set; }
        public Ingredient Ingredient { get; set; }
        public int IngredientId { get; set; }

    }


    
    public enum Measurement
    {
        càs,
        càc,
        g,
        mL
    }
}
