using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class User
    {
        [Key]
        [MaxLength(20)]
        [Required]
        public string Username { get; set; } // TODO: Will be added by authentication system

        [MaxLength(64)]
        [Required]
        public string PwordHash { get; set; } // TODO: Will be added by authentication system

        [DefaultValue(0)]
        public int LoginCount { get; set; }
    }
}