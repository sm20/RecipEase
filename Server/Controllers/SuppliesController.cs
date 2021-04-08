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
using RecipEase.Shared.ApiResponses;
using RecipEase.Shared.Models;

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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SuppliesController(RecipEaseContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Returns suppliers that supply an ingredient.
        /// </summary>
        /// <remarks>
        ///
        /// The suppliers are retrieved from the `Supplies` and `Supplier` tables
        /// using `select` queries.
        /// 
        /// </remarks>
        /// <param name="ingredientName">The name of the ingredient whose suppliers should be retrieved.</param>
        [HttpGet("{ingredientName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<ApiSupplier>>> GetSuppliersOfIngredient(string ingredientName)
        {
            var query =
                from supplies in _context.Supplies
                join supplier in _context.Supplier on supplies.UserId equals supplier.UserId
                where supplies.IngrName == ingredientName && supplies.Quantity > 0
                select supplier.ToApiSupplier();
            return await query.ToListAsync();
        }

        /// <summary>
        /// Returns ingredients supplied by a supplier.
        /// </summary>
        /// <remarks>
        ///
        /// The ingredients, units, and quantities are retrieved from the `Supplies`, `Ingredient`, and `Unit` tables
        /// using `select` queries. The supplier with the given username is retrieved from the `Users` table as
        /// necessary using a `select query.
        /// 
        /// </remarks>
        /// <param name="userId">The user id of the supplier whose supplied ingredients should be retrieved.</param>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<QuantifiedIngredient>>> GetSuppliedIngredients(string userId)
        {
            var query =
                from supplies in _context.Supplies
                join unit in _context.Unit on supplies.UnitName equals unit.Name
                join ingredient in _context.Ingredient on supplies.IngrName equals ingredient.Name
                where supplies.UserId == userId
                select new QuantifiedIngredient()
                {
                    Ingredient = new ApiIngredient()
                    {
                        Name = ingredient.Name,
                        Rarity = ingredient.Rarity,
                        WeightToVolRatio = ingredient.WeightToVolRatio
                    },
                    Quantity = supplies.Quantity,
                    Unit = new ApiUnit()
                    {
                        Name = unit.Name,
                        Symbol = unit.Symbol,
                        UnitType = unit.UnitType
                    }
                };

            var ingredientList = await query.ToListAsync();

            if (ingredientList == null)
            {
                return NotFound();
            }

            return ingredientList;
        }

        /// <summary>
        /// Update the information of an existing supplies entry
        /// </summary>
        /// <remarks>
        ///
        /// Updates the quantity information of an existing supplies entry
        /// in the supplies table of the database.
        /// The authenticated user must be the supplier whose ingredient is to be updated.
        ///
        /// An Update operation is used to update the supplies entry in the database, if
        /// the entry exists.
        /// </remarks>
        ///<param name="userId">The supplier who supplies the ingredient to be updated.</param>
        ///<param name="quantifiedIngredient">The new ingredient data.</param>
        [HttpPut]
        [Consumes("application/json")]
        public async Task<IActionResult> PutSupplies(string userId, QuantifiedIngredient quantifiedIngredient)
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != currentUserId)
            {
                return Unauthorized();
            }

            var updatedSupplies = new Supplies()
            {
                Quantity = quantifiedIngredient.Quantity,
                UnitName = quantifiedIngredient.Unit.Name,
                IngrName = quantifiedIngredient.Ingredient.Name,
                UserId = userId
            };

            _context.Entry(updatedSupplies).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuppliesExists(updatedSupplies))
                {
                    return NotFound();
                }

                throw;
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
        /// The authenticated user must be the supplier of the new item.
        /// 
        /// An Insert  operation to insert a new supplies entry is performed.
        /// </remarks>
        ///<param name="apiSupplies">The Supplies object to be updated.</param>
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<ApiSupplies>> PostApiSupplies(ApiSupplies apiSupplies)
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (apiSupplies.UserId != currentUserId)
            {
                return Unauthorized();
            }
            
            var supplies = Supplies.FromApiSupplies(apiSupplies);
            await _context.Supplies.AddAsync(supplies);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SuppliesExists(supplies))
                {
                    return Conflict();
                }

                throw;
            }

            return CreatedAtAction("GetSuppliedIngredients", new {id = apiSupplies.IngrName}, apiSupplies);
        }

        /// <summary>
        /// Delete an existing supplies entry
        /// </summary>
        /// <remarks>
        ///
        /// Delete a supplies entry from the Supplies relation,
        /// if the entry exists in the Supplies relation of the database.
        /// 
        /// The authenticated user must be the supplier of the item to be deleted.
        /// 
        /// A Delete operation to delete a supplies entry is performed.
        /// </remarks>
        ///<param name="apiSupplies">The entry to delete.</param>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteSupplies(ApiSupplies apiSupplies)
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (apiSupplies.UserId != currentUserId)
            {
                return Unauthorized();
            }

            var deletedSupplies = Supplies.FromApiSupplies(apiSupplies);

            _context.Entry(deletedSupplies).State = EntityState.Deleted;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuppliesExists(deletedSupplies))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        private bool SuppliesExists(Supplies supplies)
        {
            return _context.Supplies.Any(e =>
                e.IngrName == supplies.IngrName && e.UnitName == supplies.UnitName && e.UserId == supplies.UserId);
        }
    }
}