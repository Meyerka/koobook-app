using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public int GetTotalCalories(string nutrient)
        {
            double result = 0;
            int value = 0;


            foreach (var component in RecipeComponents)
            {
                
                    switch (nutrient.ToLower())
                    {
                    case "calories":
                        value = component.Ingredient.Calories;
                        break;
                    case "proteins":
                        value = component.Ingredient.Proteins;
                        break;
                    case "fat":
                        value = component.Ingredient.Fat;
                        break;
                    case "carbs":
                        value = component.Ingredient.Carbohydrates;
                        break;
                    default:
                        value = 0;
                        break;
                    }

                switch (component.Unit)
                {
                    case Measurement.càs:
                        result += 0.05 * component.Quantity * component.Ingredient.Calories;
                        break;
                    case Measurement.càc:
                        result += 0.02 * component.Quantity * component.Ingredient.Calories;
                        break;
                    case Measurement.g:
                        result += 0.01 * component.Quantity * component.Ingredient.Calories;
                        break;
                    case Measurement.mL:
                        result += 0.01 * component.Quantity * component.Ingredient.Calories;
                        break;
                    default:
                        break;
                }
            }
            return (int)result;
        }



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
