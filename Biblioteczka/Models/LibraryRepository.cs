using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteczka.Models
{
    public class LibraryRepository : ILibraryRepository 
    {
        private LibraryContext _context;
        private ILogger<ILibraryRepository> _logger;

        public LibraryRepository(LibraryContext context, ILogger<ILibraryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddBook(string bookshelveName, Book newBook, string username)
        {
            var bookshelve = GetUserBookShelvesByName(bookshelveName, username);

            if(bookshelve != null)
            {
                bookshelve.Books.Add(newBook);
                _context.Books.Add(newBook);
            }
        }

        //Add new bookshelve to database
        public void AddBookShelve(Bookshelve bookshelve)
        {
            _context.Add(bookshelve);
        }

       public void DeleteBookshelve(int id)
        {
            var entity = _context.Bookshalves.First(b => b.Id == id);
            _context.Bookshalves.Remove(entity);
            
            
        }

        //Test method
        public Bookshelve Find(int id)
        {
            return _context.Bookshalves.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Bookshelve> GetAllBookshelves()
        {
            _logger.LogInformation("Getting All Bookshelves from the Database");
            return _context.Bookshalves.ToList();
        }

        public Bookshelve GetBookShelvesByName(string bookshelveName)
        {
            return _context.Bookshalves
                .Include(t => t.Books)
                .Where(t => t.Name == bookshelveName)
                .FirstOrDefault();
        }

        public IEnumerable<Bookshelve> GetBookshelvesByUsername(string name)
        {

            return _context
                .Bookshalves
                .Include(t => t.Books)
                .Where(t => t.UserName == name)
                .ToList();
        }
      

        public Bookshelve GetUserBookShelvesByName(string bookshelveName, string username)
        {
            return _context.Bookshalves
         .Include(t => t.Books)
         .Where(t => t.Name == bookshelveName && t.UserName == username)
         .FirstOrDefault();
        }

        public Bookshelve GetUserBookShelvesById(int id, string username)
        {
         return _context.Bookshalves
         .Include(t => t.Books)
         .Where(t => t.Id == id && t.UserName == username)
         .FirstOrDefault();
        }

        public async Task<bool> SaveChangesAsync()
        {
           return (await _context.SaveChangesAsync()) > 0; 
        }
    }
}
