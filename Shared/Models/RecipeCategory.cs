using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class RecipeCategory
    {
        [Key]
        public string Name { get; set; }

        public int TotalInCatg { get; set; } // TODO: Calculated?

        public int AverageCaloriesCatg { get; set; } // TODO: Calculated?
    }
}