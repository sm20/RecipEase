using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class Recipe_In_Collection
    {
        public int recipe { get; set; }
        public string collection_user { get; set; }
        public string collection_title { get; set; }
    }
}