using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models.Api
{
    public enum UnitType
    {
        Volume, Mass
    }

    public class ApiUnit
    {
        [Required]
        [Key]
        public string Name { get; set; }

        public UnitType UnitType { get; set; }

        public string Symbol { get; set; }
    }
}