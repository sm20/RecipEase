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
    public class UnitConverseController : ControllerBase
    {
        private readonly RecipEaseContext _context;

        public UnitConverseController(RecipEaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns all the unit conversion
        /// </summary>
        /// <remarks>
        ///retrieve all unit conversion from the UnitConversion table.
        ///
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiUnitConversion>>> GetApiUnitConversion()
        {
            return await _context.ApiUnitConversion.ToListAsync();
        }

        /// <summary>
        /// Returns the unit conversion  with the given ids
        /// </summary>
        /// <remarks>
        ///
        /// Retrieves unit conversion with the given names in the ConvertsToUnitName column and 
        /// ConvertsFromUnitName column.
        ///
        /// </remarks>
        /// <param name="id1">The Name of the Unit to converse to.</param>
        /// <param name="id2">The Name of the Unit to converse from.</param>

        [HttpGet("{id1, id2}")]
        public async Task<ActionResult<ApiUnitConversion>> GetApiUnitConversion(string id1, string id2)
        {
            var apiUnitConversion = _context.ApiUnitConversion.Find(id1, id2);

            if (apiUnitConversion == null)
            {
                return NotFound();
            }

            return apiUnitConversion;
        }

        // /// <summary>
        // /// Edit the unit conversion  with the given ids
        // /// </summary>
        // /// <remarks>
        // /// 
        // /// Only an authenticated admin can make this request.
        // /// Edit the  unit conversion  with the given names in the ConvertsToUnitName column and 
        // /// ConvertsFromUnitName column.
        // ///
        // /// </remarks>
        // /// <param name="id1">The Name of the Unit to converse to.</param>
        // /// <param name="id2">The Name of the Unit to converse from.</param>
        // [HttpPut("{id1, id2}")]
        // public async Task<IActionResult> PutApiUnitConversion(string id1, string id2, ApiUnitConversion apiUnitConversion)
        // {
        //     if (id1 != apiUnitConversion.ConvertsToUnitName && id2 != apiUnitConversion.ConvertsFromUnitName)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(apiUnitConversion).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!ApiUnitConversionExists(id1))
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
        // /// create a unit conversion
        // /// </summary>
        // /// <remarks>
        // ///
        // /// create a new unit conversion.
        // /// Only an authenticated admin can make this request.
        // /// given names should not exists in the database
        // /// </remarks>

        // [HttpPost]
        // public async Task<ActionResult<ApiUnitConversion>> PostApiUnitConversion(ApiUnitConversion apiUnitConversion)
        // {
        //     _context.ApiUnitConversion.Add(apiUnitConversion);
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateException)
        //     {
        //         if (ApiUnitConversionExists(apiUnitConversion.ConvertsFromUnitName))
        //         {
        //             return Conflict();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return CreatedAtAction("GetApiUnitConversion", new { id = apiUnitConversion.ConvertsFromUnitName }, apiUnitConversion);
        // }

        // /// <summary>
        // /// delete the unit with the given ids
        // /// </summary>
        // /// <remarks>
        // ///
        // /// Delete the given unit conversion
        // ///
        // /// Only an authenticated admin can make this request.
        // ///
        // /// The endpoint will perform an `delete from` command on the `unit conversion` table
        // /// </remarks>
        // /// <param name="id1">The Name of the Unit to converse to.</param>
        // /// <param name="id2">The Name of the Unit to converse from.</param>

        // [HttpDelete("{id1, id2}")]
        // public async Task<IActionResult> DeleteApiUnitConversion(string id1, string id2)
        // {
        //     var apiUnitConversion = _context.ApiUnitConversion.Find(id1, id2);
        //     if (apiUnitConversion == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.ApiUnitConversion.Remove(apiUnitConversion);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        private bool ApiUnitConversionExists(string id1)
        {
            return _context.ApiUnitConversion.Any(e => e.ConvertsFromUnitName == id1 || e.ConvertsToUnitName == id1);
        }
    }
}
