using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteczka.Models
{
    public interface ILibraryRepository
    {
        IEnumerable<Bookshelve> GetAllBookshelves();
        IEnumerable<Bookshelve> GetBookshelvesByUsername(string username);

        Bookshelve GetBookShelvesByName(string bookshelveName);
        Bookshelve GetUserBookShelvesByName(string bookshelveName, string name);
        Bookshelve GetUserBookShelvesById(int id, string username);

        void AddBookShelve(Bookshelve bookshelve);
        void DeleteBookshelve(Bookshelve bookshelve);
        void AddBook(string bookshelveName, Book newBook, string username);

        Task<bool> SaveChangesAsync();
        
    }
}