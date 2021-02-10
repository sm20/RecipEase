using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AspDotNetMySqlTemplate.Models;

namespace AspDotNetMySqlTemplate.Data
{
    public class ExampleContext : DbContext
    {
        public ExampleContext (DbContextOptions<ExampleContext> options)
            : base(options)
        {
        }

        public DbSet<AspDotNetMySqlTemplate.Models.Example> Example { get; set; }
    }
}
