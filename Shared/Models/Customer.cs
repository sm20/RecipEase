using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class Customer
    {
        public string username { get; set; }
        public string customer_name { get; set; }
        public int age { get; set; }
        public double weight { get; set; }
        public string fav_recipe { get; set; }
    }
}