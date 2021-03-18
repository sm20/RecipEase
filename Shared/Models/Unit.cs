using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public enum UnitType {
        Volume, Mass
    }

    public class Unit
    {
        [Key]
        public string Name { get; set; }

        [Required]
        public UnitType UnitType { get; set; }

        [Required]
        public string Symbol { get; set; }
    }
}