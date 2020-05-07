using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KooBooKMVC.Models.Api
{
    public class ApiRecipeComponent
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Measurement Unit { get; set; }
        public string IngredientName { get; set; }


    }
}
