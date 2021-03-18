using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipEase.Shared.Models
{
    public class RecipeRating
    {
        [ForeignKey("Customer")]
        public string UserId { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }
        

        public Customer Customer { get; set; }

        public Recipe Recipe { get; set; }
    }
}