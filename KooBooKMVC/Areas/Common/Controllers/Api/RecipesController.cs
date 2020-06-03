using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KooBooKMVC.Models;
using KooBooKMVC.Models.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KooBooKMVC.Controllers.Api
{   
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : Controller
    {
        private readonly IRecipeData _recipeData;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        public RecipesController(IRecipeData recipeData, IMapper mapper, LinkGenerator linkGenerator)
        {
            _recipeData = recipeData;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }


        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            var recipeList = _recipeData.GetRecipeByName("");
            var result = _mapper.Map<ApiRecipe[]>(recipeList);
            return Ok(result);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var recipe = _recipeData.GetById(id);
            var result = _mapper.Map<ApiRecipe>(recipe);
            return Ok(result);
        }

        // GET api/<controller>/5
        [HttpGet("search")]
        public IActionResult Get(string name)
        {
            var recipes = _recipeData.GetRecipeByName(name);
            var result = _mapper.Map<ApiRecipe[]>(recipes);
            return Ok(result);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Recipe input)
        {
            var location = _linkGenerator.GetPathByAction("Get", "Recipes");

            var recipe = _mapper.Map<Recipe>(input);
            _recipeData.Add(recipe);
            _recipeData.Commit();
            return Created(location, _mapper.Map<ApiRecipe>(recipe));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Recipe input)
        {
            var oldRecipe = _recipeData.GetById(input.Id);
            if (oldRecipe == null)
            {
                return NotFound();
            }
            _recipeData.Update(input);
            _recipeData.Commit();
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var oldRecipe = _recipeData.GetById(id);
            if (oldRecipe == null)
            {
                return NotFound();
            }
            _recipeData.Delete(id);
            _recipeData.Commit();
            return Ok();
        }
    }
}
