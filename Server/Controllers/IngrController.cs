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
    public class IngrController : ControllerBase
    {
        private readonly RecipEaseContext _context;

        public IngrController(RecipEaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns all the ingredients
        /// </summary>
        /// <remarks>
        /// retrieve all ingredient from the Ingredients table.
        ///
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiIngredient>>> GetApiIngredient()
        {
            return await _context.ApiIngredient.ToListAsync();
        }

        /// <summary>
        /// Returns the ingredient with the given id
        /// </summary>
        /// <remarks>
        ///
        /// Retrieves ingredient with the given name in the Name column.
        ///
        /// </remarks>
        /// <param name="id">The Name of the Ingredient.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiIngredient>> GetApiIngredient(string id)
        {
            var apiIngredient = await _context.ApiIngredient.FindAsync(id);

            if (apiIngredient == null)
            {
                return NotFound();
            }

            return apiIngredient;
        }

        // /// <summary>
        // /// edit the ingredient with the given id
        // /// </summary>
        // /// <remarks>
        // ///
        // /// Updates the given ingredient in the database.
        // ///
        // /// Only an authenticated admin can make this request.
        // ///
        // /// The endpoint will perform an `update` command on the `ingredient` table
        // /// </remarks>
        // /// <param name="id">The Name of the ingredient to retrieve.</param>
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutApiIngredient(string id, ApiIngredient apiIngredient)
        // {
        //     if (id != apiIngredient.Name)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(apiIngredient).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!ApiIngredientExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // /// <summary>
        // /// create an ingredient
        // /// </summary>
        // /// <remarks>
        // ///
        // /// create a new ingredient.
        // /// Only an authenticated admin can make this request.
        // /// given name should not exists in the database
        // ///
        // /// </remarks>        

        // [HttpPost]
        // public async Task<ActionResult<ApiIngredient>> PostApiIngredient(ApiIngredient apiIngredient)
        // {
        //     _context.ApiIngredient.Add(apiIngredient);
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateException)
        //     {
        //         if (ApiIngredientExists(apiIngredient.Name))
        //         {
        //             return Conflict();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return CreatedAtAction("GetApiIngredient", new { id = apiIngredient.Name }, apiIngredient);
        // }

        // /// <summary>
        // /// delete the ingredient with the given id
        // /// </summary>
        // /// <remarks>
        // ///
        // /// Delete the given ingredient in the database.
        // ///
        // /// Only an authenticated admin can make this request.
        // ///
        // /// The endpoint will perform an `delete from` command on the `ingredient` table
        // /// </remarks>
        // /// <param name="id">The Name of the ingredient to delete.</param>
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteApiIngredient(string id)
        // {
        //     var apiIngredient = await _context.ApiIngredient.FindAsync(id);
        //     if (apiIngredient == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.ApiIngredient.Remove(apiIngredient);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        private bool ApiIngredientExists(string id)
        {
            return _context.ApiIngredient.Any(e => e.Name == id);
        }
    }
}
