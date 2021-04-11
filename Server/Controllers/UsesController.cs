using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipEase.Server.Data;
using RecipEase.Shared.Models.Api;
using RecipEase.Shared.Models;
using Microsoft.AspNetCore.Authorization;

namespace RecipEase.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]

    public class UsesController : ControllerBase
    {
        private readonly RecipEaseContext _context;

        public UsesController(RecipEaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns list of all ingredients used by all recipes
        /// </summary>
        /// <remarks>
        /// Retrieves all ingredients that all recipes use,
        /// and their associated units, from the `Uses` table.
        ///
        /// A 'select*' query with a 'where' clause to find the list of ingredients
        /// used by all recipes,
        ///and their associated attributes, is performed.
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiUses>>> GetApiUses()
        {
            return await _context.ApiUses.ToListAsync();
        }

        /// <summary>
        /// Returns list of all ingredients used by a given recipe
        /// </summary>
        /// <remarks>
        ///
        /// Retrieves all ingredients that a given recipe uses,
        /// and their associated units attribute, from the `Uses` table.
        ///
        /// A 'select*' query with a 'where' clause to find the list of ingredients
        /// used by a recipe,
        ///and their associated attributes, is performed.
        /// <param name="id">The id of the recipe who's ingredients should be retrieved.</param>
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ApiUses>>> GetApiUses(int id)
        {
            var uses = await _context.Uses.Where(u => u.RecipeId.Equals(id)).Select(u => u.ToApiUses()).ToListAsync();

            if (uses == null)
            {
                return NotFound();
            }
            return uses;
        }

        /// <summary>
        /// Update ingredients of a recipe
        /// </summary>
        /// <remarks>
        ///
        /// Retrieves the object with the given recipe id, from the
        /// `Uses` table, if it exists.
        /// The recipe owner must be the user to update.
        ///
        /// An update query is performed using the recipe id, to update the ingredients
        /// and their associated units.
        /// </remarks>
        /// <param name="apiUses">The associated object from the Uses table.</param>
        [HttpPut]
        [Consumes("application/json")]
        public async Task<IActionResult> PutApiUses(ApiUses apiUses)
        {
             Uses uses = new Uses
            {
                RecipeId = apiUses.RecipeId,
                IngrName = apiUses.IngrName,
                Quantity = apiUses.Quantity,
                UnitName = apiUses.UnitName

        };
            _context.Entry(uses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                System.Console.WriteLine(e.Message);
                if (!ApiUsesExists(apiUses.RecipeId, apiUses.UnitName, apiUses.IngrName))
                {
                    System.Console.WriteLine(apiUses.RecipeId);
                    System.Console.WriteLine(apiUses.UnitName);
                    System.Console.WriteLine(apiUses.IngrName);
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Add a new Uses entry
        /// </summary>
        /// <remarks>
        ///
        /// Add a new Uses entry to the Uses relation,
        /// if the entry does not exist in the Uses relation of the database.
        /// 
        /// The recipe owner must be the user to add a new entry.
        /// An Insert  operation to insert a new Uses entry is performed.
        /// </remarks>
        /// <param name="apiUses">The associated object from the Uses table.</param>
        [HttpPost]
        public async Task<ActionResult<ApiUses>> PostApiUses(Uses apiUses)
        {
            _context.Uses.Add(apiUses);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ApiUsesExists(apiUses.RecipeId, apiUses.UnitName, apiUses.IngrName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created("", apiUses);
        }

        /// <summary>
        /// Delete an existing Uses entry
        /// </summary>
        /// <remarks>
        ///
        /// Delete a Uses entry from the Uses relation based on recipe_id,
        /// if the entry exists in the Uses relation of the database.
        /// 
        /// A Delete operation to delete a Uses entry is performed.
        /// </remarks>
        [HttpDelete]
        public async Task<IActionResult> DeleteApiUses(ApiUses apiUses)
        {
            Uses uses = new Uses
            {
                RecipeId = apiUses.RecipeId,
                IngrName = apiUses.IngrName,
                Quantity = apiUses.Quantity,
                UnitName = apiUses.UnitName

        };
        _context.Entry(uses).State = EntityState.Deleted;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApiUsesExists(apiUses.RecipeId, apiUses.UnitName, apiUses.IngrName))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        private bool ApiUsesExists(int rec, string unit, string ingr)
        {
            return _context.Uses.Any(e => (e.RecipeId == rec && e.UnitName == unit && e.IngrName == ingr));
        }
    }
}
