using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KooBooKMVC.Models
{
    public interface IUserRecipeData
    {
        UserRecipe Add(UserRecipe userRecipe);
        UserRecipe Delete(int id);

        IEnumerable<UserRecipe> GetUserRecipesById(string id);
        UserRecipe GetUserRecipeById(int id);

        int Commit();
    }
}
