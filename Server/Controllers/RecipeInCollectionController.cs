using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipEase.Server.Data;
using RecipEase.Shared.Models;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RecipeInCollectionController(RecipEaseContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
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
        /// <param name="userId">The userId of the customer who owns the collections to be retrieved.</param>
        /// <param name="collectionTitle">The title of the collection whose recipes should be retrieved.</param>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiRecipeInCollection>>> GetRecipeInCollection(string userId,
            string collectionTitle)
        {
            var query = from c in _context.RecipeInCollection
                where c.CollectionTitle == collectionTitle && c.CollectionUserId == userId
                select c;
            return await query.Select(c => c.ToApiRecipeInCollection()).ToListAsync();
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
        public async Task<ActionResult<ApiRecipeInCollection>> PostRecipeInCollection(
            ApiRecipeInCollection apiRecipeInCollection)
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (apiRecipeInCollection.CollectionUserId != currentUserId)
            {
                return Unauthorized();
            }

            var recipeInCollection = new RecipeInCollection()
            {
                CollectionTitle = apiRecipeInCollection.CollectionTitle,
                RecipeId = apiRecipeInCollection.RecipeId,
                CollectionUserId = apiRecipeInCollection.CollectionUserId
            };

            _context.RecipeInCollection.Add(recipeInCollection);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RecipeInCollectionExists(recipeInCollection))
                {
                    return Conflict();
                }

                throw;
            }

            return CreatedAtAction("GetRecipeInCollection", new {id = apiRecipeInCollection.RecipeId},
                apiRecipeInCollection);
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
        /// <param name="recipeId">The recipe to remove from the collection.</param>
        /// <param name="collectionTitle">The collection to delete from.</param>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteRecipeInCollection(int recipeId, string collectionTitle)
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var query = from c in _context.RecipeInCollection
                where c.CollectionUserId == currentUserId && c.CollectionTitle == collectionTitle &&
                      c.RecipeId == recipeId
                select c;
            var recipeInCollection = await query.FirstOrDefaultAsync();
            if (recipeInCollection == null)
            {
                return NotFound();
            }

            _context.RecipeInCollection.Remove(recipeInCollection);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool RecipeInCollectionExists(RecipeInCollection recipeInCollection)
        {
            return _context.RecipeInCollection.Any(e =>
                e.RecipeId == recipeInCollection.RecipeId && e.CollectionTitle == recipeInCollection.CollectionTitle &&
                e.CollectionUserId == recipeInCollection.CollectionUserId);
        }
    }
}