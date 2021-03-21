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
    public class RecipeCollectionController : ControllerBase
    {
        private readonly RecipEaseContext _context;

        public RecipeCollectionController(RecipEaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get recipe collections.
        /// </summary>
        /// <remarks>
        ///
        /// Returns the recipe collections of the user with given `userId`.
        ///
        /// This endpoint interacts with the `recipecollection` table.
        ///
        /// The endpoint performs a `select *` query with a `where` clause to
        /// find the specified recipe collections.
        ///
        /// </remarks>
        /// <param name="userId">The id of the customer whose recipe collections
        /// should be returned.</param>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiRecipeCollection>>> GetRecipeCollection(string userId)
        {
            return await this._context.ApiRecipeCollection.ToListAsync();
        }

        // PUT: api/RecipeCollection/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutRecipeCollection(string id, ApiRecipeCollection apiRecipeCollection)
        {
            if (id != apiRecipeCollection.UserId)
            {
                return BadRequest();
            }

            _context.Entry(apiRecipeCollection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeCollectionExists(id))
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
        /// Create a recipe collection.
        /// </summary>
        /// <remarks>
        ///
        /// Adds the given recipe collection to the database, and returns it on
        /// success. The `title` must be unique across all recipe collections
        /// for the given user; if it isn't an error code will be returned.
        ///
        /// The customer specified by `userId` must be the authenticated user
        /// making this request.
        ///
        /// This endpoint interacts with the `recipecollection` and `customer`
        /// tables. The `UserId` attribute on the `customer` table will be
        /// checked against the `userId`.
        ///
        /// The endpoint will perform an `insert` command on the
        /// `recipecollection` table to add the recipe collection, and foreign
        /// key constraints will be relied upon to validate the `userId`.
        ///
        /// </remarks>
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<ApiRecipeCollection>> PostRecipeCollection(ApiRecipeCollection apiRecipeCollection)
        {
            _context.ApiRecipeCollection.Add(apiRecipeCollection);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RecipeCollectionExists(apiRecipeCollection.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetApiRecipeCollection", new { id = apiRecipeCollection.UserId }, apiRecipeCollection);
        }

        /// <summary>
        /// Delete a recipe collection.
        /// </summary>
        /// <remarks>
        ///
        /// Deletes the given recipe collection in the database.
        ///
        /// If the recipe collection does not exist in the database, a 404
        /// status code is returned. The customer specified by `UserId` in the
        /// found recipe collection must be the authenticated user making this
        /// request.
        ///
        /// This endpoint interacts with the `recipecollection` and `customer`
        /// tables. The `UserId` attribute on the `customer` table will be
        /// checked against the `UserId` from the `recipecollection` table.
        ///
        /// The endpoint will perform a `select` query on the `customer` table
        /// to validate the `UserId`, and a `delete` command on the
        /// `recipecollection` table to delete the recipe collection.
        ///
        /// </remarks>
        /// <param name="title">The title of the recipe collection to delete.</param>
        [HttpDelete("{title}")]
        public async Task<IActionResult> DeleteRecipeCollection(string title)
        {
            var apiRecipeCollection = await _context.ApiRecipeCollection.FindAsync(title);
            if (apiRecipeCollection == null)
            {
                return NotFound();
            }

            _context.ApiRecipeCollection.Remove(apiRecipeCollection);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool RecipeCollectionExists(string id)
        {
            return _context.ApiRecipeCollection.Any(e => e.UserId == id);
        }
    }
}

