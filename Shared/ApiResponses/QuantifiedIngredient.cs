using System;
using RecipEase.Shared.Models.Api;

namespace RecipEase.Shared.ApiResponses
{
    public class QuantifiedIngredient
    {
        public ApiIngredient Ingredient { get; init; }

        public ApiUnit Unit { get; set; }

        public double Quantity { get; set; }

        public QuantifiedIngredient GetCopy()
        {
            return new()
            {
                Ingredient = new ApiIngredient()
                {
                    Name = Ingredient.Name,
                    Rarity = Ingredient.Rarity,
                    WeightToVolRatio = Ingredient.WeightToVolRatio
                },
                Quantity = Quantity,
                Unit = new ApiUnit()
                {
                    Name = Unit.Name,
                    Symbol = Unit.Symbol,
                    UnitType = Unit.UnitType
                }
            };
        }

        public ApiSupplies ToApiSupplies(string userId)
        {
            return new()
            {
                Quantity = Quantity,
                IngrName = Ingredient.Name,
                UnitName = Unit.Name,
                UserId = userId
            };
        }

        public ApiIngrInShoppingList ToApiIngrInShoppingList(string userId)
        {
            return new()
            {
                Quantity = Quantity,
                IngrName = Ingredient.Name,
                UnitName = Unit.Name,
                UserId = userId
            };
        }
        
        protected bool Equals(QuantifiedIngredient other)
        {
            return Equals(Ingredient, other.Ingredient) && Equals(Unit, other.Unit) && Quantity == other.Quantity;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((QuantifiedIngredient) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Ingredient, Unit, Quantity);
        }
    }
}