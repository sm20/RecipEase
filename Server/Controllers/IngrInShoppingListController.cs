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

    [ApiController]
    public class IngrInShoppingListController : ControllerBase
    {
        private readonly RecipEaseContext _context;

        public IngrInShoppingListController(RecipEaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns all the items in an user's shopping list
        /// </summary>
        /// <remarks>
        ///
        /// functionalities : Retrieves ingredient in a user's shopping list with according unit.
        /// 
        /// database: IngrInShoppingList, Customer
        /// 
        /// constraints: The authenticated user making this request must be the owner of the
        /// shopping list.
        /// 
        /// query: select * IngrInShoppingList with UserId = id
        /// 
        /// </remarks>
        /// <param name="id">id of the user who have the shopping list to converse to.</param>

        [HttpGet("{id}")]
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
        /// Returns the specific item in the IngrInShoppingList
        /// </summary>
        /// <remarks>
        ///
        /// functionalities : Retrieves an item ingredient with a specific userid ,ingredient and unit.
        /// 
        /// database: IngrInShoppingList, Customer
        /// 
        /// constraints: The authenticated user making this request must be the owner of the
        /// shopping list.
        /// 
        /// query: select * IngrInShoppingList with UserId = id and IngrName = iname and UnitName = uname
        /// 
        /// </remarks>
        [HttpGet]
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
        /// Edit the specific item in the IngrInShoppingList
        /// </summary>
        /// <remarks>
        ///
        /// functionalities : Edit an item ingredient with a specific userid ,ingredient and unit.
        /// 
        /// database: IngrInShoppingList, Customer
        /// 
        /// constraints: The authenticated user making this request must be the owner of the
        /// shopping list.
        /// 
        /// query: update * IngrInShoppingList set (some instance) where UserId = id and IngrName = iname and UnitName = uname
        /// 
        /// </remarks>

        [HttpPut]
        [Consumes("application/json")]
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
        ///   Create new row in IngrInShoppingList
        /// </summary>
        /// <remarks>
        ///
        /// functionalities : Insert a row with a specific userid ,ingredient and unit.
        /// 
        /// database: IngrInShoppingList, Customer
        /// 
        /// constraints: The authenticated user making this request must be the owner of the
        /// shopping list.
        /// 
        /// query: insert into IngrInShoppingList
        /// </remarks>
        [HttpPost]
        [Consumes("application/json")]
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
        /// Delete an ingredient in an user's shopping list
        /// </summary>
        /// <remarks>
        ///
        /// functionalities : Retrieves ingredient in a user's shopping list with according unit.
        /// 
        /// database: IngrInShoppingList, Customer
        /// 
        /// constraints: The authenticated user making this request must be the owner of the
        /// shopping list.
        /// 
        /// query: delete from IngrInShoppingList where userId = userId, ingrName = ingrName, unitName = unitName
        /// 
        /// </remarks>

        [HttpDelete]
        public async Task<IActionResult> DeleteApiIngrInShoppingList(ApiIngrInShoppingList apiIngrInShoppingList)
        {
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
