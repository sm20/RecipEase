using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipEase.Shared.Models.Api
{
    public class ApiSupplier
    {
        [Key]
        public string UserId { get; set; }


        public string Email { get; set; }


        public string PhoneNo { get; set; }


        [Required]
        public string Website { get; set; }

        [Required]
        public string SupplierName { get; set; }

        [Range(0, Double.PositiveInfinity)]
        [DefaultValue(0)]
        public int StoreVisitCount { get; set; }
    }
}