using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class Recipe_Collection
    {
        public string user { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string visibility { get; set; }
    }
}