using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipEase.Shared.Models.Api
{
    public class ApiIngrInShoppingList
    {
        [Required]
        public string UserId { get; set; }
        [Required]
         public string UnitName { get; set; }
        [Required]
        public string IngrName { get; set; }
        public int Quantity { get; set; }


    }
}