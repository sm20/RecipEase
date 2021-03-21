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
    public class RecipeCollectionController : ControllerBase
    {
        private readonly RecipEaseContext _context;

        public RecipeCollectionController(RecipEaseContext context)
        {
            _context = context;
        }

        // GET: api/RecipeCollection
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiRecipeCollection>>> GetRecipeCollection()
        {
            return await _context.ApiRecipeCollection.ToListAsync();
        }

        // GET: api/RecipeCollection/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiRecipeCollection>> GetRecipeCollection(string id)
        {
            var apiRecipeCollection = await _context.ApiRecipeCollection.FindAsync(id);

            if (apiRecipeCollection == null)
            {
                return NotFound();
            }

            return apiRecipeCollection;
        }

        // PUT: api/RecipeCollection/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutRecipeCollection(string id, ApiRecipeCollection apiRecipeCollection)
        {
            if (id != apiRecipeCollection.UserId)
            {
                return BadRequest();
            }

            _context.Entry(apiRecipeCollection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeCollectionExists(id))
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

        // POST: api/RecipeCollection
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<ApiRecipeCollection>> PostRecipeCollection(ApiRecipeCollection apiRecipeCollection)
        {
            _context.ApiRecipeCollection.Add(apiRecipeCollection);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RecipeCollectionExists(apiRecipeCollection.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetApiRecipeCollection", new { id = apiRecipeCollection.UserId }, apiRecipeCollection);
        }

        // DELETE: api/RecipeCollection/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipeCollection(string id)
        {
            var apiRecipeCollection = await _context.ApiRecipeCollection.FindAsync(id);
            if (apiRecipeCollection == null)
            {
                return NotFound();
            }

            _context.ApiRecipeCollection.Remove(apiRecipeCollection);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeCollectionExists(string id)
        {
            return _context.ApiRecipeCollection.Any(e => e.UserId == id);
        }
    }
}

