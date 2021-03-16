using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class Shopping_List
    {
        public string name { get; set; }
        public string user { get; set; }
        public DateTime last_update { get; set; }
        public int num_ingredients { get; set; }
    }
}