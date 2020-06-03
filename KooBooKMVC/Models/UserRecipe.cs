using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KooBooKMVC.Models
{
    public class UserRecipe
    {
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
