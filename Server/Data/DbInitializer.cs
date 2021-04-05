using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RecipEase.Shared;
using RecipEase.Shared.Models;
using RecipEase.Shared.Models.Api;

namespace RecipEase.Server.Data
{
    public static class DbInitializer
    {
        private static string _testCustomerId;
        private static string _testSupplierId;
        private const string TestCustomerUsername = "c@c";
        private const string TestCustomerPassword = "c";
        private const string TestSupplierUsername = "s@s";
        private const string TestSupplierPassword = "s";

        public static async Task Initialize(RecipEaseContext context, UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await context.Database.EnsureCreatedAsync();

            await InitializeRoles(roleManager);
            await InitializeUsers(context, userManager);

            const string flour = "Flour";
            const string grams = "Grams";
            const string cups = "Cups";
            const string rice = "Rice";
            const string goatMilk = "Goat Milk";

            if (!context.Ingredient.Any())
            {
                var ingredients = new[]
                {
                    new Ingredient()
                    {
                        Name = flour,
                        Rarity = Rarity.Common,
                        WeightToVolRatio = 0.5
                    },
                    new Ingredient()
                    {
                        Name = rice,
                        Rarity = Rarity.Common,
                        WeightToVolRatio = 0.9
                    },
                    new Ingredient()
                    {
                        Name = goatMilk,
                        Rarity = Rarity.Rare,
                        WeightToVolRatio = 0.6
                    },
                };

                await context.Ingredient.AddRangeAsync(ingredients);
            }

            if (!context.Unit.Any())
            {
                var units = new[]
                {
                    new Unit()
                    {
                        Name = grams,
                        Symbol = "g",
                        UnitType = UnitType.Mass
                    },
                    new Unit()
                    {
                        Name = cups,
                        Symbol = "cups",
                        UnitType = UnitType.Volume
                    },
                };

                await context.Unit.AddRangeAsync(units);
            }

            if (!context.Supplies.Any())
            {
                var supplies = new[]
                {
                    new Supplies()
                    {
                        UserId = _testSupplierId,
                        Quantity = 5,
                        IngrName = flour,
                        UnitName = grams,
                    },
                    new Supplies()
                    {
                        UserId = _testSupplierId,
                        Quantity = 100,
                        IngrName = rice,
                        UnitName = grams,
                    },
                    new Supplies()
                    {
                        UserId = _testSupplierId,
                        Quantity = 10,
                        IngrName = goatMilk,
                        UnitName = cups,
                    }
                };

                await context.Supplies.AddRangeAsync(supplies);
            }

            await context.SaveChangesAsync();
        }

        private static async Task InitializeUsers(RecipEaseContext context, UserManager<User> userManager)
        {
            var (customer, _) = await Users.CreateUser(userManager, context, Users.AccountType.Customer,
                TestCustomerUsername, TestCustomerPassword);
            _testCustomerId = customer.Id;

            var (supplier, _) = await Users.CreateUser(userManager, context, Users.AccountType.Supplier,
                TestSupplierUsername, TestSupplierPassword);
            _testSupplierId = supplier.Id;
        }

        private static async Task InitializeRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Enum.GetNames(typeof(Role)))
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}