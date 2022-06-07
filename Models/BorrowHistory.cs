using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibApp.Models
{
    public class BorrowHistory
    {
        public int BorrowHistoryId { get; set; }

        [Required]
        [Display(Name = "Ksiązka")]
        public int BookId { get; set; }

        public Book Book { get; set; }

        [Required]
        [Display(Name = "Klient")]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        [Display(Name = "Data Wypożyczenia")]
        public DateTime BorrowDate { get; set; }

        [Display(Name = "Data Zwrotu")]
        public DateTime? ReturnDate { get; set; }
    }
}