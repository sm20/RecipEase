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
    [ApiController]
    public class UnitConversionController : ControllerBase
    {
        private readonly RecipEaseContext _context;

        public UnitConversionController(RecipEaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all unit conversion
        /// </summary>
        /// <remarks>
        ///
        /// functionalities : retrieve all unitconversion in UnitConversion
        /// 
        /// database: UnitConversion
        /// 
        /// constraints: no constraints
        /// 
        /// query: select * from UnitConversion
        /// 
        /// </remarks>
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<ApiUnitConversion>>> GetApiUnitConversion()
        {
            return await _context.ApiUnitConversion.ToListAsync();
        }

        /// <summary>
        /// Get unit conversion from one unit to another
        /// </summary>
        /// <remarks>
        ///
        /// functionalities : retrieve a unitconversion matching the given keys.
        /// 
        /// database: UnitConversion
        /// 
        /// query: select * from UnitConversion where ConvertsToUnitName = id1 and 
        /// ConvertsFromUnitName = id2
        /// 
        /// </remarks>
        /// <param name="convertsToUnitName">A unit name to find conversions to.</param>
        /// <param name="convertsFromUnitName">A unit name to find conversions from.</param>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ApiUnitConversion>> GetApiUnitConversion(string convertsToUnitName,
            string convertsFromUnitName)
        {
            var unitConversion = await _context.UnitConversion.FindAsync(convertsToUnitName, convertsFromUnitName);
            double ratio;

            if (unitConversion == null)
            {
                // Try the opposite direction
                unitConversion = await _context.UnitConversion.FindAsync(convertsFromUnitName, convertsToUnitName);
                if (unitConversion == null)
                {
                    return NotFound();
                }

                ratio = 1 / unitConversion.Ratio;
            }
            else
            {
                ratio = unitConversion.Ratio;
            }

            return new ApiUnitConversion
            {
                Ratio = ratio,
                ConvertsFromUnitName = unitConversion.ConvertsFromUnitName,
                ConvertsToUnitName = unitConversion.ConvertsToUnitName
            };
        }


        private bool ApiUnitConversionExists(string id1)
        {
            return _context.ApiUnitConversion.Any(e => e.ConvertsFromUnitName == id1 || e.ConvertsToUnitName == id1);
        }
    }
}