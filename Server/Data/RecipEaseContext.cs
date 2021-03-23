using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RecipEase.Shared.Models;
using RecipEase.Shared.Models.Api;

namespace RecipEase.Server.Data
{
    public class RecipEaseContext : ApiAuthorizationDbContext<User>
    {
        public RecipEaseContext(
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ensure Identity tables are setup correctly
            base.OnModelCreating(modelBuilder);

            // Rename the generated user table to match our schema
            modelBuilder.Entity<User>().ToTable("User");

            // Configure composite keys
            modelBuilder.Entity<RecipeCollection>()
                .HasKey(e => new { e.UserId, e.Title });

            modelBuilder.Entity<RecipeRating>()
                .HasKey(e => new { e.UserId, e.RecipeId });

            modelBuilder.Entity<RecipeInCollection>()
                .HasKey(e => new { e.RecipeId, e.CollectionUserId, e.CollectionTitle });

            modelBuilder.Entity<RecipeInCategory>()
                .HasKey(e => new { e.RecipeId, e.CategoryName });

            modelBuilder.Entity<UnitConversion>()
                .HasKey(e => new { e.ConvertsFromUnitName, e.ConvertsToUnitName });

            modelBuilder.Entity<Supplies>()
                .HasKey(e => new { e.UserId, e.IngrName, e.UnitName });

            modelBuilder.Entity<Uses>()
                .HasKey(e => new { e.IngrName, e.RecipeId, e.UnitName });

            modelBuilder.Entity<IngrInShoppingList>()
                .HasKey(e => new { e.UserId, e.UnitName, e.IngrName });

            // Configure composite foreign keys
            modelBuilder.Entity<RecipeInCollection>()
                .HasOne<RecipeCollection>(e => e.Collection)
                .WithMany(e => e.Recipes)
                .HasForeignKey(e => 
                    // Note that the order these are declared is important, they
                    // should line up with the order of the keys they're referencing
                    new { e.CollectionUserId, e.CollectionTitle });
        }

        public DbSet<RecipEase.Shared.Models.Api.ApiRecipe> ApiRecipe { get; set; }

        public DbSet<RecipEase.Shared.Models.Api.ApiUser> ApiUser { get; set; }
    }
}
