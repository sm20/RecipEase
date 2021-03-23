using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipEase.Shared.Models.Api
{
    public class ApiRecipeInCollection
    {
        [Required]
        public int RecipeId { get; set; }

        [Required]
        public string CollectionUserId { get; set; }

        [Required]
        public string CollectionTitle { get; set; }
    }
}