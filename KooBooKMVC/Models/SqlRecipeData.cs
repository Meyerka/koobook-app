using KooBooKMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using static KooBooKMVC.Models.Recipe;

namespace KooBooKMVC
{
    public class SqlRecipeData : IRecipeData
    {
        private readonly KoobookDbContext _db;
        public SqlRecipeData(KoobookDbContext db)
        {
            _db = db;
        }
        public Recipe Add(Recipe newRecipe)
        {
            _db.Add(newRecipe);
            foreach (RecipeComponent component in newRecipe.RecipeComponents)
            {
                if (component.Id > 0)
                {
                    _db.RecipeComponents.Update(component);

                }
                else
                {
                    _db.RecipeComponents.Add(component);
                }
            }
            _db.SaveChanges();
            return newRecipe;
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }

        public Recipe Delete(int id)
        {
            var recipe = GetById(id);
            if (recipe != null)
            {
                _db.Recipes.Remove(recipe);
            }
            return recipe;
        }

        public Recipe GetById(int id)
        {
            return _db.Recipes.Include(r => r.RecipeComponents).ThenInclude(rc => rc.Ingredient).SingleOrDefault(r => r.Id == id);
        }

        public Recipe GetRandom(MealType? mealType)
        {
            if (mealType == null)
            {
                return _db.Recipes.OrderBy(t => Guid.NewGuid())
                                    .FirstOrDefault();
            }
            return _db.Recipes.OrderBy(t => Guid.NewGuid())
                                    .FirstOrDefault(r => r.Type == mealType);
            

        }

        public Recipe GetRecentRecipe()
        {
            return _db.Recipes.Include(r => r.RecipeComponents).ThenInclude(rc => rc.Ingredient).OrderByDescending(r => r.CreationDate).FirstOrDefault();
        }

        public IEnumerable<Recipe> GetRecipeBy(string filter, string term)
        {

            switch (filter)
            {
                case "all":
                    return _db.Recipes.Include(r => r.RecipeComponents)
                                .ThenInclude(rc => rc.Ingredient)
                                .Where(r => r.Name.Contains(term) ||
                                        string.IsNullOrEmpty(term) ||
                                         r.RecipeComponents.All(rc => rc.Ingredient.Name.Contains(term)))
                                .OrderBy(r => r.Name);

                case "ingredients":
                    return _db.Recipes.Include(r => r.RecipeComponents)
                                .ThenInclude(rc => rc.Ingredient)
                                .Where(r => r.RecipeComponents.All(rc => rc.Ingredient.Name.Contains(term)))
                                .OrderBy(r => r.Name);

                case "description":
                    return null;

                default:
                    return _db.Recipes.Include(r => r.RecipeComponents)
                                .ThenInclude(rc => rc.Ingredient)
                                .Where(r => r.Name.Contains(term) ||
                                        string.IsNullOrEmpty(term) ||
                                         r.RecipeComponents.All(rc => rc.Ingredient.Name.Contains(term)))
                                .OrderBy(r => r.Name);
            }
            
        }

        public IEnumerable<Recipe> GetRecipeByName(string name)
        {

            return _db.Recipes.Include(r => r.RecipeComponents)
                                .ThenInclude(rc => rc.Ingredient)
                                .Where(r => r.Name.Contains(name) || string.IsNullOrEmpty(name))
                                .OrderBy(r => r.Name) ;
        }

        public Recipe Update(Recipe updatedRecipe)
        {
            var entity = _db.Recipes.Attach(updatedRecipe);
            entity.State = EntityState.Modified;


            foreach (RecipeComponent component in updatedRecipe.RecipeComponents)
            {
                if (_db.RecipeComponents.Find(component.Id) == null)
                {
                    _db.RecipeComponents.Add(component);
                }
                else
                {
                    _db.RecipeComponents.Update(component);
                }
            }
            return updatedRecipe;
        }
    }
}