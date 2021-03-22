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
    public class UnitController : ControllerBase
    {
        private readonly RecipEaseContext _context;

        public UnitController(RecipEaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns all unit.
        /// </summary>
        /// <remarks>
        ///
        /// interact with the units table
        ///
        /// </remarks>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiUnit>>> GetApiUnit()
        {
            return await _context.ApiUnit.ToListAsync();
        }

        /// <summary>
        /// Returns the unit with the given id
        /// </summary>
        /// <remarks>
        ///
        /// Retrieves unit with the given id  in the UserId column from the
        /// `Name` table.
        ///
        /// </remarks>
        /// <param name="id">The Name of the Unit to retrieve.</param>


        [HttpGet("{id}")]
        public async Task<ActionResult<ApiUnit>> GetApiUnit(string id)
        {
            var apiUnit = await _context.ApiUnit.FindAsync(id);

            if (apiUnit == null)
            {
                return NotFound();
            }

            return apiUnit;
        }

        // /// <summary>
        // /// edit the unit with the given id
        // /// </summary>
        // /// <remarks>
        // ///
        // /// Updates the given unit in the database.
        // ///
        // /// Only an authenticated admin can make this request.
        // ///
        // /// The endpoint will perform an `update` command on the `unit` table
        // /// </remarks>
        // /// <param name="id">The Name of the Unit to retrieve.</param>

        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutApiUnit(string id, ApiUnit apiUnit)
        // {
        //     if (id != apiUnit.Name)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(apiUnit).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!ApiUnitExists(id))
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
        // /// create an unit
        // /// </summary>
        // /// <remarks>
        // ///
        // /// create a new unit.
        // /// Only an authenticated admin can make this request.
        // /// given name should not exists in the database
        // ///
        // /// </remarks>

        // [HttpPost]
        // public async Task<ActionResult<ApiUnit>> PostApiUnit(ApiUnit apiUnit)
        // {
        //     _context.ApiUnit.Add(apiUnit);
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateException)
        //     {
        //         if (ApiUnitExists(apiUnit.Name))
        //         {
        //             return Conflict();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return CreatedAtAction("GetApiUnit", new { id = apiUnit.Name }, apiUnit);
        // }

        // /// <summary>
        // /// delete the unit with the given id
        // /// </summary>
        // /// <remarks>
        // ///
        // /// Delete the given unit in the database.
        // ///
        // /// Only an authenticated admin can make this request.
        // ///
        // /// The endpoint will perform an `delete from` command on the `unit` table
        // /// </remarks>
        // /// <param name="id">The Name of the unit to delete.</param>

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteApiUnit(string id)
        // {
        //     var apiUnit = await _context.ApiUnit.FindAsync(id);
        //     if (apiUnit == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.ApiUnit.Remove(apiUnit);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        private bool ApiUnitExists(string id)
        {
            return _context.ApiUnit.Any(e => e.Name == id);
        }
    }
}
