﻿using KooBooKMVC.Models;
using System.Collections.Generic;

namespace KooBooKMVC
{
    public interface IRecipeData
    {
        IEnumerable<Recipe> GetRecipeByName(string name);
        Recipe GetById(int id);
        Recipe Update(Recipe updatedRecipe);
        Recipe Add(Recipe newRecipe);
        Recipe Delete(int id);
        int Commit();
    }
}