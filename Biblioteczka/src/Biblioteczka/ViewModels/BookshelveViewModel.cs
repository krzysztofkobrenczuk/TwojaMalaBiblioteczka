using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteczka.ViewModels
{
    public class BookshelveViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    }
}
