using Microsoft.AspNetCore.Mvc;

namespace Assignment5.Controllers
{
    public class SessionTestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SetSession()
        {
            HttpContext.Session.SetInt32("userId", 1);
            return RedirectToAction("Index", "Home");
        }
    }
}
