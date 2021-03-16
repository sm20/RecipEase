using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class Recipe_Rating
    {
        public string user { get; set; }
        public int recipe { get; set; }
        public int rating { get; set; }
    }
}