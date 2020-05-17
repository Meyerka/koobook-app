using KooBooKMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KooBooKMVC.ViewModels
{
    public class HomeViewModel
    {
        public Recipe RecentRecipe { get; set; }
        public Recipe RandomStarter{ get; set; }
        public Recipe RandomMainCourse{ get; set; }
        public Recipe RandomDessert{ get; set; }
    }
}
