using KooBooKMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KooBooKMVC.ViewModels
{
    public class GroceryListViewModel
    {
        public IList<Ingredient> IngredientList { get; set; }
    }
}
