using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipEase.Shared.Models
{
    public class Uses
    {
        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }

        [ForeignKey("Unit")]
        public string UnitName { get; set; }

        [ForeignKey("Ingredient")]
        public string IngrName { get; set; } 

        public int Quantity { get; set; }
        //should be considered
        //  public int order{ get; set; } 


        public Recipe Recipe { get; set; }

        public Unit Unit { get; set; }

        public Ingredient Ingredient { get; set; }
    }
}