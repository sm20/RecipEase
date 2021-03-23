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
    public class SuppliesController : ControllerBase
    {
        private readonly RecipEaseContext _context;

        public SuppliesController(RecipEaseContext context)
        {
            _context = context;
        }

        // GET: api/Supplies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiSupplies>>> GetApiSupplies()
        {
            return await _context.ApiSupplies.ToListAsync();
        }

        // GET: api/Supplies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiSupplies>> GetApiSupplies(string id)
        {
            var apiSupplies = await _context.ApiSupplies.FindAsync(id);

            if (apiSupplies == null)
            {
                return NotFound();
            }

            return apiSupplies;
        }

        // PUT: api/Supplies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApiSupplies(string id, ApiSupplies apiSupplies)
        {
            if (id != apiSupplies.IngrName)
            {
                return BadRequest();
            }

            _context.Entry(apiSupplies).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApiSuppliesExists(id))
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

        // POST: api/Supplies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiSupplies>> PostApiSupplies(ApiSupplies apiSupplies)
        {
            _context.ApiSupplies.Add(apiSupplies);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ApiSuppliesExists(apiSupplies.IngrName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetApiSupplies", new { id = apiSupplies.IngrName }, apiSupplies);
        }

        // DELETE: api/Supplies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApiSupplies(string id)
        {
            var apiSupplies = await _context.ApiSupplies.FindAsync(id);
            if (apiSupplies == null)
            {
                return NotFound();
            }

            _context.ApiSupplies.Remove(apiSupplies);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApiSuppliesExists(string id)
        {
            return _context.ApiSupplies.Any(e => e.IngrName == id);
        }
    }
}
