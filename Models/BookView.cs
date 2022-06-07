using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibApp.Models
{
    public class BookView
    {
        public int BookId { get; set; }

        public string Tytul { get; set; }

        [Display(Name = "Serial Number")]
        public string NrSeryjny { get; set; }

        public string Autor { get; set; }

        public string Wydawnictwo { get; set; }
        [Display(Name = "Dostępność")]
        public bool IsAvailable { get; set; }
    }
}