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
        Book GetUserBookById(int id);


        void AddBookShelve(Bookshelve bookshelve);   
        void AddBook(string bookshelveName, Book newBook, string username);
        void DeleteBookshelve(int id);
        void DeleteBook(int id);
        Bookshelve Find(int id);

        Task<bool> SaveChangesAsync();
     
    }
}