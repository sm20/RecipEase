using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipEase.Shared.Models.Api
{
    public class ApiShoppingList
    {
        [Required]
        [Key]
        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }


        public DateTime LastUpdate { get; set; }

        public int NumIngredients { get; set; } // TODO: Calculated?

    }
}