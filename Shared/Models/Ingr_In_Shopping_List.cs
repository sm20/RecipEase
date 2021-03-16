using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class Ingr_In_Shopping_List
    {
        public string user { get; set; }
        public string unit_name { get; set; }
        public string ingr_name { get; set; }
        public int quantity { get; set; }
    }
}