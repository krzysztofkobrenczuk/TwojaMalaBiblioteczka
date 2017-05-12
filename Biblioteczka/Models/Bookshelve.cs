using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteczka.Models
{
    public class Bookshelve
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserName { get; set; }

        //Member which hold each stops on our trip   -> ICollection of stop entities
        public ICollection<Book> Books { get; set; }
    }
}
