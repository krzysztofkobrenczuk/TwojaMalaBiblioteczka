using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteczka.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }
        public int Pages { get; set; }
        public string Description { get; set; }
        public DateTime DateStarted { get; set; }
       

    }
}
