using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipEase.Shared.Models.Api
{
    public class ApiSupplies
    {
        public string IngrName { get; set; }

        public string UnitName { get; set; }

        public string UserId { get; set; }

        public int Quantity { get; set; }
    }
}