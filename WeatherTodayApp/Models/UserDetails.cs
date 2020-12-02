using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WeatherTodayApp.Models
{
    
    public class UserDetails
    {
        public int User_ID { get; set; }

        [DisplayName("First Name")]
        [Required]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only Characters Are Allowed")]
        public string User_First_Name { get; set; }

        [DisplayName("Last Name")]
        [Required]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only Characters Are Allowed")]
        public string User_Last_Name { get; set; }

        [DisplayName("Email Address")]
        [Required]
        [EmailAddress]
        public string User_Emaild { get; set; }

        [DisplayName("Password")]
        [Required]
        [DataType(DataType.Password)]
        public string User_Password { get; set; }
    }
}