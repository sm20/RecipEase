using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RecipEase.Shared.Models
{
    public class WeatherForecast
    {
        public int ID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        [MaxLength(20)]
        public string Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public double Humidity { get; set; }
    }
}
