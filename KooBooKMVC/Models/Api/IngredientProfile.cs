using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KooBooKMVC.Models.Api
{
    public class IngredientProfile: Profile
    {
        public IngredientProfile()
        {
            this.CreateMap<Ingredient, ApiIngredient>()
    .ReverseMap();
        }
    }
}
