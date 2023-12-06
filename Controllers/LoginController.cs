using Assignment5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Assignment5.Controllers
{
    public class LoginController : Controller
    {
        MusicStoreContext context;
        public LoginController(MusicStoreContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SetSession()
        {
            HttpContext.Session.SetInt32("userId", 1);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login(string userName, string password)
        {
            var user = (from x in context.Users where x.UserName == userName select x).First();
            if (user.Password != password)
            {
                return RedirectToAction("Index", "Home");
            }

            HttpContext.Session.SetInt32("userId", user.UserId);
            return RedirectToAction("Index", "Home");
        }
    }
}
