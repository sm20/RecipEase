using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace RecipEase.Shared.Models
{
    public class User : IdentityUser
    {
        [DefaultValue(0)]
        public int LoginCount { get; set; }
    }
}