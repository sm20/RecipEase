using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public enum Visibility {
        Private, Public
    }

    public class RecipeCollection
    {
        public string User { get; set; } // TODO: fk

        public string Title { get; set; }
        
        public string Description { get; set; }

        [Required]
        [DefaultValue(Visibility.Private)]
        public Visibility Visibility { get; set; }
    }
}