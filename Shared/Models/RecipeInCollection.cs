using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RecipEase.Shared.Models.Api;

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
        
        public ApiRecipeInCollection ToApiRecipeInCollection()
        {
            return new()
            {
                CollectionTitle = CollectionTitle,
                RecipeId = RecipeId,
                CollectionUserId = CollectionUserId
            };
        }
    }
}