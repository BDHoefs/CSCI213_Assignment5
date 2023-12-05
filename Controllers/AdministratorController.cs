using Assignment5.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment5.Controllers
{
    public class AdministratorController : Controller
    {
        MusicStoreContext context;

        public AdministratorController(MusicStoreContext context) {
            this.context = context;
        }

        public IActionResult Index()
        {
            var maybeUserId = HttpContext.Session.GetInt32("userId");

            if (!maybeUserId.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            int userId = maybeUserId.Value;

            var user = (from x in context.Users where x.UserId == userId select x).First();
            if(user.Type != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
