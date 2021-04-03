using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipEase.Server.Data;
using RecipEase.Shared.Models.Api;
using Microsoft.AspNetCore.Authorization;
using RecipEase.Shared.Models;

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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SupplierController(RecipEaseContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Returns the Supplier with the given username.
        /// </summary>
        /// <remarks>
        ///
        /// Retrieves the object with the given username value, in the username column, from the
        /// `Supplier` table, if it exists.
        ///
        /// A 'select*' query with a 'where' clause to find the username
        /// and its associated attributes.
        /// </remarks>
        /// <param name="userId">The username of the Supplier to retrieve.</param>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ApiSupplier>> GetSupplier(string userId)
        {
            var supplier = await _context.Supplier.FindAsync(userId);

            if (supplier == null)
            {
                return NotFound();
            }

            return new ApiSupplier()
            {
                UserId = supplier.UserId,
                Email = supplier.Email,
                Website = supplier.Website,
                PhoneNo = supplier.PhoneNo,
                SupplierName = supplier.SupplierName,
                StoreVisitCount = supplier.StoreVisitCount
            };
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
        ///<param name="id">The user id of the Supplier to update.</param>
        ///<param name="apiSupplier">The Supplier object to be updated.</param>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutSupplier(string id, ApiSupplier apiSupplier)
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (id != currentUserId)
            {
                return Unauthorized();
            }

            if (id != apiSupplier.UserId)
            {
                return BadRequest();
            }

            var supplier = new Supplier()
            {
                Email = apiSupplier.Email,
                Website = apiSupplier.Website,
                PhoneNo = apiSupplier.PhoneNo,
                SupplierName = apiSupplier.SupplierName,
                UserId = apiSupplier.UserId,
                StoreVisitCount = apiSupplier.StoreVisitCount
            };

            _context.Entry(supplier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(id))
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
                if (SupplierExists(apiSupplier.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSupplier", new { id = apiSupplier.UserId }, apiSupplier);
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

        private bool SupplierExists(string id)
        {
            return _context.Supplier.Any(e => e.UserId == id);
        }
    }
}
