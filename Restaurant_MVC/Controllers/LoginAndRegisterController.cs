using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using Restaurant_MVC.Models;

namespace Restaurant_MVC.Controllers
{
    public class LoginAndRegisterController : Controller
    {
        private readonly ModelContext _context;
        public LoginAndRegisterController(ModelContext context) {
        _context = context;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Customer customer , string userName , string Password) // USERNAME = Bayan
        {

            var user = _context.UserLogins.Where(x => x.UserName == userName).SingleOrDefault();
            if(user == null) {
                if (ModelState.IsValid)
                {
                    _context.Add(customer);
                    _context.SaveChanges();

                    UserLogin userLogin = new UserLogin();
                    userLogin.UserName = userName;
                    userLogin.Password = Password;
                    userLogin.CustomerId = customer.Id;
                    userLogin.RoleId = 3;
                    _context.Add(userLogin);
                    _context.SaveChanges();

                }
            }
            ModelState.AddModelError("", "UserName already exist");
            return View();
        }
    
    
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            var user = await _context.UserLogins.
                Where(x=>x.UserName == userLogin.UserName && x.Password == userLogin.Password).SingleOrDefaultAsync();

            if (user != null) {
            switch (user.RoleId)
                {
                    case 1:
                        HttpContext.Session.SetInt32("AdminId", (int)user.CustomerId);
                       return RedirectToAction("Index" , "Admin");
                    case 3:
                        HttpContext.Session.SetInt32("CustomerId", (int)user.CustomerId);
                        return RedirectToAction("Index", "Home");

                }
            }
            ModelState.AddModelError("", "UserName or Password are incorret");
            return View();
        }

    }
}
