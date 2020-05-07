using KooBooKMVC.Models;
using System.Collections.Generic;

namespace KooBooKMVC
{
    public interface IIngredientData
    {
        IEnumerable<Ingredient> GetIngredientByName(string name);
        Ingredient GetIngredientByExactName(string name);
        Ingredient GetById(int id);
        Ingredient Update(Ingredient updatedIngredient);
        Ingredient Add(Ingredient newIngredient);
        Ingredient Delete(int id);
        int Commit();
    }
}