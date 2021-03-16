using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class Uses
    {
        public int recipe { get; set; }
        public string unit_name { get; set; }
        public string ingr_name { get; set; }
        public int quantity { get; set; }
        //  public int order{ get; set; }
    }
}