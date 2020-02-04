using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProject.Controllers
{
    public class AccountController : Controller
    {


        private readonly AccountDbContext context;
        public AccountController(AccountDbContext dbContext) {
            context = dbContext;
        }
        public IActionResult Index(string userName)
        {
            ViewBag.UserName = userName;
            return View();
        }
        public IActionResult Signin()
        {
            SigninViewModel signinToAccount = new SigninViewModel();
            return View(signinToAccount);
        }
        /* [HttpPost]
         public IActionResult Signin(SigninViewModel signinToAccount)
         {
             if (ModelState.IsValid)
             {
                 var isNameAlreadyExists = context.Pharmacies.Any(X => X.PharmacyName == signinToAccount.PharmacyName);
                 var isPasswordAlredyExists = context.Pharmacies.Any(x => x.Password == signinToAccount.Password);

                 *//*ClaimsIdentity identity = null;
                 bool IsAuthenticated = false;
 *//*

                 if (!(isNameAlreadyExists && isPasswordAlredyExists))
                 {
                     ModelState.AddModelError("", "There is no account with this name and password");
                     return Redirect("Signup");
                 }
                 Pharmacy exitsingPharmacy = new Pharmacy
                 {
                     PharmacyName = signinToAccount.PharmacyName,
                     Password = signinToAccount.Password

                 };
                 *//*identity = new ClaimsIdentity(new[] {
                     new Claim(ClaimTypes.Name, exitsingPharmacy.PharmacyName)
                 }, CookieAuthenticationDefaults.AuthenticationScheme);
                 IsAuthenticated = true;
                 var principal = new ClaimsPrincipal(identity);

                 var Signin = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);*//*

                 return Redirect("Index?userName="+exitsingPharmacy.PharmacyName);

             }
             return View(signinToAccount);

         }*/
        [HttpPost]
        public async Task<ActionResult> Signin(SigninViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pharmacy = await context.Pharmacies
                .SingleOrDefaultAsync(m => m.PharmacyName == model.PharmacyName && m.Password == model.Password);
                if (pharmacy == null)
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View("Signin");
                }
                HttpContext.Session.SetString("userId", pharmacy.PharmacyName);
                var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, model.PharmacyName),
                     new Claim(ClaimTypes.Role, "Pharmacy")
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            }
            else
            {
                return View("Signin");
            }
            return RedirectToAction("Index");
        }
        public IActionResult Signup()
        {
            SignupViewModel createAccount = new SignupViewModel();
            return View(createAccount);
        }
        
        [HttpPost]
        public IActionResult Signup(SignupViewModel createAccount)
        {
            if (ModelState.IsValid)
            {
                var isNameAlreadyExists = context.Pharmacies.Any(X => X.PharmacyName == createAccount.PharmacyName);
                var isPasswordAlredyExists = context.Pharmacies.Any(x => x.Password == createAccount.Password);
                var isDEANumberAlredyExists = context.Pharmacies.Any(x => x.DEANumber == createAccount.DEANumber);
                if (isNameAlreadyExists && isPasswordAlredyExists && isDEANumberAlredyExists)
                {
                    ModelState.AddModelError("", "User with this Account already exists so plase go to signin");
                    return View(createAccount);
                }
                Pharmacy newPharmacy = new Pharmacy
                {
                    PharmacyName = createAccount.PharmacyName,
                    Email = createAccount.Email,
                    Password = createAccount.Password,
                    DEANumber = createAccount.DEANumber,
                    NPINumber = createAccount.NPINumber,
                    StreetNumberAndName = createAccount.StreetNumberAndName,
                    City = createAccount.City,
                    State = createAccount.State,
                    ZipCode = createAccount.ZipCode
                };
                context.Pharmacies.Add(newPharmacy);
                context.SaveChanges();
                return Redirect("/Account/Signin");
            }
            return View(createAccount);
        }

        public IActionResult Register()
        {
            RegisterViewModel addAccount = new RegisterViewModel();
            return View(addAccount);
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel addAccount)
        {
            if (ModelState.IsValid)
            {

                var isEmailAlreadyExists = context.Users.Any(x => x.Email == addAccount.Email);
                var isPasswordAlreadyExists = context.Users.Any(x => x.Password == addAccount.Password);
                if (isEmailAlreadyExists && isPasswordAlreadyExists)
                {
                    ModelState.AddModelError("", "User with this account already exists");
                    return View(addAccount);
                }
                User newUser = new User
                {
                    Name = addAccount.Name,
                    Email = addAccount.Email,
                    Password = addAccount.Password,

                };
                context.Users.Add(newUser);
                context.SaveChanges();
                // return Redirect("Index?userName=" + newUser.Name);
                return Redirect("/Account/Login");

            }

            return View(addAccount);
        }
        public IActionResult Login()
        {
            LoginViewModel login = new LoginViewModel();
            return View(login);
        }

        [HttpPost]

        public IActionResult Login(LoginViewModel existingUser)
        {
            if (ModelState.IsValid)
            {
                var isNameAlreadyExists = context.Users.Any(X => X.Name == existingUser.Name);
                var isPasswordAlredyExists = context.Users.Any(x => x.Password == existingUser.Password);
                ClaimsIdentity identity = null;
                bool IsAuthenticated = false;

                if (!(isNameAlreadyExists && isPasswordAlredyExists))
                {
                    ModelState.AddModelError("", "There is no account with this name and password please create account");
                    return Redirect("Register");
                }
                User extingUser = new User
                {
                    Name = existingUser.Name,
                    Password = existingUser.Password
                };
                identity = new ClaimsIdentity(new[] {
                     new Claim(ClaimTypes.Name, existingUser.Name),
                     new Claim(ClaimTypes.Role, "User")
                 }, CookieAuthenticationDefaults.AuthenticationScheme);
                IsAuthenticated = true;
                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return Redirect("Index?userName=" + extingUser.Name);

            }

            return View(existingUser);
        }

        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Account/Login");

        }
    }
}
       /* [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var users = await context.Users
                .SingleOrDefaultAsync(m => m.Name == model.Name && m.Password == model.Password);
                if (users == null)
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View("Login");
                }
                HttpContext.Session.SetString("userId", users.Name);
                var user = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, model.Name)
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(user);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            }
            else
            {
                return View("Login");
            }
            return Redirect("Index");
        }
         
          
        
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        public void ValidationMessage(string key, string alert, string value)
        {
            try
            {
                TempData.Remove(key);
                TempData.Add(key, value);
                TempData.Add("alertType", alert);
            }
            catch
            {
                Debug.WriteLine("TempDataMessage Error");
            }

        }
    }
}
*/