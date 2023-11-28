using Assignment5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Assignment5.Controllers
{
    public class ShoppingCartController : Controller
    {
        MusicStoreContext context;

        public ShoppingCartController(MusicStoreContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var maybeUserId = HttpContext.Session.GetInt32("userId");

            if (!maybeUserId.HasValue)
            {
                // TODO: Handle error
                throw new Exception();
            }

            int userId = maybeUserId.Value;
            var songs = (from x in context.CartItems.Include(item => item.Song.Musician) where x.UserId==userId select x.Song);

            return View(songs);
        }

        public IActionResult AddToCart(int songId)
        {
            var maybeUserId = HttpContext.Session.GetInt32("userId");

            if (!maybeUserId.HasValue)
            {
                // TODO: Handle error
                throw new Exception();
            }

            int userId = maybeUserId.Value;
            var user = (from x in context.Users where x.UserId == userId select x).First();

            using (var transaction = context.Database.BeginTransaction())
            {
                context.Database.ExecuteSql($"SET IDENTITY_INSERT dbo.CartItem ON");

                user.CartItems.Add(new CartItem { SongId = 1, UserId = 1 });
                context.Update(user);
                context.SaveChanges();
                transaction.Commit();
            }

            return RedirectToAction("Index", "Songs");
        }

        public IActionResult ConfirmPurchase()
        {
            return View();
        }

        public IActionResult Purchase()
        {
            return View();
        }
    }
}
