using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace LibApp.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required]
        public string Tytul { get; set; }

        [Required]
        [Display(Name = "Numer seryjny")]
        public string NrSeryjny { get; set; }

        public string Autor { get; set; }

        public string Wydawnictwo { get; set; }
        public ICollection<BorrowHistory> BorrowHistories { get; set; }
    }
}