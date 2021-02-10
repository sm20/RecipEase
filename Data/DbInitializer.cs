using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AspDotNetMySqlTemplate.Models;

namespace AspDotNetMySqlTemplate.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ExampleContext context)
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