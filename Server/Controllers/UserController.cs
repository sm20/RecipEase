using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipEase.Server.Data;
using RecipEase.Shared.Models.Api;

namespace RecipEase.Server.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly RecipEaseContext _context;

        public UserController(RecipEaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns all User credential information.
        /// </summary>
        /// <remarks>
        ///
        /// Retrieves all items and all attributes from the `user`
        /// table.
        /// A 'select*' query with a 'where' clause to find the list of usernames
        ///and their associated attributes.
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiUser>>> GetApiUser()
        {
            return await _context.ApiUser.ToListAsync();
        }

        /// <summary>
        /// Returns the User with the given username id.
        /// </summary>
        /// <remarks>
        ///
        /// Retrieves the object with the given username id value, in the username column, from the
        /// `User` table, if it exists.
        ///
        /// A 'select*' query with a 'where' clause to find the username id
        /// and its associated attributes.
        /// </remarks>
        /// <param name="id">The Username ID of the User to retrieve.</param>
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

        /// <summary>
        /// Update the information of an existing user
        /// </summary>
        /// <remarks>
        ///
        /// Updates information of existing user
        /// if the user exists in the User table of the database.
        /// The authenticated user must be the user to be updated.
        ///
        /// An Update operation is used to update the User in the database if
        /// the user exists.
        /// </remarks>
        ///<param name="id">The username of the User to update.</param>
        ///<param name="apiUser">The User object to be updated.</param>
        [HttpPut("{id}")]
        [Consumes("application/json")]
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

        /// <summary>
        /// Add a new User
        /// </summary>
        /// <remarks>
        ///
        /// Add a new User to the User relation,
        /// if the username does not exist in the User relation of the database.
        /// 
        /// An Insert  operation to insert a new User is performed.
        /// </remarks>
        ///<param name="apiUser">The User object to be updated.</param>
        [HttpPost]
        [Consumes("application/json")]
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

        /// <summary>
        /// Delete aUser
        /// </summary>
        /// <remarks>
        ///
        /// Delete a User from the database,
        /// if the user exists in the User relation of the database.
        /// The authenticated user must be the user to be deleted.
        /// 
        /// A Delete operation to delete a User is performed.
        /// </remarks>
        ///<param name="id">The username of the User to delete.</param>
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
