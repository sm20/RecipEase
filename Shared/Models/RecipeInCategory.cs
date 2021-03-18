using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipEase.Shared.Models
{
    public class RecipeInCategory
    {
        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }

        [ForeignKey("Category")]
        public string CategoryName { get; set; }
        

        public Recipe Recipe { get; set; }

        public RecipeCategory Category { get; set; }
    }
}