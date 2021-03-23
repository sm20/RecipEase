using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipEase.Shared.Models.Api
{
    public class ApiSupplies
    {
        [Required]
        public string IngrName { get; set; }

        [Required]
        public string UnitName { get; set; }

        [Required]
        public string UserId { get; set; }

        public int Quantity { get; set; }
    }
}