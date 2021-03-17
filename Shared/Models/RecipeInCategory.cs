using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class RecipeInCategory
    {
        public int Recipe { get; set; } // TODO: fk

        public string Category { get; set; } // TODO: fk
    }
}