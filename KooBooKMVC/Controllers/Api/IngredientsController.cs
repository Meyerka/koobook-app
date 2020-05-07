using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KooBooKMVC.Models;
using KooBooKMVC.Models.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace KooBooKMVC.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IIngredientData _ingredientData;
        private readonly LinkGenerator _linkGenerator;
        public IngredientsController(IMapper mapper, IIngredientData ingredientData, LinkGenerator linkGenerator)
        {
            _mapper = mapper;
            _ingredientData = ingredientData;
            _linkGenerator = linkGenerator;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            var ingredientList = _ingredientData.GetIngredientByName("");
            var result = _mapper.Map<ApiIngredient[]>(ingredientList);
            return Ok(result);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var ingredient = _ingredientData.GetById(id);
            var result = _mapper.Map<ApiIngredient>(ingredient);
            return Ok(result);
        }

        // GET api/<controller>/5
        [HttpGet("search")]
        public IActionResult Get(string name)
        {
            var ingredients = _ingredientData.GetIngredientByName(name);
            var result = _mapper.Map<ApiIngredient[]>(ingredients);
            return Ok(result);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Ingredient input)
        {
            var location = _linkGenerator.GetPathByAction("Get", "Ingredients");

            var ingredient = _mapper.Map<Ingredient>(input);
            _ingredientData.Add(ingredient);
            _ingredientData.Commit();
            return Created(location, _mapper.Map<ApiIngredient>(ingredient));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Ingredient input)
        {
            var oldIngredient = _ingredientData.GetById(input.Id);
            if (oldIngredient == null)
            {
                return NotFound();
            }
            _ingredientData.Update(input);
            _ingredientData.Commit();
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var oldIngredient = _ingredientData.GetById(id);
            if (oldIngredient == null)
            {
                return NotFound();
            }
            _ingredientData.Delete(id);
            _ingredientData.Commit();
            return Ok();
        }
    }
}