using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteczka.ViewModels
{
    public class BookViewModel
    {

        public int BookId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        public string Author { get; set; }

        public int Pages { get; set; }

        [StringLength(4096)]
        public string Description { get; set;  }

        public DateTime DateStarted { get; set; }
        public bool Finished { get; set; }

    }
}
