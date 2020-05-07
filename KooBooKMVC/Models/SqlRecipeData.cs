using KooBooKMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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