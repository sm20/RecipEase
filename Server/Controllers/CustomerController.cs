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
    public class CustomerController : ControllerBase
    {
        private readonly RecipEaseContext _context;

        public CustomerController(RecipEaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns all Customer credential information.
        /// </summary>
        /// <remarks>
        ///
        /// Retrieves all items and all attributes from the 'customer'
        /// table.
        /// A 'select*' query with a 'where' clause to find the list of usernames
        ///and their associated attributes.
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiCustomer>>> GetApiCustomer()
        {
            return await _context.ApiCustomer.ToListAsync();
        }

        /// <summary>
        /// Returns the Customer with the given username id.
        /// </summary>
        /// <remarks>
        ///
        /// Retrieves the object with the given username id value, in the username column, from the
        /// `Customer` table, if it exists.
        ///
        /// A 'select*' query with a 'where' clause to find the username id
        /// and its associated attributes.
        /// </remarks>
        /// <param name="id">The Username ID of the Customer to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiCustomer>> GetApiCustomer(string id)
        {
            var apiCustomer = await _context.ApiCustomer.FindAsync(id);

            if (apiCustomer == null)
            {
                return NotFound();
            }

            return apiCustomer;
        }

        /// <summary>
        /// Update the information of an existing customer
        /// </summary>
        /// <remarks>
        ///
        /// Updates information/attributes of an existing user of type customer,
        /// if the user exists in the Customer table of the database.
        /// The authenticated user must be the user to be updated.
        ///
        /// An Update operation is used to update the Customer in the database if
        /// the user exists.
        /// </remarks>
        ///<param name="id">The username of the Customer to update.</param>
        ///<param name="apiUser">The Customer object to be updated.</param>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutApiCustomer(string id, ApiCustomer apiCustomer)
        {
            if (id != apiCustomer.UserId)
            {
                return BadRequest();
            }

            _context.Entry(apiCustomer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApiCustomerExists(id))
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
        /// Add a new Customer
        /// </summary>
        /// <remarks>
        ///
        /// Add a new Customer to the Customer relation,
        /// if the username does not exist in the Customer relation of the database.
        /// 
        /// An Insert  operation to insert a new Customer is performed.
        /// </remarks>
        ///<param name="apiUser">The Customer object to be updated.</param>
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<ApiCustomer>> PostApiCustomer(ApiCustomer apiCustomer)
        {
            _context.ApiCustomer.Add(apiCustomer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ApiCustomerExists(apiCustomer.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetApiCustomer", new { id = apiCustomer.UserId }, apiCustomer);
        }

        /// <summary>
        /// Delete a Customer user
        /// </summary>
        /// <remarks>
        ///
        /// Delete a Customer from the database,
        /// if the customer exists in the Customer relation of the database.
        /// The authenticated user must be the user to be deleted.
        /// 
        /// A Delete operation to delete a Customer is performed.
        /// </remarks>
        ///<param name="id">The username of the Customer to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApiCustomer(string id)
        {
            var apiCustomer = await _context.ApiCustomer.FindAsync(id);
            if (apiCustomer == null)
            {
                return NotFound();
            }

            _context.ApiCustomer.Remove(apiCustomer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApiCustomerExists(string id)
        {
            return _context.ApiCustomer.Any(e => e.UserId == id);
        }
    }
}
