using System;
using System.ComponentModel.DataAnnotations;

namespace RecipEase.Shared.Models.Api
{
    public enum UnitType
    {
        Volume, Mass
    }

    public class ApiUnit
    {
        [Required]
        [Key]
        public string Name { get; init; }

        public UnitType UnitType { get; init; }

        public string Symbol { get; init; }
    
        protected bool Equals(ApiUnit other)
        {
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ApiUnit) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}