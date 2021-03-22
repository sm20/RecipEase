using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models.Api
{
    public enum Rarity
    {
        Common, Rare, VeryRare
    }

    public class ApiIngredient
    {
        [Key]
        [Required]
        public string Name { get; set; }

        public Rarity? Rarity { get; set; }

        public double WeightToVolRatio { get; set; }
    }
}