using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
namespace KooBooKMVC.Models.Api
{
    public class RecipeComponentProfile : Profile
    {
        public RecipeComponentProfile()
        {
            this.CreateMap<RecipeComponent, ApiRecipeComponent>()
                .ReverseMap();
        }

    }
}
