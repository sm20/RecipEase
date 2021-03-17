using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class Supplier
    {
        [Key]
        public string Username { get; set; } // TODO: foreign key

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNo { get; set; }

        [Url]
        [Required]
        public string Website { get; set; }

        [Required]
        public string SupplierName { get; set; }

        [Range(0, Double.PositiveInfinity)]
        [DefaultValue(0)]
        public int StoreVisitCount { get; set; }
    }
}