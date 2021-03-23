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
    public class RecipeRatingController : ControllerBase
    {
        private readonly RecipEaseContext _context;

        public RecipeRatingController(RecipEaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create a recipe rating.
        /// </summary>
        /// <remarks>
        ///
        /// Adds the given recipe rating to the database, and returns it on
        /// success.
        ///
        /// The customer specified by `userId` must be the authenticated user
        /// making this request. The recipe specified by `recipeId` must exist
        /// in the database.
        ///
        /// This endpoint interacts with the `reciperating`, `recipe`, and
        /// `customer` tables. The `UserId` attribute on the `customer` table
        /// will be checked against the `userId`. The `Id` attribute on the
        /// `recipe` table will be checked against the `recipeId`.
        ///
        /// The endpoint will perform an `insert` command on the `reciperating`
        /// table to add the recipe rating, and the foreign key constraints will
        /// be relied upon to validate the `recipeId` and `userId`.
        ///
        /// </remarks>
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<ApiRecipeRating>> PostRecipeRating(ApiRecipeRating apiRecipeRating)
        {
            _context.ApiRecipeRating.Add(apiRecipeRating);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ApiRecipeRatingExists(apiRecipeRating.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetApiRecipeRating", new { id = apiRecipeRating.UserId }, apiRecipeRating);
        }

        private bool ApiRecipeRatingExists(string id)
        {
            return _context.ApiRecipeRating.Any(e => e.UserId == id);
        }
    }
}
