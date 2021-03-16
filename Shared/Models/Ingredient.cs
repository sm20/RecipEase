using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class Ingredient
    {
        public string name { get; set; }
        public string rarity { get; set; }
        public int weight_to_vol_ratio { get; set; }
    }
}