using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipEase.Server.Data;
using RecipEase.Shared.Models.Api;
using Microsoft.AspNetCore.Authorization;

namespace RecipEase.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class SuppliesController : ControllerBase
    {
        private readonly RecipEaseContext _context;

        public SuppliesController(RecipEaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns list of Suppliers' Ingredient stock
        /// </summary>
        /// <remarks>
        ///
        /// Retrieves all ingredients that all suppliers supply,
        /// and their associated attributes, from the `supplies` table.
        ///
        /// A 'select*' query with a 'where' clause to find the list of ingredients
        ///and their associated attributes is performed.
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiSupplies>>> GetApiSupplies()
        {
            return await _context.ApiSupplies.ToListAsync();
        }

        /// <summary>
        /// Returns the list of suppliers of an ingredient
        /// </summary>
        /// <remarks>
        ///
        /// Retrieves the object with the given ingredient name, from the
        /// `Supplies` table, if it exists.
        ///
        /// A 'select user,ingredient' query with a 'where' clause to find the ingredient
        /// and its associated suppliers.
        /// </remarks>
        /// <param name="id">The ingredient name in the Supplies table.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiSupplies>> GetApiSupplies(string id)
        {
            var apiSupplies = await _context.ApiSupplies.FindAsync(id);

            if (apiSupplies == null)
            {
                return NotFound();
            }

            return apiSupplies;
        }

        /// <summary>
        /// Update the information of an existing supplies entry
        /// </summary>
        /// <remarks>
        ///
        /// Updates information of an existing supplies entry
        /// in the supplies table of the database.
        /// The authenticated user must be the user to update the entry.
        ///
        /// An Update operation is used to update the supplies entry in the database, if
        /// the entry exists.
        /// </remarks>
        ///<param name="id">The ingredient to update.</param>
        ///<param name="apiSupplies">The Supplies object to be updated.</param>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutApiSupplies(string id, ApiSupplies apiSupplies)
        {
            if (id != apiSupplies.IngrName)
            {
                return BadRequest();
            }

            _context.Entry(apiSupplies).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApiSuppliesExists(id))
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
        /// Add a new supplies entry
        /// </summary>
        /// <remarks>
        ///
        /// Add a new supplies entry to the Supplies relation,
        /// if the entry does not exist in the Supplies relation of the database.
        /// 
        /// An Insert  operation to insert a new supplies entry is performed.
        /// </remarks>
        ///<param name="apiSupplies">The Supplies object to be updated.</param>
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<ApiSupplies>> PostApiSupplies(ApiSupplies apiSupplies)
        {
            _context.ApiSupplies.Add(apiSupplies);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ApiSuppliesExists(apiSupplies.IngrName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetApiSupplies", new { id = apiSupplies.IngrName }, apiSupplies);
        }

        /// <summary>
        /// Delete an existing supplies entry
        /// </summary>
        /// <remarks>
        ///
        /// Delete a supplies entry from the Supplies relation,
        /// if the entry exists in the Supplies relation of the database.
        /// 
        /// A Delete operation to delete a supplies entry is performed.
        /// </remarks>
        ///<param name="id">The supplies entry to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApiSupplies(string id)
        {
            var apiSupplies = await _context.ApiSupplies.FindAsync(id);
            if (apiSupplies == null)
            {
                return NotFound();
            }

            _context.ApiSupplies.Remove(apiSupplies);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApiSuppliesExists(string id)
        {
            return _context.ApiSupplies.Any(e => e.IngrName == id);
        }
    }
}
