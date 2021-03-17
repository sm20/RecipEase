using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class Recipe
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Steps { get; set; }

        public double? Cholesterol { get; set; }

        public double? Fat { get; set; }

        public double? Sodium { get; set; }

        public double? Protein { get; set; }

        public double? Carbs { get; set; }

        public double? Calories { get; set; }

        public string Author { get; set; } // TODO: fk
    }
}