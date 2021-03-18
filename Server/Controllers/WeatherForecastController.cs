using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipEase.Server.Data;
using RecipEase.Shared.Models;

namespace RecipEase.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly RecipEaseContext _context;

        public WeatherForecastController(RecipEaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns all WeatherForecasts.
        /// </summary>
        /// <remarks>
        ///
        /// Retrieves all items and all attributes from the `weatherforecasts`
        /// table.
        ///
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> GetWeatherForecasts()
        {
            return await _context.WeatherForecasts.ToListAsync();
        }

        /// <summary>
        /// Returns the WeatherForecast with the given id.
        /// </summary>
        /// <remarks>
        ///
        /// Retrieves the item with the given id value in the ID column from the
        /// `weatherforecasts` table.
        ///
        /// </remarks>
        /// <param name="id">The ID of the WeatherForecast to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherForecast>> GetWeatherForecast(int id)
        {
            var weatherForecast = await _context.WeatherForecasts.FindAsync(id);

            if (weatherForecast == null)
            {
                return NotFound();
            }

            return weatherForecast;
        }

        /// <summary>
        /// Adds the new forecast to the database.
        /// </summary>
        /// <remarks>
        ///
        /// Leave `id` out or specify a value of 0 to have it be automatically
        /// generated.
        ///
        /// </remarks>
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<WeatherForecast>> PostWeatherForecast(WeatherForecast weatherForecast)
        {
            _context.WeatherForecasts.Add(weatherForecast);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWeatherForecast), new { id = weatherForecast.ID }, weatherForecast);
        }

        /// <summary>
        /// Removes the forecast from the database.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <param name="id">The ID of the WeatherForecast to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeatherForecast(int id)
        {
            var weatherForecast = await _context.WeatherForecasts.FindAsync(id);
            if (weatherForecast == null)
            {
                return NotFound();
            }

            _context.WeatherForecasts.Remove(weatherForecast);
            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Replaces the forecast in the database.
        /// </summary>
        /// <remarks>
        /// 
        /// Ensure the `id` in the body matches the id of the request URL,
        /// otherwise a 400 response will be returned.
        ///
        /// </remarks>
        /// <param name="id">The ID of the WeatherForecast to update.</param>
        /// <param name="weatherForecast"></param>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutWeatherForecast(int id, WeatherForecast weatherForecast)
        {
            if (id != weatherForecast.ID)
            {
                return BadRequest();
            }

            _context.Entry(weatherForecast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeatherForecastExists(id))
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
        private bool WeatherForecastExists(int id)
        {
            return _context.WeatherForecasts.Any(e => e.ID == id);
        }
    }
}
