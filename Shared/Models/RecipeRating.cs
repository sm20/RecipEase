using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class RecipeRating
    {
        public string User { get; set; } // TODO: fk

        public int Recipe { get; set; } // TODO: fk

        [Range(1, 5)]
        public int Rating { get; set; }
    }
}