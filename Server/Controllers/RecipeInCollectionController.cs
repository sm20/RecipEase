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

        /// <summary>
        /// Add a recipe to a collection.
        /// </summary>
        /// <remarks>
        ///
        /// Adds the given recipe to the given recipe collection in the
        /// database, and returns it on success. If the given recipe or recipe
        /// collection do not exist, an error code is returned.
        ///
        /// The authenticated user making this request must be the owner of the
        /// collection.
        ///
        /// This endpoint interacts with the `recipe`, `customer`,
        /// `recipeincollection`, and `recipecollection` tables. The keys in the
        /// payload will be checked against the rows in `recipe` and
        /// `recipeincollection`.
        ///
        /// The endpoint will perform an `insert` command on the
        /// `recipeincollection` table to add the recipe to the collection, and
        /// foreign key constraints will be relied upon to validate recipe
        /// collection and recipe keys.
        ///
        /// </remarks>
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

        /// <summary>
        /// Remove a recipe from a collection.
        /// </summary>
        /// <remarks>
        ///
        /// Adds the given recipe to the given recipe collection in the
        /// database, and returns it on success. If the given recipe or recipe
        /// collection do not exist, an error code is returned.
        ///
        /// The authenticated user making this request must be the owner of the
        /// collection.
        ///
        /// This endpoint interacts with the `recipe`, `customer`,
        /// `recipeincollection`, and `recipecollection` tables. The keys in the
        /// payload will be checked against the rows in `recipe` and
        /// `recipeincollection`.
        ///
        /// The endpoint will perform a `delete` command on the
        /// `recipeincollection` table to remove the recipe from the collection,
        /// and foreign key constraints will be relied upon to validate recipe
        /// collection and recipe keys.
        ///
        /// </remarks>
        [HttpDelete]
        public async Task<IActionResult> DeleteRecipeInCollection(ApiRecipeInCollection apiRecipeInCollection)
        {
            // var apiRecipeInCollection = await _context.ApiRecipeInCollection.FindAsync(id);
            if (apiRecipeInCollection == null)
            {
                return NotFound();
            }

            _context.ApiRecipeInCollection.Remove(apiRecipeInCollection);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool RecipeInCollectionExists(int id)
        {
            return _context.ApiRecipeInCollection.Any(e => e.RecipeId == id);
        }
    }
}
