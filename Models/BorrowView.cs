using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibApp.Models
{
    public class BorrowView
    {
        public string Imie { get; set; }
        public string Tytul { get; set; }
        public Book Books { get; set; }

        public Customer Customers { get; set; }

        public BorrowHistory borrowHistories { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

    }
}