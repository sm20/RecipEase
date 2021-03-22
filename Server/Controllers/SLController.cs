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
    [ApiController]
    public class SLController : ControllerBase
    {
        private readonly RecipEaseContext _context;

        public SLController(RecipEaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns the ShoppingList with the given userId.
        /// </summary>
        /// <remarks>
        ///
        /// Retrieves shopping list with the given userId in the UserId column from the
        /// `ShoppingList` table.
        ///
        /// </remarks>
        /// <param name="userId">The UserId of the ShoppingList to retrieve.</param>


        [HttpGet("{userId}")]
        public async Task<ActionResult<ApiShoppingList>> GetApiShoppingList(string userId)
        {
            var apiShoppingList = await _context.ApiShoppingList.FindAsync(userId);

            if (apiShoppingList == null)
            {
                return NotFound();
            }

            return apiShoppingList;
        }

        /// <summary>
        /// Edit the ShoppingList with the given userId.
        /// </summary>
        /// <remarks>
        ///
        /// Updates the given shopping list in the database.
        ///
        /// The customer specified by `UserId` must be the authenticated user
        /// making this request.
        ///
        /// The endpoint will perform an `update` command on the `ShoppingList` table
        /// to update the ShoppingList, and foreign key constraints will be relied
        /// upon to validate the `UserId`.
        ///
        /// </remarks>
        /// <param name="userId">The ID of the ApiClass to retrieve.</param>

        [HttpPut("{userId}")]
        public async Task<IActionResult> PutApiShoppingList(string userId, ApiShoppingList apiShoppingList)
        {
            if (userId != apiShoppingList.UserId)
            {
                return BadRequest();
            }

            _context.Entry(apiShoppingList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApiShoppingListExists(userId))
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

        private bool ApiShoppingListExists(string id)
        {
            return _context.ApiShoppingList.Any(e => e.UserId == id);
        }
    }
}
