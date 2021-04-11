using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RecipEase.Shared;
using RecipEase.Shared.Models;

namespace RecipEase.Server.Data
{
    public class Users
    {
        public enum AccountType
        {
            Customer,
            Supplier
        }

        public static async Task<(User user, IdentityResult result)> CreateUser(UserManager<User> userManager,
            RecipEaseContext context,
            AccountType accountType, string email, string password, Supplier supplierInfo = null,
            Customer customerInfo = null)
        {
            var user = new User {UserName = email, Email = email};
            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded) return (user, result);
            switch (accountType)
            {
                case AccountType.Customer:
                    Customer customer;
                    if (customerInfo != null)
                    {
                        customer = customerInfo;
                        customer.UserId = user.Id;
                    }
                    else
                    {
                        customer = new Customer {UserId = user.Id};
                    }
                    await context.Customer.AddAsync(customer);
                    await userManager.AddToRoleAsync(user, Role.Customer.ToString());
                    await context.SaveChangesAsync();
                    break;
                case AccountType.Supplier:
                    Supplier supplier;
                    if (supplierInfo != null)
                    {
                        supplier = supplierInfo;
                        supplier.UserId = user.Id;
                    }
                    else
                    {
                        supplier = new Supplier {UserId = user.Id};
                    }
                    await context.Supplier.AddAsync(supplier);
                    await userManager.AddToRoleAsync(user, Role.Supplier.ToString());
                    await context.SaveChangesAsync();
                    break;
                default:
                    throw new Exception("Account type not provided.");
            }

            return (user, result);
        }
    }
}