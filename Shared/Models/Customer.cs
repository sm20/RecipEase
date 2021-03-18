using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipEase.Shared.Models
{
    public enum Meal {
        Breakfast, Lunch, Dinner
    }

    public class Customer
    {
        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public string CustomerName { get; set; }

        public int? Age { get; set; }

        public double? Weight { get; set; }

        public Meal? FavMeal { get; set; }


        public User User { get; set; }
    }
}