using System;
using System.Collections.Generic;
using RecipEase.Shared.Models.Api;

namespace RecipEase.Shared.ApiResponses
{
    public class ShoppingListResponse
    {
        public ApiShoppingList _shoppingList { get; init; }
        
        public ApiShoppingList ShoppingList
        {
            get
            {
                if (Ingredients != null)
                {
                    _shoppingList.NumIngredients = Ingredients.Count;
                }

                return _shoppingList;
            }
        }

        public List<QuantifiedIngredient> Ingredients { get; set; }
    }
}