using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class Unit_Conversion
    {
        public string converts_to_unit { get; set; }
        public string converts_from_unit { get; set; }
        public int raio{ get; set; }

    }
}