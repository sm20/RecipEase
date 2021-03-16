using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class User
    {
        [Key]
        [MaxLength(20)]
        [Required]
        public string username { get; set; }

        [MaxLength(64)]
        [Required]
        public string pword_hash { get; set; }

        public int login_count { get; set; }
    }
}