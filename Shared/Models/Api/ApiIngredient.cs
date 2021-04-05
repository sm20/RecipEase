using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RecipEase.Shared.ApiResponses;

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
        public string Name { get; init; }

        public Rarity? Rarity { get; init; }

        public double WeightToVolRatio { get; init; }

        private bool Equals(ApiIngredient other)
        {
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ApiIngredient)obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        public QuantifiedIngredient ToQuantifiedIngredient()
        {
            return new QuantifiedIngredient()
            {
                Quantity = 0,
                Ingredient = this,
                Unit = new ApiUnit()
                {
                    Name = "Grams",
                    Symbol = "g",
                    UnitType = UnitType.Mass
                }
            };
        }
    }
}