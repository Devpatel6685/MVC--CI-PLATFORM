using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM.repository.Repository;
using CI_PLATFORM_.repository.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_OF_CI_PLATFORM.Models;
using System.Diagnostics;
using System.Security.Claims;
using System.Transactions;

namespace MVC_OF_CI_PLATFORM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserInterface _iuserRepository;

        public HomeController(ILogger<HomeController> logger, IUserInterface iuserRepository)
        {
            _iuserRepository = iuserRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {   
            return View();
        }

        public IActionResult LOGIN()
        {
            HttpContext.Session.Clear();
            return View();
        }
        
        [HttpPost]
        public IActionResult LOGIN(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                
                var entity = _iuserRepository.login(user);
                if (entity == "User Does not Exists" )
                {
                    ModelState.AddModelError("Email", entity);
                    return View("LOGIN");
                }
                if (entity == "invalid password")
                {
                    ModelState.AddModelError("Password", entity);
                    return View("LOGIN");
                }
                var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Email) },
                    CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                HttpContext.Session.SetString("username", entity);
                TempData["LOGIN"] = "successfully logged in";
                return RedirectToAction("PlatformLanding", "Mission");
            }
            return View("LOGIN");
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return RedirectToAction("PlatformLanding","Mission");
        }

        public IActionResult FORGOTPASSWORD()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FORGOTPASSWORD(forgotviewmodel user)
        {
            if (ModelState.IsValid)
            {
                var entity = _iuserRepository.FORGOTPASSWORD(user);
                if (entity == null)
                {
                    ModelState.AddModelError("Email", "User not found");
                    return RedirectToAction("FORGOTPASSWORD");
                }
                HttpContext.Session.SetString("Token", entity);
                TempData["FORGOTPASSWORD"] = "Link Send to Mail";
                return View();
            }
            return View();
        }

        public IActionResult RESETPAGE()
        {
           if(HttpContext.Session.GetString("Token")== null)
            {
                return NotFound("Link Expired");
            }
            return View();
        }

        [HttpPost]
        public IActionResult RESETPAGE(resetviemodel entry)
        {
            if (ModelState.IsValid)
            {
               
                var token = HttpContext.Session.GetString("Token");
                var entity = _iuserRepository.RESETPAGE(entry, token);
                if (entity == null)
                {
                    ModelState.AddModelError("", "invalid user");
                    return RedirectToAction("RESETPAGE");
                }
                if (entity.Equals("Confirm password is not matching with password"))
                {
                    ModelState.AddModelError("ConfirmPassword", entity);
                    return RedirectToAction("RESETPAGE");
                }
                HttpContext.Session.Remove(token);
                TempData["RESETPAGE"] = "Password Changed Successfully";
                return RedirectToAction("LOGIN");
            }
            return View();
        }

        public IActionResult REGISTRATIONPAGE()
        {
            return View();
        }

        [HttpPost]
        public IActionResult REGISTRATIONPAGE(registerviewmodel user )
        {
            
            if (ModelState.IsValid)
            {
                var entity = _iuserRepository.REGISTRATIONPAGE(user);
                if (entity == "user already exist")
                {
                    ModelState.AddModelError("Email",entity);
                    return View();
                }
                TempData["REGISTRATIONPAGE"] = "Successfully Registered";
                return RedirectToAction("LOGIN");
            }
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

     /*   [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    }
}