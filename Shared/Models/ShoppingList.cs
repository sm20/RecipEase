using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class ShoppingList
    {
        public string Name { get; set; }

        [Key]
        public string User { get; set; } // TODO: fk

        public DateTime LastUpdate { get; set; }

        public int NumIngredients { get; set; } // TODO: Calculated?
    }
}