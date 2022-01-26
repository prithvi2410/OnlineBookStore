using Online_Book_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Book_Store.Controllers
{
    public class OrderController : Controller
    {
        private OnlineBookStoreContext database = new OnlineBookStoreContext();
        public ActionResult History()
        {
            if (TempData["CurrentUserId"] == null)
            {
                TempData["LoginMessage"] = "Login to use this Orders feature";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                TempData.Keep();
                List<Order> orderList = database.Customers.Find(Convert.ToInt32(TempData["CurrentUserId"])).Orders.ToList();
                return View(orderList);
            }
        }

        public ActionResult Details(int orderId)
        {
            TempData.Keep();
            List<OrderItem> orderItemsList = database.Orders.Find(orderId).OrderItems.ToList();         
            return View(orderItemsList);
        }
    }
}