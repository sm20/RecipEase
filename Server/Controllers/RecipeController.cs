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
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly RecipEaseContext _context;

        public RecipeController(RecipEaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Documentation
        /// </summary>
        /// <remarks>
        ///
        /// More documentation.
        ///
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiRecipe>>> GetApiRecipe()
        {
            return await _context.ApiRecipe.ToListAsync();
        }

        /// <summary>
        /// Documentation
        /// </summary>
        /// <remarks>
        ///
        /// More documentation.
        ///
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiRecipe>> GetApiRecipe(int id)
        {
            var apiRecipe = await _context.ApiRecipe.FindAsync(id);

            if (apiRecipe == null)
            {
                return NotFound();
            }

            return apiRecipe;
        }

        /// <summary>
        /// Documentation
        /// </summary>
        /// <remarks>
        ///
        /// More documentation.
        ///
        /// </remarks>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutApiRecipe(int id, ApiRecipe apiRecipe)
        {
            if (id != apiRecipe.Id)
            {
                return BadRequest();
            }

            _context.Entry(apiRecipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApiRecipeExists(id))
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
        /// Documentation
        /// </summary>
        /// <remarks>
        ///
        /// More documentation.
        ///
        /// </remarks>
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<ApiRecipe>> PostApiRecipe(ApiRecipe apiRecipe)
        {
            _context.ApiRecipe.Add(apiRecipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApiRecipe", new { id = apiRecipe.Id }, apiRecipe);
        }

        /// <summary>
        /// Documentation
        /// </summary>
        /// <remarks>
        ///
        /// More documentation.
        ///
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApiRecipe(int id)
        {
            var apiRecipe = await _context.ApiRecipe.FindAsync(id);
            if (apiRecipe == null)
            {
                return NotFound();
            }

            _context.ApiRecipe.Remove(apiRecipe);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ApiRecipeExists(int id)
        {
            return _context.ApiRecipe.Any(e => e.Id == id);
        }
    }
}
