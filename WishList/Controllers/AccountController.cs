using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WishList.Models;
using WishList.Models.AccountViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WishList.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        // GET: /<controller>/
        public IActionResult Index()    
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Register");
            }

            var applicationUser = new ApplicationUser
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email,
                PasswordHash = registerViewModel.Password
            };

            var result = await _userManager.CreateAsync(applicationUser);
            if(!result.Succeeded)
            {
                foreach(var errors in result.Errors)
                {
                    ModelState.AddModelError("Password", errors.Description);
                }

                return View(registerViewModel);
            }

            return RedirectToAction("HomeController.Index");
        }
    }
}
