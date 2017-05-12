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
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        //[Required]
        public string Author { get; set; }

        public int Pages { get; set; }

        [StringLength(4096)]
        public string Description { get; set;  }

        [Required]
        public DateTime DateStarted { get; set; }

    }
}
