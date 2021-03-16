using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class Recipe_In_Category
    {
        public int recipe { get; set; }
        public string category { get; set; }
    }
}