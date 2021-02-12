using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecipEase.Models;

namespace RecipEase.Data
{
    public static class DbInitializer
    {
        public static void Initialize(RecipEaseContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Example.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Example[]
            {
                new Example { ExampleProperty = "Hello" },
                new Example { ExampleProperty = "World!" },
            };

            context.Example.AddRange(students);
            context.SaveChanges();
        }
    }
}