using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipEase.Shared.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Steps { get; set; }

        public double? Cholesterol { get; set; }

        public double? Fat { get; set; }

        public double? Sodium { get; set; }

        public double? Protein { get; set; }

        public double? Carbs { get; set; }

        public double? Calories { get; set; }

        [ForeignKey("Author")]
        public string AuthorId { get; set; }
        

        public Customer Author { get; set; }
        
        public ICollection<RecipeInCategory> Categories { get; set; }
        
        public ICollection<Uses> UsesIngredients { get; set; }
        
        public ICollection<RecipeRating> Ratings { get; set; }
    }
}