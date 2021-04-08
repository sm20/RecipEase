using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecipEase.Shared;
using RecipEase.Shared.Models;
using RecipEase.Shared.Models.Api;
using Visibility = RecipEase.Shared.Models.Visibility;

namespace RecipEase.Server.Data
{
    public static class DbInitializer
    {
        private static string _testCustomerId;
        private static string _testCustomerId2;
        private static string _testCustomerId3;
        private static string _testSupplierId;
        private static int _recipe0Id;
        private static int _recipe1Id;
        private static int _recipe2Id;
        private static string _recColl1Title = "Favourite breakfasts";
        private static string _recColl2Title = "Favourite seafoods";
        private const string TestCustomerUsername = "c@c";
        private const string TestCustomerPassword = "c";
        private const string TestSupplierUsername = "s@s";
        private const string TestSupplierPassword = "s";
        private const string Flour = "Flour";
        private const string Grams = "Grams";
        private const string Cups = "Cups";
        private const string Rice = "Rice";
        private const string GoatMilk = "Goat Milk";

        public static async Task Initialize(RecipEaseContext context, UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await context.Database.EnsureCreatedAsync();

            await InitializeRoles(roleManager);
            await InitializeUsers(context, userManager);

            await InitializeIngredients(context);
            await InitializeUnits(context);
            await InitializeSupplies(context);

            await InitializeRecipes(context);
            await InitializeRecipeRatings(context);
            await InitializeRecipeCollections(context);
            await InitializeUses(context);
            await InitializeRecipeCategories(context);

            await context.SaveChangesAsync();
        }

        private static async Task InitializeRecipes(RecipEaseContext context)
        {
            if (!context.Recipe.Any())
            {
                var recipes = new[]
                {
                    new Recipe
                    {
                        Name = "Mac N' Cheese",
                        AuthorId = _testCustomerId,
                        Calories = 1,
                        Carbs = 2,
                        Cholesterol = 3,
                        Fat = 4,
                        Protein = 5,
                        Sodium = 6,
                        Steps = "The steps to make the recipe..."
                    },
                    new Recipe
                    {
                        Name = "Pork Cutlets",
                        AuthorId = _testCustomerId,
                        Calories = 1,
                        Carbs = 2,
                        Cholesterol = 3,
                        Fat = 4,
                        Protein = 5,
                        Sodium = 6,
                        Steps = "The steps to make the recipe..."
                    },
                    new Recipe
                    {
                        Name = "Sushi",
                        AuthorId = _testCustomerId2,
                        Calories = 1,
                        Carbs = 2,
                        Cholesterol = 3,
                        Fat = 4,
                        Protein = 5,
                        Sodium = 6,
                        Steps = "The steps to make the recipe..."
                    },
                };

                await context.Recipe.AddRangeAsync(recipes);
                await context.SaveChangesAsync();
            }

            var allRecipes = await context.Recipe.ToListAsync();
            if (allRecipes.Count == 3)
            {
                _recipe0Id = allRecipes[0].Id;
                _recipe1Id = allRecipes[1].Id;
                _recipe2Id = allRecipes[2].Id;
            }
        }

