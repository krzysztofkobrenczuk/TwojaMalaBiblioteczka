using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Biblioteczka.Models;
using Biblioteczka.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Biblioteczka.Controllers.Web;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Biblioteczka.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<LibraryUser> _signInManager;
        private readonly UserManager<LibraryUser> _userManager;

        public AuthController(UserManager<LibraryUser> userManager, SignInManager<LibraryUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        
      
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Bookshelves", "App");
            }

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
               // var user = await _userManager.FindByNameAsync(vm.Username);

               // if(user != null)
              //  {
                    var signInResult = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, true, false);

                    if (signInResult.Succeeded)
                    {
                        if (string.IsNullOrWhiteSpace(returnUrl))
                        {
                            return RedirectToAction("Bookshelves", "App");
                        }
                        else
                        {
                            return Redirect(returnUrl);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username or password incorrect");
                    }

               // }
            }
            return View();
        }

        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new LibraryUser {
                    UserName = loginViewModel.UserName
                };
                var result = await _userManager.CreateAsync(user, loginViewModel.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "App");
                }
                else
                {
                    ModelState.AddModelError("", "Username or password incorrect");
                }
            }
            return View();
        }



        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "App");
        } 
    }
}
