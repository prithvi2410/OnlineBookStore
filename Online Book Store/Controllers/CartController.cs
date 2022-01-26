using Online_Book_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Book_Store.Controllers
{
    public class CartController : Controller
    {
        private OnlineBookStoreContext database = new OnlineBookStoreContext();
        public ActionResult Watch()
        {
            if (TempData["CurrentUserId"] == null)
            {
                TempData["LoginMessage"] = "Login to use this cart feature";
                return RedirectToAction("Login","Account");
            }
            else
            {
                TempData.Keep();
                List<CartItem> cartItems = database.Customers.Find((int)TempData["CurrentUserId"]).Cart.CartItems.ToList();
                return View(cartItems);
            }
        }
        public ActionResult RemoveFromCart(int bookId)
        {
            TempData.Keep();
            foreach (var cartItem in database.Customers.Find((int)TempData["CurrentUserId"]).Cart.CartItems.ToList())
            {
                if (cartItem.BookId == bookId)
                {
                    database.CartItems.Remove(cartItem);
                    break;
                }
            }
            database.SaveChanges();
            return RedirectToAction("Watch", "Cart");
        }
        public ActionResult Buy()
        {
            TempData.Keep();
            List<CartItem> cartItems = database.Customers.Find((int)TempData["CurrentUserId"]).Cart.CartItems.ToList();
            if (cartItems.Count() == 0)
            {
                return RedirectToAction("Watch");
            }
            List<string> books = new List<string>();
            Order order = new Order()
            {
                UserId = (int)TempData["CurrentUserId"],
                OrderDate = DateTime.Now
            };
            database.Orders.Add(order);
            database.SaveChanges();

            double orderTotal = 0;
            foreach (var cartItem in cartItems)
            {
                if (cartItem.Book.Stock != 0)
                {
                    books.Add(cartItem.Book.Name);
                    OrderItem orderItem = new OrderItem()
                    {
                        OrderId = order.OrderId,
                        BookId = cartItem.BookId,
                        BookName = cartItem.Book.Name,
                        Quantity = 1,
                        Amount = cartItem.Book.Price
                    };
                    database.OrderItems.Add(orderItem);
                    database.Books.Find(cartItem.BookId).Stock--;
                    database.CartItems.Remove(cartItem);
                    orderTotal += orderItem.Amount;
                }
            }
            database.Orders.Find(order.OrderId).Amount = orderTotal;
            database.Carts.Find((int)TempData["CurrentUserId"]).CartSize = 0;
            database.SaveChanges();

            TempData["PurchasedBooks"] = books;
            return RedirectToAction("BuyCartSummary");
        }
        public ActionResult BuyCartSummary()
        {
            TempData.Keep();
            List<String> PurchasedBooks = (List<String>)TempData["PurchasedBooks"];
            TempData.Remove("PurchasedBooks");
            return View(PurchasedBooks);
        }


    }
}



