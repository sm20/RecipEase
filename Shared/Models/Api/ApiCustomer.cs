using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipEase.Shared.Models.Api
{

    public class ApiCustomer
    {
        [Key]
        public string UserId { get; set; }

        public string CustomerName { get; set; }

        public int? Age { get; set; }

        public double? Weight { get; set; }

        public Meal? FavMeal { get; set; }

    }
}