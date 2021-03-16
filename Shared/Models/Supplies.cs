using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class Supplies
    {
        public string ingr_name { get; set; }
        public string unit_name { get; set; }
        public string user { get; set; }
        public int quantity { get; set; }
    }
}