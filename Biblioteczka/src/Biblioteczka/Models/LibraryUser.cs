using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteczka.Models
{
    public class LibraryUser : IdentityUser
    {
        public DateTime FirstBookshelve { get; set; }
    }
}
