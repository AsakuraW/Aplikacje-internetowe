using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibApp.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        public string Imie { get; set; }
        [Required]
        public string Nazwisko { get; set; }

        [Required]
        [Display(Name = "Numer kontaktowy")]
        public string Numer_kontaktowy { get; set; }
    }
}