using KooBooKMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace KooBooKMVC
{
    public class SqlRecipeComponentData : IRecipeComponentData
    {
        private readonly KoobookDbContext _db;

        public SqlRecipeComponentData(KoobookDbContext db)
        {
            _db = db;
        }
        public RecipeComponent Add(RecipeComponent recipeComponent)
        {
            _db.Add(recipeComponent);
            return recipeComponent;
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }

        public RecipeComponent Delete(int recipeComponentId)
        {
            var component = GetById(recipeComponentId);
            if (component != null)
            {
                _db.Remove(component);
            }

            return component;
        }

        public RecipeComponent GetById(int id)
        {
            return _db.RecipeComponents.Include(rc => rc.Ingredient).FirstOrDefault(rc => rc.Id == id);
        }

        public IEnumerable<RecipeComponent> GetByRecipeId(int id)
        {
            return _db.RecipeComponents.Where(r => r.RecipeId == id).ToList();
        }

        public RecipeComponent Update(RecipeComponent updatedRecipeComponent)
        {
            var entity = _db.RecipeComponents.Attach(updatedRecipeComponent);
            entity.State = EntityState.Modified;
            return updatedRecipeComponent;
        }
    }
}