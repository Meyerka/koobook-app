using KooBooKMVC.Models;
using System.Collections.Generic;

namespace KooBooKMVC
{
    public interface IRecipeComponentData
    {

        RecipeComponent Add(RecipeComponent recipeComponent);

        RecipeComponent Delete(int recipeComponentId);

        RecipeComponent Update(RecipeComponent recipeComponent);
        RecipeComponent GetById(int id);

        IEnumerable<RecipeComponent> GetByRecipeId(int id);

        int Commit();
    }
}