        private static async Task InitializeRecipeCategories(RecipEaseContext context)
        {
            var breakfast = "Breakfast";
            var lunch = "Lunch";
            var dinner = "Dinner";
            if (!context.RecipeCategory.Any())
            {
                var categories = new[]
                {
                    new RecipeCategory
                    {
                        Name = breakfast,
                        AverageCaloriesCatg = 500,
                        TotalInCatg = 1,
                    },
                    new RecipeCategory
                    {
                        Name = lunch,
                        AverageCaloriesCatg = 600,
                        TotalInCatg = 1
                    },
                    new RecipeCategory
                    {
                        Name = dinner,
                        AverageCaloriesCatg = 700,
                        TotalInCatg = 1,
                    },
                };

                await context.RecipeCategory.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            if (!context.RecipeInCategory.Any())
            {
                var recipeInCategories = new[]
                {
                    new RecipeInCategory
                    {
                        RecipeId = _recipe0Id,
                        CategoryName = breakfast
                    },
                    new RecipeInCategory
                    {
                        RecipeId = _recipe1Id,
                        CategoryName = lunch
                    },
                    new RecipeInCategory
                    {
                        RecipeId = _recipe2Id,
                        CategoryName = dinner
                    },
                };

                await context.RecipeInCategory.AddRangeAsync(recipeInCategories);
                await context.SaveChangesAsync();
            }
        }

        private static async Task InitializeUses(RecipEaseContext context)
        {
            if (!context.Uses.Any())
            {
                var uses = new[]
                {
                    new Uses
                    {
                        RecipeId = _recipe0Id,
                        IngrName = Flour,
                        UnitName = Grams,
                        Quantity = 5
                    },
                    new Uses
                    {
                        RecipeId = _recipe1Id,
                        IngrName = Flour,
                        UnitName = Grams,
                        Quantity = 100
                    },
                    new Uses
                    {
                        RecipeId = _recipe2Id,
                        IngrName = Flour,
                        UnitName = Grams,
                        Quantity = 6
                    },
                    new Uses
                    {
                        RecipeId = _recipe0Id,
                        IngrName = GoatMilk,
                        UnitName = Cups,
                        Quantity = 10
                    },
                    new Uses
                    {
                        RecipeId = _recipe1Id,
                        IngrName = Rice,
                        UnitName = Cups,
                        Quantity = 4
                    },
                    new Uses
                    {
                        RecipeId = _recipe2Id,
                        IngrName = Rice,
                        UnitName = Grams,
                        Quantity = 200
                    },
                };

                await context.Uses.AddRangeAsync(uses);
                await context.SaveChangesAsync();
            }
        }

        private static async Task InitializeRecipeCollections(RecipEaseContext context)
        {
            if (!context.RecipeCollection.Any())
            {
                var recipeCollections = new[]
                {
                    new RecipeCollection
                    {
                        UserId = _testCustomerId,
                        Title = _recColl1Title,
                        Description = "All my favourite breakfast recipes",
                        Visibility = Visibility.Public
                    },
                    new RecipeCollection
                    {
                        UserId = _testCustomerId,
                        Title = _recColl2Title,
                        Description = "All my favourite seafood recipes",
                        Visibility = Visibility.Public
                    },
                };

                var recipeInCollections = new[]
                {
                    new RecipeInCollection
                    {
                        RecipeId = _recipe0Id,
                        CollectionTitle = _recColl1Title,
                        CollectionUserId = _testCustomerId,
                    },
                    new RecipeInCollection
                    {
                        RecipeId = _recipe1Id,
                        CollectionTitle = _recColl1Title,
                        CollectionUserId = _testCustomerId,
                    },
                    new RecipeInCollection
                    {
                        RecipeId = _recipe1Id,
                        CollectionTitle = _recColl2Title,
                        CollectionUserId = _testCustomerId,
                    },
                    new RecipeInCollection
                    {
                        RecipeId = _recipe2Id,
                        CollectionTitle = _recColl2Title,
                        CollectionUserId = _testCustomerId,
                    },
                };

                await context.RecipeCollection.AddRangeAsync(recipeCollections);
                await context.RecipeInCollection.AddRangeAsync(recipeInCollections);
                await context.SaveChangesAsync();
            }
        }

        private static async Task InitializeRecipeRatings(RecipEaseContext context)
        {
            if (!context.RecipeRating.Any())
            {
                var recipeRatings = new[]
                {
                    // new RecipeRating
                    // {
                    //     Rating = 5,
                    //     RecipeId = _recipe0Id,
                    //     UserId = _testCustomerId
                    // },
                    new RecipeRating
                    {
                        Rating = 4,
                        RecipeId = _recipe1Id,
                        UserId = _testCustomerId
                    },
                    new RecipeRating
                    {
                        Rating = 3,
                        RecipeId = _recipe2Id,
                        UserId = _testCustomerId
                    },
                    new RecipeRating
                    {
                        Rating = 2,
                        RecipeId = _recipe0Id,
                        UserId = _testCustomerId2
                    },
                    new RecipeRating
                    {
                        Rating = 1,
                        RecipeId = _recipe1Id,
                        UserId = _testCustomerId2
                    },
                    new RecipeRating
                    {
                        Rating = 5,
                        RecipeId = _recipe2Id,
                        UserId = _testCustomerId2
                    },
                    new RecipeRating
                    {
                        Rating = 4,
                        RecipeId = _recipe0Id,
                        UserId = _testCustomerId3
                    },
                    new RecipeRating
                    {
                        Rating = 3,
                        RecipeId = _recipe1Id,
                        UserId = _testCustomerId3
                    },
                    new RecipeRating
                    {
                        Rating = 2,
                        RecipeId = _recipe2Id,
                        UserId = _testCustomerId3
                    },
                };

                await context.RecipeRating.AddRangeAsync(recipeRatings);
            }
        }

        private static async Task InitializeSupplies(RecipEaseContext context)
        {
            if (!context.Supplies.Any())
            {
                var supplies = new[]
                {
                    new Supplies()
                    {
                        UserId = _testSupplierId,
                        Quantity = 5,
                        IngrName = Flour,
                        UnitName = Grams,
                    },
                    new Supplies()
                    {
                        UserId = _testSupplierId,
                        Quantity = 100,
                        IngrName = Rice,
                        UnitName = Grams,
                    },
                    new Supplies()
                    {
                        UserId = _testSupplierId,
                        Quantity = 10,
                        IngrName = GoatMilk,
                        UnitName = Cups,
                    }
                };

                await context.Supplies.AddRangeAsync(supplies);
                await context.SaveChangesAsync();
            }
        }

        private static async Task InitializeUnits(RecipEaseContext context)
        {
            if (!context.Unit.Any())
            {
                var units = new[]
                {
                    new Unit()
                    {
                        Name = Grams,
                        Symbol = "g",
                        UnitType = UnitType.Mass
                    },
                    new Unit()
                    {
                        Name = Cups,
                        Symbol = "cups",
                        UnitType = UnitType.Volume
                    },
                };

                await context.Unit.AddRangeAsync(units);
                await context.SaveChangesAsync();
            }
        }

        private static async Task InitializeIngredients(RecipEaseContext context)
        {
            if (!context.Ingredient.Any())
            {
                var ingredients = new[]
                {
                    new Ingredient()
                    {
                        Name = Flour,
                        Rarity = Rarity.Common,
                        WeightToVolRatio = 0.5
                    },
                    new Ingredient()
                    {
                        Name = Rice,
                        Rarity = Rarity.Common,
                        WeightToVolRatio = 0.9
                    },
                    new Ingredient()
                    {
                        Name = GoatMilk,
                        Rarity = Rarity.Rare,
                        WeightToVolRatio = 0.6
                    },
                };

                await context.Ingredient.AddRangeAsync(ingredients);
            }
        }

        private static async Task InitializeUsers(RecipEaseContext context, UserManager<User> userManager)
        {
            var existingCustomer = await context.Customer.FirstOrDefaultAsync();
            if (existingCustomer == null)
            {
                var customer1Info = new Customer()
                {
                    CustomerName = "Calum Sieppert",
                    Age = 20,
                    Weight = 160,
                    FavMeal = Meal.Lunch,
                };
                var (customer, _) = await
                    Users.CreateUser(userManager, context, Users.AccountType.Customer,
                        TestCustomerUsername, TestCustomerPassword, customerInfo: customer1Info);
                _testCustomerId = customer.Id;

                var customer2Info = new Customer()
                {
                    CustomerName = "Sajid Choudhry",
                    Age = 20,
                    Weight = 160,
                    FavMeal = Meal.Dinner,
                };
                (customer, _) = await
                    Users.CreateUser(userManager, context, Users.AccountType.Customer,
                        "c2@c2", "c2", customerInfo: customer2Info);
                _testCustomerId2 = customer.Id;

                var customer3Info = new Customer()
                {
                    CustomerName = "Khoa Nguyen Tran Dang",
                    Age = 20,
                    Weight = 160,
                    FavMeal = Meal.Breakfast,
                };
                (customer, _) = await
                    Users.CreateUser(userManager, context, Users.AccountType.Customer,
                        "c3@c3", "c3", customerInfo: customer3Info);
                _testCustomerId3 = customer.Id;
            }
            else
            {
                _testCustomerId = existingCustomer.UserId;
            }

            var existingSupplier = await context.Supplier.FirstOrDefaultAsync();
            if (existingSupplier == null)
            {
                var supplierInfo = new Supplier()
                {
                    Email = "business@business.com",
                    Website = "https://example.com",
                    PhoneNo = "123-456-7890",
                    SupplierName = "A Business",
                    StoreVisitCount = 3
                };
                var (supplier, _) = await Users.CreateUser(userManager, context, Users.AccountType.Supplier,
                    TestSupplierUsername, TestSupplierPassword, supplierInfo: supplierInfo);
                _testSupplierId = supplier.Id;
            }
            else
            {
                _testSupplierId = existingSupplier.UserId;
            }
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