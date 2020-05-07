using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
namespace KooBooKMVC.Models.Api
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            this.CreateMap<Recipe, ApiRecipe>()
                .ReverseMap();
        }

    }
}
