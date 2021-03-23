using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipEase.Shared.Models.Api
{
    public class ApiUses
    {
        [Required]
        public int RecipeId { get; set; }

        [Required]
        public string UnitName { get; set; }

        [Required]
        public string IngrName { get; set; } 

        public int Quantity { get; set; }
        //should be considered
        //  public int order{ get; set; } 
    }
}