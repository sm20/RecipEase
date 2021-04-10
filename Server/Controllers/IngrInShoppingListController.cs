using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipEase.Server.Data;
using RecipEase.Shared.Models;
using RecipEase.Shared.Models.Api;

namespace RecipEase.Server.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class IngrInShoppingListController : ControllerBase
    {
        private readonly RecipEaseContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IngrInShoppingListController(RecipEaseContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutIngrInShoppingList(ApiIngrInShoppingList apiIngredient)
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (apiIngredient.UserId != currentUserId)
            {
                return Unauthorized();
            }

            var updatedIngredient = apiIngredient.ToIngrInShoppingList();
            
            _context.Entry(updatedIngredient).State = EntityState.Modified;
        
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngrInShoppingListExists(updatedIngredient))
                {
                    return NotFound();
                }

                throw;
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
        public async Task<ActionResult<ApiIngrInShoppingList>> PostIngrInShoppingList(
            ApiIngrInShoppingList apiIngredient)
        {
            var ingredient = apiIngredient.ToIngrInShoppingList();
            _context.IngrInShoppingList.Add(ingredient);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (IngrInShoppingListExists(ingredient))
                {
                    return Conflict();
                }

                throw;
            }
        
            return Created("", apiIngredient);
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteIngrInShoppingList(ApiIngrInShoppingList ingredient)
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (ingredient.UserId != currentUserId)
            {
                return Unauthorized();
            }

            var deletedIngredient = ingredient.ToIngrInShoppingList();

            _context.Entry(deletedIngredient).State = EntityState.Deleted;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngrInShoppingListExists(deletedIngredient))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        private bool IngrInShoppingListExists(IngrInShoppingList i)
        {
            return _context.ApiIngrInShoppingList.Any(e =>
                e.UserId == i.UserId && e.IngrName == i.IngrName && e.UnitName == i.UnitName);
        }
    }
}