using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecipEase.Server.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RecipEase.Shared.Models;

namespace RecipEase.Server.Data
{
    public class RecipEaseContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public RecipEaseContext (
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    }
}
