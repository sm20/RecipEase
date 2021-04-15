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
using RecipEase.Shared.ApiResponses;
using RecipEase.Shared.Models.Api;
using RecipEase.Shared.Models;

namespace RecipEase.Server.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class ShoppingListController : ControllerBase
    {
        private readonly RecipEaseContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShoppingListController(RecipEaseContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Returns a user's shopping list
        /// </summary>
        /// <remarks>
        ///
        /// functionalities : Retrieves the shopping list of the currently logged in user. Creates the shopping list of
        /// the user if it does not exist.
        /// 
        /// database: ShoppingList, User, Ingredient, IngrInShoppingList, Unit
        /// 
        /// constraints: The authenticated user making this request must be the owner of the
        /// shopping list.
        /// 
        /// query: select * ShoppingList with UserId = userId, select * ingredients where ingredient is in shopping list
        /// based on ingrinshoppinglist table
        /// 
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ShoppingListResponse>> GetApiShoppingList()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var query =
                from shoppingList in _context.ShoppingList
                where shoppingList.UserId == userId
                select new ShoppingListResponse
                {
                    Ingredients = (
                        from ingredientInList in _context.IngrInShoppingList
                        join ingredient in _context.Ingredient on ingredientInList.IngrName equals ingredient.Name
                        where ingredientInList.UserId == shoppingList.UserId
                        select new QuantifiedIngredient
                        {
                            Ingredient = ingredient.ToApiIngredient(),
                            Quantity = ingredientInList.Quantity,
                            Unit = ingredientInList.Unit.ToApiUnit()
                        }).ToList(),
                    _shoppingList = shoppingList.ToApiShoppingList()
                };

            var shoppingListResponse = await query.FirstOrDefaultAsync();

            if (shoppingListResponse != null) return shoppingListResponse;

            var defaultShoppingList = new ShoppingList
            {
                Name = "My Shopping List",
                LastUpdate = DateTime.Now,
                UserId = userId,
                NumIngredients = 0
            };
            await _context.AddAsync(defaultShoppingList);
            return new ShoppingListResponse
            {
                _shoppingList = defaultShoppingList.ToApiShoppingList(),
                Ingredients = new List<QuantifiedIngredient>()
            };
        }
    }
}