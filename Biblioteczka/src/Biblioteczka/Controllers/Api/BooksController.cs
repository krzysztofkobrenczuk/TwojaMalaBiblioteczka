using AutoMapper;
using Biblioteczka.Models;
using Biblioteczka.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteczka.Controllers.Api
{
    
    [Route("/api/bookshelves/{bookshelveName}/books")]
    [Authorize]
    public class BooksController : Controller
    {
        private ILogger<BooksController> _logger;
        private ILibraryRepository _repository;

        public BooksController(ILibraryRepository repository, ILogger<BooksController> logger)
        {
            _repository = repository;
            _logger = logger;

        }
        [HttpGet("")]
        public IActionResult Get(string bookshelveName)
        {
            try
            {
                var bookShelve = _repository.GetUserBookShelvesByName(bookshelveName, User.Identity.Name);

                return Ok(Mapper.Map<IEnumerable<BookViewModel>>(bookShelve.Books.OrderBy(s => s.BookId).ToList()));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get books: {0}", ex);
            }
            return BadRequest("Failed to get books");
          
        }

        [HttpPost("")]
        public async Task<IActionResult> Post(string bookshelveName, [FromBody]BookViewModel vm)
        {
            try
            {

                //If the VM is valid
                if (ModelState.IsValid)
                {
                    var newBook = Mapper.Map<Book>(vm);
                    //Lookup the

                    // Save to the Database
                    _repository.AddBook(bookshelveName, newBook, User.Identity.Name);

                    if (await _repository.SaveChangesAsync())
                    {
                        return Created($"/api/bookshelves/{bookshelveName}/books/{newBook.Name}",
                   Mapper.Map<BookViewModel>(newBook));
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save new Book: {0} ", ex);
            }
            return BadRequest("Failed to save new Book");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //var bookshelve = _repository.Find(id);
            var book = _repository.GetUserBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            _repository.DeleteBook(id);

            if (await _repository.SaveChangesAsync())
            {
                return new NoContentResult();
            }

            return BadRequest("Failed to delete");

        }
    }
}
