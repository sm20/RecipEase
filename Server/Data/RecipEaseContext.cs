using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipEase.Shared.Models;

namespace RecipEase.Data
{
    public class RecipEaseContext : DbContext
    {
        public RecipEaseContext (DbContextOptions<RecipEaseContext> options)
            : base(options)
        {
        }

        public DbSet<RecipEase.Shared.Models.WeatherForecast> WeatherForecasts { get; set; }
    }
}
