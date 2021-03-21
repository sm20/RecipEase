using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipEase.Server.Data;
using RecipEase.Shared.Models.Api;

namespace RecipEase.Server.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly RecipEaseContext _context;

        public RecipeController(RecipEaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Documentation
        /// </summary>
        /// <remarks>
        ///
        /// More documentation.
        ///
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiRecipe>> GetRecipe(int id)
        {
            var apiRecipe = await _context.ApiRecipe.FindAsync(id);

            if (apiRecipe == null)
            {
                return NotFound();
            }

            return apiRecipe;
        }

        /// <summary>
        /// Get a list of recipes.
        /// </summary>
        /// <remarks>
        ///
        /// Returns a list of recipes from the database, optionally filtered by
        /// title or category.
        ///
        /// This endpoint interacts with all attributes in the `recipe` and
        /// `recipeincategory` tables.
        ///
        /// The endpoint performs a `select *` query, with a `where` clause
        /// included when necessary to apply the specified filters. The `recipe`
        /// table is optionally joined with `recipeincategory` to filter by
        /// category.
        ///
        /// </remarks>
        /// <param name="titleMatch">String to filter recipes by title. If
        /// provided, only recipes with the filter string in their title (case
        /// insensitive) will be returned.</param>
        /// <param name="categoryName">String to filter recipes by category. If
        /// provided, only recipes in the given category will be returned. If
        /// there is no category with the given name in the database, no recipes
        /// will be returned.</param>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiRecipe>>> GetRecipes(string titleMatch, string categoryName)
        {
            return await _context.ApiRecipe.ToListAsync();
        }

        /// <summary>
        /// Documentation
        /// </summary>
        /// <remarks>
        ///
        /// More documentation.
        ///
        /// </remarks>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutRecipe(int id, ApiRecipe apiRecipe)
        {
            if (id != apiRecipe.Id)
            {
                return BadRequest();
            }

            _context.Entry(apiRecipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Documentation
        /// </summary>
        /// <remarks>
        ///
        /// More documentation.
        ///
        /// </remarks>
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<ApiRecipe>> PostRecipe(ApiRecipe apiRecipe)
        {
            _context.ApiRecipe.Add(apiRecipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApiRecipe", new { id = apiRecipe.Id }, apiRecipe);
        }

        /// <summary>
        /// Documentation
        /// </summary>
        /// <remarks>
        ///
        /// More documentation.
        ///
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var apiRecipe = await _context.ApiRecipe.FindAsync(id);
            if (apiRecipe == null)
            {
                return NotFound();
            }

            _context.ApiRecipe.Remove(apiRecipe);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool RecipeExists(int id)
        {
            return _context.ApiRecipe.Any(e => e.Id == id);
        }
    }
}
