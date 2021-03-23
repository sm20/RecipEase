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
    public class SupplierController : ControllerBase
    {
        private readonly RecipEaseContext _context;

        public SupplierController(RecipEaseContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiSupplier>>> GetApiSupplier()
        {
            return await _context.ApiSupplier.ToListAsync();
        }

        /// <summary>
        /// Returns the Supplier with the given username id.
        /// </summary>
        /// <remarks>
        ///
        /// Retrieves the object with the given username id value, in the username column, from the
        /// `Supplier` table, if it exists.
        ///
        /// A 'select*' query with a 'where' clause to find the username id
        /// and its associated attributes.
        /// </remarks>
        /// <param name="id">The Username ID of the Supplier to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiSupplier>> GetApiSupplier(string id)
        {
            var apiSupplier = await _context.ApiSupplier.FindAsync(id);

            if (apiSupplier == null)
            {
                return NotFound();
            }

            return apiSupplier;
        }

        /// <summary>
        /// Update the information of an existing supplier user
        /// </summary>
        /// <remarks>
        ///
        /// Updates information of existing supplier,
        /// if the supplier exists in the Supplier table of the database.
        /// The authenticated user must be the user to be updated.
        ///
        /// An Update operation is used to update the Supplier in the database if
        /// the user exists.
        /// </remarks>
        ///<param name="id">The username of the Supplier to update.</param>
        ///<param name="apiSupplier">The Supplier object to be updated.</param>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutApiSupplier(string id, ApiSupplier apiSupplier)
        {
            if (id != apiSupplier.UserId)
            {
                return BadRequest();
            }

            _context.Entry(apiSupplier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApiSupplierExists(id))
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
        /// Add a new Supplier user
        /// </summary>
        /// <remarks>
        ///
        /// Add a new Supplier to the Supplier relation,
        /// if the username does not exist in the Supplier relation of the database.
        /// 
        /// An Insert  operation to insert a new Suppplier user is performed.
        /// </remarks>
        ///<param name="apiSupplier">The Supplier object to be updated.</param>
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<ApiSupplier>> PostApiSupplier(ApiSupplier apiSupplier)
        {
            _context.ApiSupplier.Add(apiSupplier);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ApiSupplierExists(apiSupplier.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetApiSupplier", new { id = apiSupplier.UserId }, apiSupplier);
        }

        /// <summary>
        /// Delete a Supplier user
        /// </summary>
        /// <remarks>
        ///
        /// Delete a Supplier from the database,
        /// if the user exists in the Supplier relation of the database.
        /// The authenticated user must be the user to be deleted.
        /// 
        /// A Delete operation to delete a Supplier is performed.
        /// </remarks>
        ///<param name="id">The username of the Supplier to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApiSupplier(string id)
        {
            var apiSupplier = await _context.ApiSupplier.FindAsync(id);
            if (apiSupplier == null)
            {
                return NotFound();
            }

            _context.ApiSupplier.Remove(apiSupplier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApiSupplierExists(string id)
        {
            return _context.ApiSupplier.Any(e => e.UserId == id);
        }
    }
}
