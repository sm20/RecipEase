using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RecipEase.Shared.Models.Api;

namespace RecipEase.Shared.Models
{
    public class Ingredient
    {
        [Key]
        public string Name { get; set; }

        public Rarity? Rarity { get; set; }

        public double WeightToVolRatio { get; set; }
        

        public ICollection<Supplies> SuppliedBy { get; set; }
        
        public ApiIngredient ToApiIngredient()
        {
            return new()
            {
                Name = Name,
                Rarity = Rarity,
                WeightToVolRatio = WeightToVolRatio
            };
        }
    }
}