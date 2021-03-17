using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class RecipeInCollection
    {
        public int Recipe { get; set; } // TODO: fk

        public string CollectionUser { get; set; } // TODO: fk

        public string CollectionTitle { get; set; } // TODO: fk
    }
}