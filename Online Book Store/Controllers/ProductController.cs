using Online_Book_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Book_Store.Controllers
{
    public class ProductController : Controller
    {
        private OnlineBookStoreContext database = new OnlineBookStoreContext();
        public ActionResult BuyConfirmation(int bookId)
        {
            if (TempData["CurrentUserId"] == null)
            {
                TempData["LoginMessage"] = "Login to use this buy feature";
                return RedirectToAction("Login", "Account");
            }
            TempData.Keep();
            Book book = database.Books.Find(bookId);
            return View(book);
        }
        public ActionResult Buy(int bookId)
        {
            if (TempData["CurrentUserId"] == null)
            {
                TempData["LoginMessage"] = "Login to use this buy feature";
                return RedirectToAction("Login", "Account");
            }

            TempData.Keep();
            ViewBag.CurrentUserId = TempData["CurrentUserId"];
            Order order = new Order()
            {
                UserId = (int)TempData["CurrentUserId"],
                OrderDate = DateTime.Now
            };
            database.Orders.Add(order);
            database.SaveChanges();

            OrderItem orderItem = new OrderItem()
            {
                OrderId = order.OrderId,
                BookId = bookId,
                BookName = database.Books.Find(bookId).Name,
                Quantity = 1,
                Amount = database.Books.Find(bookId).Price
            };
            
            database.OrderItems.Add(orderItem);
            database.Orders.Find(order.OrderId).Amount = orderItem.Amount;
            database.Books.Find(bookId).Stock--;
            database.SaveChanges();

            Book PurchasedBook = database.Books.Find(bookId);
            TempData["PurchasedBook"] = PurchasedBook;
            return RedirectToAction("BuySummary");
        }
        public ActionResult BuySummary()
        {
            TempData.Keep();
            Book PurchasedBook = (Book)TempData["PurchasedBook"];
            TempData.Remove("PurchasedBook");
            return View(PurchasedBook);
        }
        public ActionResult AddToCart(int bookId, string viewName,string value)
        {
            if (TempData["CurrentUserId"] == null)
            {
                TempData["LoginMessage"] = "Login to use this add to cart feature";
                return RedirectToAction("Login", "Account");
            }

            TempData.Keep();
            database.CartItems.Add(new CartItem()
            {
                CartId = (int)TempData["CurrentUserId"],
                BookId = bookId,
                Quantity = 1
            }
            );
            database.Carts.Find((int)TempData["CurrentUserId"]).CartSize++;
            database.SaveChanges();
            if (value == null)
                return RedirectToAction($"{viewName}", "Shop");
            else if(viewName=="Category")
                return RedirectToAction($"{viewName}", "Shop",new { category = value});
            else
                return RedirectToAction($"{viewName}", "Shop", new { searched = value });
            
              
        }

    }
}
