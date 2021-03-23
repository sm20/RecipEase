using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipEase.Shared.Models.Api
{
    public class ApiRecipeRating
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int RecipeId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }
    }
}