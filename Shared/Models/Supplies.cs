    using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RecipEase.Shared.Models.Api;

namespace RecipEase.Shared.Models
{
    public class Supplies
    {
        [ForeignKey("Ingredient")]
        public string IngrName { get; set; }

        [ForeignKey("Unit")]
        public string UnitName { get; set; }

        [ForeignKey("Supplier")]
        public string UserId { get; set; }

        public double Quantity { get; set; }
        

        public Supplier Supplier { get; set; }
        public Ingredient Ingredient { get; set; }
        public Unit Unit { get; set; }

        public static Supplies FromApiSupplies(ApiSupplies apiSupplies)
        {
            return new()
            {
                Quantity = apiSupplies.Quantity,
                IngrName = apiSupplies.IngrName,
                UserId = apiSupplies.UserId,
                UnitName = apiSupplies.UnitName
            };
        }
    }
}