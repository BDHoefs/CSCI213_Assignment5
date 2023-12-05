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
                return RedirectToAction("Index", "Home");
            }

            int userId = maybeUserId.Value;
            var songs = (from x in context.CartItems.Include(item => item.Song.Musician) where x.UserId==userId select x.Song);

            decimal total = 0;
            foreach(var song in songs)
            {
                total += song.Price;
            }

            return View((songs.ToList(), total));
        }

        public IActionResult ClearCart()
        {
            var maybeUserId = HttpContext.Session.GetInt32("userId");

            if (!maybeUserId.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            int userId = maybeUserId.Value;
            var user = (from x in context.Users where x.UserId == userId select x).First();
            var cartItems = from x in context.CartItems where x.UserId == userId select x;

            using (var transaction = context.Database.BeginTransaction())
            {
                cartItems.ExecuteDelete();
                context.SaveChanges();
                transaction.Commit();
            }
            return RedirectToAction("Index");
        }

        [Route("ShoppingCart/AddToCart/{songId}")]
        public IActionResult AddToCart(int songId)
        {
            var maybeUserId = HttpContext.Session.GetInt32("userId");

            if (!maybeUserId.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            int userId = maybeUserId.Value;
            var user = (from x in context.Users where x.UserId == userId select x).Include(user=>user.CartItems).First();

            bool songAlreadyInCart = (from x in context.CartItems where (x.SongId == songId && x.UserId == userId) select x).Count() > 0;
            if(songAlreadyInCart)
            {
                return RedirectToAction("Browse", "Songs");
            }

            CartItem item = new CartItem { SongId = songId, UserId = userId };
            context.CartItems.Add(item);
            context.SaveChanges();

            return RedirectToAction("Browse", "Songs");
        }

        public IActionResult EnterPaymentDetails()
        {
            var maybeUserId = HttpContext.Session.GetInt32("userId");

            if (!maybeUserId.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            int userId = maybeUserId.Value;
            var user = (from x in context.Users where x.UserId == userId select x).First();

            if (user.Type != "Customer")
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Purchase([Bind("Id,NameOnCard,CardNumber,Expiration,CVV")] PaymentDetails paymentDetails)
        {
            // TODO: In a real world application purchasing logic would go here
            return RedirectToAction("Index", "Home");
        }
    }
}
