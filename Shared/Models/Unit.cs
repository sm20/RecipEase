using System;
using System.ComponentModel.DataAnnotations;
using RecipEase.Shared.Models.Api;

namespace RecipEase.Shared.Models
{
    public enum UnitType
    {
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

        public ApiUnit ToApiUnit()
        {
            return new()
            {
                Name = Name,
                UnitType = UnitType,
                Symbol = Symbol
            };
        }
    }
}