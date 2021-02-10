using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipEase.Models;

namespace RecipEase.Data
{
    public class ExampleContext : DbContext
    {
        public ExampleContext (DbContextOptions<ExampleContext> options)
            : base(options)
        {
        }

        public DbSet<RecipEase.Models.Example> Example { get; set; }
    }
}
