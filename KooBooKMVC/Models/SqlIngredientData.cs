using KooBooKMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KooBooKMVC
{
    public class SqlIngredientData : IIngredientData
    {
        private readonly KoobookDbContext _db;

        public SqlIngredientData(KoobookDbContext db)
        {
            _db = db;
        }
        public Ingredient Add(Ingredient newIngredient)
        {
            _db.Add(newIngredient);
            return newIngredient;
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }

        public Ingredient Delete(int id)
        {
            var ingredient = GetById(id);
            if (ingredient != null)
            {
                _db.Remove(ingredient);
            }
            return ingredient;
        }

        public Ingredient GetById(int id)
        {
            return _db.Ingredients.Find(id);
        }

        public Ingredient GetIngredientByExactName(string name)
        {
            var query = from r in _db.Ingredients
                        where r.Name.Equals(name)
                        select r;

            return query.FirstOrDefault();
        }

        public IEnumerable<Ingredient> GetIngredientByName(string name)
        {
            return _db.Ingredients.Where(i => i.Name == name || String.IsNullOrEmpty(name)).OrderBy(i => i.Name);
        }

        public Ingredient Update(Ingredient updatedIngredient)
        {
            var entity = _db.Ingredients.Attach(updatedIngredient);
            entity.State = EntityState.Modified;
            return updatedIngredient;
        }
    }
}