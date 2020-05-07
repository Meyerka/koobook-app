using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KooBooKMVC.Models
{
    public class Recipe
    {
        public Recipe()
        {
            RecipeComponents = new List<RecipeComponent>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public String Name { get; set; }
        [Required]
        public MealType Type { get; set; }
        public DateTime CreationDate { get; set; }
        [Range(0,5)]
        public int Difficulty { get; set; }
        public int Servings { get; set; }
        public int PrepTime { get; set; }

        [Required]
        [StringLength(2000)]
        public string Instructions { get; set; }

        public List<RecipeComponent> RecipeComponents { get; set; }


        public enum MealType
        {
            Entrée,
            Plat,
            Dessert,
            Apéritif,
            Cocktail
        }

    }
}
