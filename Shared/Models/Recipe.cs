using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RecipEase.Shared.Models.Api;

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

        public ApiRecipe ToApiRecipe()
        {
            return new()
            {
                Id = Id,
                Name = Name,
                Steps = Steps,
                Cholesterol = Cholesterol,
                Fat = Fat,
                Sodium = Sodium,
                Protein = Protein,
                Carbs = Carbs,
                Calories = Calories,
                AuthorId = AuthorId,
                AverageRating = 5
            };
        }
    }
}