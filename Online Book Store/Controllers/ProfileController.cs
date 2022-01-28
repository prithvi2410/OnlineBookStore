using Online_Book_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Online_Book_Store.Controllers
{
    public class ProfileController : Controller
    {
        private OnlineBookStoreContext database = new OnlineBookStoreContext();
        // GET: Profile
        public ActionResult Details()
        {
            if (TempData["CurrentUserId"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempData.Keep();
            Customer customer = database.Customers.Find((int)TempData["CurrentUserId"]);
            return View(customer);
        }
    }
}