using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace RecipEase.Shared.Models.Api
{
    public class ApiUser : IdentityUser
    {
        [DefaultValue(0)]
        public int LoginCount { get; set; }
    }
}