using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class Supplier
    {
        public string username { get; set; }
        public string email { get; set; }
        public string phone_no { get; set; }
        public string website { get; set; }
        public int store_visit_count { get; set; }
    }
}