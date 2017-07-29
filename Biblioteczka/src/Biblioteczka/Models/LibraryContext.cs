using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Biblioteczka.Models
{
    public class LibraryContext : IdentityDbContext<LibraryUser>
    {
        private IConfigurationRoot _config;

        public LibraryContext(IConfigurationRoot config, DbContextOptions options) : base(options)
        {
            _config = config;

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Bookshelve> Bookshalves { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["ConnectionStrings:LibraryContextConnection"]);
        }

    }
}
