using KooBooKMVC.Models;
using System.Collections.Generic;

namespace KooBooKMVC
{
    public interface IRecipeData
    {
        IEnumerable<Recipe> GetRecipeByName(string name);
        Recipe GetById(int id);
        Recipe GetRecentRecipe();
        Recipe Update(Recipe updatedRecipe);
        Recipe Add(Recipe newRecipe);
        Recipe Delete(int id);
        int Commit();
        Recipe GetRandom(Recipe.MealType? entrée);
    }
}