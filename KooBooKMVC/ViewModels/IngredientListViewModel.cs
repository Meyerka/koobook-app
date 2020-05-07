using KooBooKMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KooBooKMVC.ViewModels
{
    public class IngredientListViewModel
    {
        public List<Ingredient> Ingredients{ get; set; }
        public int Page { get; set; }
        public string SortBy { get; set; }

    }
}
