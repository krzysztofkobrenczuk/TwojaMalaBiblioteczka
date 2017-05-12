using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Biblioteczka.ViewModels;
using Biblioteczka.wwwroot.Services;
using Microsoft.Extensions.Configuration;
using Biblioteczka.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Biblioteczka.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        private LibraryContext _context;
        private ILibraryRepository _repository;
        private ILogger<AppController> _logger;

        public AppController(IMailService mailService, 
            IConfigurationRoot config, 
            ILibraryRepository repository, 
            ILogger<AppController> logger)
        {
            _mailService = mailService;
            _config = config;
            _repository = repository;
            _logger = logger;
        }

        public IActionResult Index()
        {
                return View();  
        }


        //ASP.NET Identity
        [Authorize]
        public IActionResult Bookshelves()
        {
               // var data = _repository.GetAllBookshelves();
                return View();    
        }



        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                _mailService.SendMail(_config["MailSettings:ToAddress"], model.Email, "From Thelsakda", model.Message);

                ViewBag.UserMessage = "Message Sent";
            }
       
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult AddBook()
        {
            return View();
        }
    


    }
}
