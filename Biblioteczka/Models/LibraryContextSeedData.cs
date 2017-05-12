using Microsoft.AspNetCore.Identity;
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

            if(await _userManager.FindByEmailAsync("kobra1253@gmail.com") == null)
            {
                var user = new LibraryUser()
                {
                    UserName = "kobra",
                    Email = "kobra1253@gmail.com"
                };
                await _userManager.CreateAsync(user, "P@ssw0rd!");
            }


            if (!_context.Bookshalves.Any())
            {
                var myShelve1 = new Bookshelve()
                {
                    Name = "Półka Maj",
                    DateCreated = DateTime.UtcNow,
                    UserName = "kobra",
                    Books = new List<Book>
                    {
                        new Book() { Name = "Droga Królów", Author = "Brandon Sanderson", DateStarted = new DateTime(2017, 5, 20), Pages = 1024, Description = "Bardzo fajna książka" },
                        new Book() { Name = "Hari Pota", Author = "JK Rowling", DateStarted = new DateTime(2015, 5, 13), Pages = 222, Description = "Hari Pota ty już wiesz co" },
                        new Book() { Name = "Gra o Tron", Author = "RR Martin",  DateStarted = new DateTime(2014, 5, 22), Pages = 898, Description = "Polityczna książka" },
                        new Book() { Name = "Amerykańscy Bogowie", Author = "Brandon Sanderson", DateStarted = new DateTime(2016, 5, 10), Pages = 332, Description = "Niby amerykańscy a jednak nie" }

                    }
                };

                _context.Bookshalves.Add(myShelve1);
                _context.Books.AddRange(myShelve1.Books);

                var myShelve2 = new Bookshelve()
                {
                    Name = "Półka Kwiecień",
                    DateCreated = DateTime.UtcNow,
                    UserName = "", // TODO Add UserName
                    Books = new List<Book>
                    {
                        new Book() { Name = "Lalalala", Author = "Hejeeeeja", DateStarted = new DateTime(2017, 4, 20), Pages = 124, Description = "La hej la" },
                        new Book() { Name = "Siubidubi", Author = "Agurola", DateStarted = new DateTime(2015, 4, 13), Pages = 122, Description = "Siubi dubi dance dance" },
                        new Book() { Name = "Gra o grę o grę", Author = "George", DateStarted = new DateTime(2014, 4, 22), Pages = 3332, Description = "Dobra książka" },
                        new Book() { Name = "Mon mon mon", Author = "Omomom", DateStarted = new DateTime(2016, 4, 10), Pages = 3222, Description = "Uhuhu witom" }

                    }
                };

                _context.Bookshalves.Add(myShelve2);
                _context.Books.AddRange(myShelve2.Books);

                await _context.SaveChangesAsync();


            }
        }

    }
}
