using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RecipEase.Shared.Models.Api;

namespace RecipEase.Shared.Models
{
    public class Supplier
    {
        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNo { get; set; }

        [Url]
        public string Website { get; set; }

        public string SupplierName { get; set; }

        [Range(0, Double.PositiveInfinity)]
        [DefaultValue(0)]
        public int StoreVisitCount { get; set; }


        public User User { get; set; }

        public ApiSupplier ToApiSupplier()
        {
            return new()
            {
                Email = Email,
                Website = Website,
                PhoneNo = PhoneNo,
                SupplierName = SupplierName,
                UserId = UserId,
                StoreVisitCount = StoreVisitCount
            };
        }
    }
}