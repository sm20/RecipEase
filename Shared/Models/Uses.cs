using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models
{
    public class Uses
    {
        public int Recipe { get; set; } // TODO: fk

        public string UnitName { get; set; } // TODO: fk

        public string IngrName { get; set; } // TODO: fk

        public int Quantity { get; set; }
        //should be considered
        //  public int order{ get; set; } 
    }
}