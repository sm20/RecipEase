using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipEase.Shared.Models
{
    public class RecipeInCollection
    {
        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }

        public string CollectionUserId { get; set; }

        public string CollectionTitle { get; set; }
        

        public Recipe Recipe { get; set; }

        public RecipeCollection Collection { get; set; }
    }
}