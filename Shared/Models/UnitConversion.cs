using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class UnitConversion
    {
        public string ConvertsToUnit { get; set; } // TODO: fk

        public string ConvertsFromUnit { get; set; } // TODO: fk

        public int Ratio { get; set; }
    }
}