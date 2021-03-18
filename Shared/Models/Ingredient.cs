using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public enum Rarity {
        Common, Rare, VeryRare
    }

    public class Ingredient
    {
        [Key]
        public string Name { get; set; }

        public Rarity? Rarity { get; set; }

        public double WeightToVolRatio { get; set; }
        

        public ICollection<Supplies> SuppliedBy { get; set; }
    }
}