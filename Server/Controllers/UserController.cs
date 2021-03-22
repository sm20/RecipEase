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
    public class UserController : ControllerBase
    {
        private readonly RecipEaseContext _context;

        public UserController(RecipEaseContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiUser>>> GetApiUser()
        {
            return await _context.ApiUser.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiUser>> GetApiUser(string id)
        {
            var apiUser = await _context.ApiUser.FindAsync(id);

            if (apiUser == null)
            {
                return NotFound();
            }

            return apiUser;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApiUser(string id, ApiUser apiUser)
        {
            if (id != apiUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(apiUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApiUserExists(id))
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiUser>> PostApiUser(ApiUser apiUser)
        {
            _context.ApiUser.Add(apiUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ApiUserExists(apiUser.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetApiUser", new { id = apiUser.Id }, apiUser);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApiUser(string id)
        {
            var apiUser = await _context.ApiUser.FindAsync(id);
            if (apiUser == null)
            {
                return NotFound();
            }

            _context.ApiUser.Remove(apiUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApiUserExists(string id)
        {
            return _context.ApiUser.Any(e => e.Id == id);
        }
    }
}
