using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KooBooKMVC.Models
{
    public class SqlUserRecipeData : IUserRecipeData
    {
        private readonly KoobookDbContext _db;

        public SqlUserRecipeData(KoobookDbContext db)
        {
            _db = db;
        }
        public UserRecipe Add(UserRecipe userRecipe)
        {
            _db.UserRecipe.Add(userRecipe);
            return userRecipe;
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }

        public UserRecipe Delete(int id)
        {
            var userRecipe = _db.UserRecipe.Find(id);

            if (userRecipe != null)
            {
                _db.UserRecipe.Remove(userRecipe);
            }
            return userRecipe;
        }

        public UserRecipe GetUserRecipeById(int id)
        {
            return _db.UserRecipe.Find(id);
        }

        public IEnumerable<UserRecipe> GetUserRecipesById(string id)
        {
            return _db.UserRecipe.Where(ur => ur.UserId == id);
        }
    }
}
