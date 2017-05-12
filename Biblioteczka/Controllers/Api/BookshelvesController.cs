using AutoMapper;
using Biblioteczka.Models;
using Biblioteczka.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteczka.Controllers.Api
{
     
    [Route("api/bookshelves")]
    
    public class BookshelvesController : Controller
    {
        private ILogger<BookshelvesController> _logger;
        private ILibraryRepository _repository;

        public BookshelvesController(ILibraryRepository repository, ILogger<BookshelvesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                //Pobranie półek należących do poszczególnego usera
                var results = _repository.GetBookshelvesByUsername(this.User.Identity.Name);

                return Ok(Mapper.Map<IEnumerable<BookshelveViewModel>>(results));
            }
            catch (Exception ex)
            {
                //Logging
                _logger.LogError($"Failed to getAll Bookshelves: {ex}");
                return BadRequest("Error occured" + ex);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]BookshelveViewModel theBookshelve)  //frombody w celu przypieczętowania danych pochodzących z POST do naszego modelu
        {
            if (ModelState.IsValid)
            {
                //Save to the Database -> chcemy przekonwertować ViewModel do nowego newBookshelve
                //aby móc przechwycić takie smaczki jak różne nazwy pola Name używamy AutoMappera który robi to za nas
                var newBookshelve = Mapper.Map<Bookshelve>(theBookshelve);          // chcemy obiekt Bookshelve przekazać do obiektu theBookshelve

                newBookshelve.UserName = User.Identity.Name;

                _repository.AddBookShelve(newBookshelve);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/bookshelve/{theBookshelve.Name}", Mapper.Map<BookshelveViewModel>(newBookshelve));
                }
            }
            return BadRequest("Failed to save the bookshelve");      //Zwracamy stan modelu  np. w przypadku za krótkiej nazwy stan zwraca komunikat o zbyt krótkiej nazwie  
        }

    }
}
