using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RecipEase.Shared.Models;

namespace RecipEase.Server.Data
{
    public class RecipEaseContext : ApiAuthorizationDbContext<User>
    {
        public RecipEaseContext (
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<Supplier> Supplier { get; set; }

        public DbSet<Recipe> Recipe { get; set; }

        public DbSet<RecipeCategory> RecipeCategory { get; set; }

        public DbSet<RecipeCollection> RecipeCollection { get; set; }

        public DbSet<RecipeRating> RecipeRating { get; set; }

        public DbSet<RecipeInCategory> RecipeInCategory { get; set; }

        public DbSet<RecipeInCollection> RecipeInCollection { get; set; }

        public DbSet<Unit> Unit { get; set; }

        public DbSet<Ingredient> Ingredient { get; set; }

        public DbSet<ShoppingList> ShoppingList { get; set; }

        public DbSet<UnitConversion> UnitConversion { get; set; }

        public DbSet<Supplies> Supplies { get; set; }

        public DbSet<Uses> Uses { get; set; }

        public DbSet<IngrInShoppingList> IngrInShoppingList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // Ensure Identity tables are setup correctly
            base.OnModelCreating(modelBuilder);
            
            // Rename the generated user table to match our schema
            modelBuilder.Entity<User>().ToTable("User");

            // Configure composite keys
            modelBuilder.Entity<RecipeCollection>()
                .HasKey(e => new { e.User, e.Title });

            modelBuilder.Entity<RecipeRating>()
                .HasKey(e => new { e.User, e.Recipe });

            modelBuilder.Entity<RecipeInCollection>()
                .HasKey(e => new { e.Recipe, e.CollectionUser, e.CollectionTitle });

            modelBuilder.Entity<RecipeInCategory>()
                .HasKey(e => new { e.Recipe, e.Category });

            modelBuilder.Entity<UnitConversion>()
                .HasKey(e => new { e.ConvertsFromUnit, e.ConvertsToUnit });

            modelBuilder.Entity<Supplies>()
                .HasKey(e => new { e.User, e.IngrName, e.UnitName });

            modelBuilder.Entity<Uses>()
                .HasKey(e => new { e.IngrName, e.Recipe, e.UnitName });

            modelBuilder.Entity<IngrInShoppingList>()
                .HasKey(e => new { e.User, e.UnitName, e.IngrName });
        }
    }
}
