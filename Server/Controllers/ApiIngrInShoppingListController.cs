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
    public class ApiIngrInShoppingListController : ControllerBase
    {
        private readonly RecipEaseContext _context;

        public ApiIngrInShoppingListController(RecipEaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns the item in the shopping list of a user
        /// </summary>
        /// <remarks>
        ///
        /// Retrieves ingredient in a user's shopping list with according unit.
        /// The authenticated user making this request must be the owner of the
        /// shopping list.
        ///
        /// </remarks>
        /// <param name="id">id of the user who have the shopping list to converse to.</param>

        [HttpGet("{id")]
        public async Task<ActionResult<ApiIngrInShoppingList>> GetApiIngrInShoppingList(string id)
        {
            var apiIngrInShoppingList = await _context.ApiIngrInShoppingList.FindAsync(id);

            if (apiIngrInShoppingList == null)
            {
                return NotFound();
            }

            return apiIngrInShoppingList;
        }

        /// <summary>
        /// Returns the a specific ingredient in the shopping list of a user
        /// </summary>
        /// <remarks>
        ///
        /// Retrieves a specific ingredient in a user's shopping list with according unit.
        /// The authenticated user making this request must be the owner of the
        /// shopping list.
        ///
        /// </remarks>
        /// <param name="id">id of the user who have the shopping list.</param>
        /// <param name="uname">name of the unit of the ingredient in the shopping list</param>
        /// <param name= "iname">name of the ingredient in shopping list.</param>
        /// 
        [HttpGet("{id,uname,iname}")]
        public async Task<ActionResult<ApiIngrInShoppingList>> GetApiIngrInShoppingList(string id, string uname, string iname)
        {
            var apiIngrInShoppingList = await _context.ApiIngrInShoppingList.FindAsync(id);

            if (apiIngrInShoppingList == null)
            {
                return NotFound();
            }

            return apiIngrInShoppingList;
        }

        /// <summary>
        /// Edit the a specific ingredient in the shopping list of a user
        /// </summary>
        /// <remarks>
        ///
        /// Edit a specific ingredient in a user's shopping list with according unit.
        /// The authenticated user making this request must be the owner of the
        /// shopping list.
        ///
        /// </remarks>
        /// <param name="id">id of the user who have the shopping list.</param>
        /// <param name="uname">name of the unit of the ingredient in the shopping list</param>
        /// <param name= "iname">name of the ingredient in shopping list.</param>
        ///

        [HttpPut("{id, uname, iname}")]
        public async Task<IActionResult> PutApiIngrInShoppingList(string id, ApiIngrInShoppingList apiIngrInShoppingList)
        {
            if (id != apiIngrInShoppingList.UserId)
            {
                return BadRequest();
            }

            _context.Entry(apiIngrInShoppingList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApiIngrInShoppingListExists(id))
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
        /// create an ingredient in a shopping list
        /// </summary>
        /// <remarks>
        ///
        /// create a new IngrInShoppingList.
        /// The authenticated user making this request must be the owner of the
        /// shopping list.
        /// given names should not exists in the database
        /// </remarks>

        [HttpPost]
        public async Task<ActionResult<ApiIngrInShoppingList>> PostApiIngrInShoppingList(ApiIngrInShoppingList apiIngrInShoppingList)
        {
            _context.ApiIngrInShoppingList.Add(apiIngrInShoppingList);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ApiIngrInShoppingListExists(apiIngrInShoppingList.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetApiIngrInShoppingList", new { id = apiIngrInShoppingList.UserId }, apiIngrInShoppingList);
        }

        /// <summary>
        /// Delete the a specific ingredient in the shopping list of a user
        /// </summary>
        /// <remarks>
        ///
        /// Delete a specific ingredient in a user's shopping list with according unit.
        /// The authenticated user making this request must be the owner of the
        /// shopping list.
        ///
        /// </remarks>
        /// <param name="id">id of the user who have the shopping list.</param>
        /// <param name="uname">name of the unit of the ingredient in the shopping list</param>
        /// <param name= "iname">name of the ingredient in shopping list.</param>
        ///
        [HttpDelete("{id, uname, iname}")]
        public async Task<IActionResult> DeleteApiIngrInShoppingList(string id)
        {
            var apiIngrInShoppingList = await _context.ApiIngrInShoppingList.FindAsync(id);
            if (apiIngrInShoppingList == null)
            {
                return NotFound();
            }

            _context.ApiIngrInShoppingList.Remove(apiIngrInShoppingList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApiIngrInShoppingListExists(string id)
        {
            return _context.ApiIngrInShoppingList.Any(e => e.UserId == id);
        }
    }
}
