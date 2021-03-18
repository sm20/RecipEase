using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipEase.Shared.Models
{
    public class IngrInShoppingList
    {
        [ForeignKey("Customer")]
        public string UserId { get; set; }
        
        [ForeignKey("Unit")]
        public string UnitName { get; set; }
        
        [ForeignKey("Ingredient")]
        public string IngrName { get; set; }
        
        public int Quantity { get; set; }
        

        public Customer Customer { get; set; }

        public Unit Unit { get; set; }

        public Ingredient Ingredient { get; set; }
    }
}