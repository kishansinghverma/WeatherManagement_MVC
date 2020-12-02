using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeatherTodayApp.Models
{
    public class WeatherDetails
    {
        public int CityID { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only Characters Are Allowed")]
        [DisplayName("City Name")]
        public string CityName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only Characters Are Allowed")]
        [DisplayName("Country Name")]
        public string CountryName { get; set; }

        [Required]
        [Range(1, 49)]
        [RegularExpression("([.0-9]+)", ErrorMessage ="Match Required Format")]
        [DisplayName("Temperature")]
        public decimal Temperature { get; set; }

        [Required]
        [Range(41, 69)]
        [RegularExpression("([0-9]+)", ErrorMessage = "Match Required Format")]
        [DisplayName("Humidity")]
        public int Humidity { get; set; }

        [Required]
        [Range(3, 19)]
        [RegularExpression("([0-9]+)", ErrorMessage = "Match Required Format")]
        [DisplayName("Visibility")]
        public int Visibility { get; set; }
    }
}