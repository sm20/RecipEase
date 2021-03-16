using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class Unit
    {
        public string name { get; set; }
        public string unit_type { get; set; }
        public string symbol { get; set; }
    }
}