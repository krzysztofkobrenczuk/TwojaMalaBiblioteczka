using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteczka.Models
{
    public class LibraryContextSeedData
    {
        private LibraryContext _context;
        private UserManager<LibraryUser> _userManager;

        public LibraryContextSeedData(LibraryContext context, UserManager<LibraryUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task EnsureSeedData()
        {

            if(await _userManager.FindByEmailAsync("user@email.com") == null)
            {
                var user = new LibraryUser()
                {
                    UserName = "user",
                    Email = "user@email.com"
                };
                await _userManager.CreateAsync(user, "P@ssw0rd!");
            }


            if (!_context.Bookshalves.Any())
            {
                var myShelve1 = new Bookshelve()
                {
                    Name = "Example Bookshelf",
                    DateCreated = DateTime.UtcNow,
                    UserName = "user",
                    Books = new List<Book>
                    {
                        new Book() { Name = "Example Book", Author = "Example Author", DateStarted = new DateTime(2017, 5, 20), Pages = 1024, Description = "Really nice example" }
               
                    }
                };

                _context.Bookshalves.Add(myShelve1);
                _context.Books.AddRange(myShelve1.Books);

                await _context.SaveChangesAsync();


            }
        }

    }
}
