using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class Recipe_Category
    {
        public string name { get; set; }
        public int total_in_catg { get; set; }
        public int average_calories_catg { get; set; }
    }
}