using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class IngrInShoppingList
    {
        public string User { get; set; } // TODO: fk
        
        public string UnitName { get; set; } // TODO: fk
        
        public string IngrName { get; set; } // TODO: fk
        
        public int Quantity { get; set; }
    }
}