using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class Recipe
    {
        public string recipe_id { get; set; }
        public string name { get; set; }
        public string steps { get; set; }
        public int cholesterol { get; set; }
        public int fat { get; set; }
        public int sodium { get; set; }
        public int protein { get; set; }
        public int carbs { get; set; }
        public int calories { get; set; }
        public string author { get; set; }
    }
}