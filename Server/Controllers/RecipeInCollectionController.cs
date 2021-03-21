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
    public class RecipeInCollectionController : ControllerBase
    {
        private readonly RecipEaseContext _context;

        public RecipeInCollectionController(RecipEaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of all recipes in a given recipe collection.
        /// </summary>
        /// <remarks>
        ///
        /// Returns a list of recipes from the database which are in the given
        /// recipe collection.
        ///
        /// This endpoint interacts with all attributes in the
        /// `recipeincollection` table.
        ///
        /// The endpoint performs a `select *` query, with a `where` clause
        /// included to filter for the given recipe collection.
        ///
        /// </remarks>
        [HttpGet]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<ApiRecipeInCollection>>> GetRecipeInCollection(ApiRecipeCollection collection)
        {
            return await _context.ApiRecipeInCollection.ToListAsync();
        }

        // POST: api/RecipeInCollection
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiRecipeInCollection>> PostRecipeInCollection(ApiRecipeInCollection apiRecipeInCollection)
        {
            _context.ApiRecipeInCollection.Add(apiRecipeInCollection);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RecipeInCollectionExists(apiRecipeInCollection.RecipeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetApiRecipeInCollection", new { id = apiRecipeInCollection.RecipeId }, apiRecipeInCollection);
        }

        // DELETE: api/RecipeInCollection/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipeInCollection(int id)
        {
            var apiRecipeInCollection = await _context.ApiRecipeInCollection.FindAsync(id);
            if (apiRecipeInCollection == null)
            {
                return NotFound();
            }

            _context.ApiRecipeInCollection.Remove(apiRecipeInCollection);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeInCollectionExists(int id)
        {
            return _context.ApiRecipeInCollection.Any(e => e.RecipeId == id);
        }
    }
}
