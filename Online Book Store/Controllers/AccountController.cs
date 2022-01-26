using Online_Book_Store.Models;
using System.Web.Mvc;

namespace Online_Book_Store.Controllers
{
    public class AccountController : Controller
    {
        private OnlineBookStoreContext database = new OnlineBookStoreContext();

        [HttpGet]
        public ActionResult Login()
        {
            Customer customer = new Customer();
            return View(customer);
        }

        [HttpPost]
        public ActionResult Login(Customer customer)
        {
            int currentuserId = 0;
            foreach (var item in database.Customers)
            {
                if (item.Username == customer.Username)
                {
                    currentuserId = item.UserId;
                    break;
                }
            }

            if (currentuserId == 0)
            {
                ViewBag.UserIdErrorMessage = "Username does not exist";
                return View(customer);
            }
            if (database.Customers.Find(currentuserId).Password != customer.Password)
            {
                ViewBag.PasswordErrorMessage = "Incorrect Password";
                return View(customer);
            }
            else
            {
                TempData["CurrentUserId"] = currentuserId;
                TempData["CurrentUserName"] = database.Customers.Find(currentuserId).Name;
                return RedirectToAction("Home", "Shop");
            }
        }

        public ActionResult Logout()
        {
            TempData.Clear();
            return RedirectToAction("Home","Shop");
        }

        //Create action method[get and post]
    }
}