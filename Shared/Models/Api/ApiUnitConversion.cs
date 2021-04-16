using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipEase.Shared.Models.Api
{
    public class ApiUnitConversion
    {

        [Required]
        public string ConvertsToUnitName { get; set; }

        [Required]
        public string ConvertsFromUnitName { get; set; }
        [Required]
        public double Ratio { get; set; }
    }
}