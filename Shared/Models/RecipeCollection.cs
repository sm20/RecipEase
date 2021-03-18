using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipEase.Shared.Models
{
    public enum Visibility {
        Private, Public
    }

    public class RecipeCollection
    {
        [ForeignKey("Customer")]
        public string UserId { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }

        [Required]
        [DefaultValue(Visibility.Private)]
        public Visibility Visibility { get; set; }
        

        public Customer Customer { get; set; }
        
        public ICollection<RecipeInCollection> Recipes { get; set; }
    }
}