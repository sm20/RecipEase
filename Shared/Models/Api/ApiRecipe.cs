using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models.Api
{
    public class ApiRecipe
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Steps { get; set; }

        public double? Cholesterol { get; set; }

        public double? Fat { get; set; }

        public double? Sodium { get; set; }

        public double? Protein { get; set; }

        public double? Carbs { get; set; }

        public double? Calories { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public double? AverageRating { get; set; }
    }
}