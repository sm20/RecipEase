using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipEase.Shared.Models
{
    public class ShoppingList
    {
        public string Name { get; set; }

        [Key]
        [ForeignKey("Customer")]
        public string UserId { get; set; }

        public DateTime LastUpdate { get; set; }

        public int NumIngredients { get; set; } // TODO: Calculated?
        

        public Customer Customer { get; set; }
        
        public ICollection<IngrInShoppingList> Ingredients { get; set; }
    }
}