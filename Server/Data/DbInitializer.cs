using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecipEase.Shared.Models;

namespace RecipEase.Data
{
    public static class DbInitializer
    {
        public static void Initialize(RecipEaseContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.WeatherForecasts.Any())
            {
                return;   // DB has been seeded
            }

            var Summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };
            var rng = new Random();
            var weatherForecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray();

            context.WeatherForecasts.AddRange(weatherForecasts);
            context.SaveChanges();
        }
    }
}