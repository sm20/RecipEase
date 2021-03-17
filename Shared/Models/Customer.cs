using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public enum Meal {
        Breakfast, Lunch, Dinner
    }

    public class Customer
    {
        [Key]
        public string Username { get; set; } // TODO: foreign key

        public string CustomerName { get; set; }

        public int? Age { get; set; }

        public double? Weight { get; set; }

        public Meal? FavMeal { get; set; }
    }
}