using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipEase.Shared.Models.Api
{
    public class ApiUses
    {
        public int RecipeId { get; set; }

        public string UnitName { get; set; }

        public string IngrName { get; set; } 

        public int Quantity { get; set; }
        //should be considered
        //  public int order{ get; set; } 
    }
}