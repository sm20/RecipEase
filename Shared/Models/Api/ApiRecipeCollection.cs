using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipEase.Shared.Models.Api
{
    public class ApiRecipeCollection
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Title { get; set; }
        
        public string Description { get; set; }

        [Required]
        [DefaultValue(Visibility.Private)]
        public Visibility Visibility { get; set; }
    }
}