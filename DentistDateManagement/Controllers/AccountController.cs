using DentistDateManagement.Data.Entity;
using DentistDateManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentistDateManagement.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// Dependency-Injection Design
        /// </summary>
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private RoleManager<AppRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInUser, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInUser;
            _roleManager = roleManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                ModelState.AddModelError(String.Empty, "Kullanıcı Bulunamadı");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Profile");
            }
            ModelState.AddModelError(String.Empty, "Kullanıcı Adı / Şifre Hatalı");

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AppUser user = new AppUser()
            {
                UserName = model.UserName,
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Color = model.Color,
                IsDentist = model.IsDentist
            };

            IdentityResult result = _userManager.CreateAsync(user, model.Password).Result;

            if (result.Succeeded)
            {
                bool roleCheck = model.IsDentist ? AddRole("Dentist") : AddRole("Secretary");
                if (!roleCheck)
                {
                    return View("Error");
                }
                _userManager.AddToRoleAsync(user, model.IsDentist ? "Dentist" : "Secretary").Wait();
                return RedirectToAction("Login", "Account");
            }

            return View("Error");
        }

        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync().Wait();

            return RedirectToAction("Login", "Account");
        }

        public IActionResult Denied()
        {
            return View();
        }

        private bool AddRole(string roleName)
        {
            if (!_roleManager.RoleExistsAsync(roleName).Result)
            {
                AppRole role = new AppRole()
                {
                    Name = roleName
                };
                IdentityResult result = _roleManager.CreateAsync(role).Result;

                return result.Succeeded;
            }
            return true;
        }
    }
}
