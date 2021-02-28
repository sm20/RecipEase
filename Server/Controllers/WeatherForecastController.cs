using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipEase.Data;
using RecipEase.Shared.Models;

namespace RecipEase.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private RecipEaseContext context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, RecipEaseContext context)
        {
            this.context = context;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var query = from wf in this.context.WeatherForecasts select wf;
            return query.AsEnumerable();
        }
        
        [HttpPost]
        public IEnumerable<WeatherForecast> Post(WeatherForecast wf)
        {
            this.context.WeatherForecasts.Add(wf);
            this.context.SaveChanges();
            var query = from w in this.context.WeatherForecasts select w;
            return query.AsEnumerable();
        }
    }
}
