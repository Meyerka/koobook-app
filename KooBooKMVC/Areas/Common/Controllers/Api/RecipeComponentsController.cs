using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KooBooKMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KooBooKMVC.Areas.Common.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeComponentsController : ControllerBase
    {
        private readonly IRecipeData _recipeData;
        private readonly IRecipeComponentData _recipeComponentData;
        public RecipeComponentsController(IRecipeData recipeData, IRecipeComponentData recipeComponentData)
        {
            _recipeData = recipeData;
            _recipeComponentData = recipeComponentData;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var componentToDelete = _recipeComponentData.GetById(id);


            if (componentToDelete == null)
            {
                return NotFound();
            }
            _recipeComponentData.Delete(id);
            _recipeComponentData.Commit();
            return Ok();
        }
    }
}